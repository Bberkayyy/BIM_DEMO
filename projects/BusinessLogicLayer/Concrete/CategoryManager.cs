using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using BusinessLogicLayer.Extensions;
using Core.Shared;
using DataAccessLayer.Repositories.CategoryRepositories;
using DataAccessLayer.Repositories.ProductRepositories;
using EntityLayer.Dtos.RequestDtos;
using EntityLayer.Dtos.RequestDtos.CategoryRequestDtos;
using EntityLayer.Dtos.ResponseDtos.CategoryResponseDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRules _rules;

    public CategoryManager(ICategoryRepository categoryRepository, IProductRepository productRepository, ICategoryRules rules)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _rules = rules;
    }

    public Response<ResultCategoryResponseDto> TCreate(CreateCategoryRequestDto createCategoryRequestDto)
    {
        try
        {
            short categoryNo = GenerateClass.GenerateRandomUniqueCategoryNo(_rules);
            _rules.CategoryNameCanNotBeNullOrWhiteSpace(createCategoryRequestDto.Name);
            _rules.CategoryNameMustBeUnique(createCategoryRequestDto.Name);
            Category createCategory = CreateCategoryRequestDto.ConvertToEntity(createCategoryRequestDto, categoryNo);
            Category createdCategory = _categoryRepository.Create(createCategory);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(createdCategory);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                Message = "Category created successfully!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }

    }

    public async Task<Response<ResultCategoryResponseDto>> TCreateAsync(CreateCategoryRequestDto createCategoryRequestDto)
    {
        try
        {
            short categoryNo = GenerateClass.GenerateRandomUniqueCategoryNo(_rules);
            _rules.CategoryNameCanNotBeNullOrWhiteSpace(createCategoryRequestDto.Name);
            _rules.CategoryNameMustBeUnique(createCategoryRequestDto.Name);
            Category createCategory = CreateCategoryRequestDto.ConvertToEntity(createCategoryRequestDto, categoryNo);
            Category createdCategory = await _categoryRepository.CreateAsync(createCategory);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(createdCategory);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                Message = "Category created successfully!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }

    }

    public Response<ResultCategoryResponseDto> TDeleteByCategoryNo(short categoryNo)
    {
        try
        {
            Category? category = _categoryRepository.GetByFilter(x => x.CategoryNo == categoryNo, include: x => x.Include(x => x.Products));
            _rules.CategoryExists(category);
            foreach (var product in category!.Products)
            {
                product.Deleted = DateTime.Now;
                _productRepository.Delete(product);
            }
            category.Deleted = DateTime.Now;
            _categoryRepository.Delete(category);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(category);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                Message = $"Category deleted successfully! ({category.Products.Count} product is affected)",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultCategoryResponseDto>> TDeleteByCategoryNoAsync(short categoryNo)
    {
        try
        {
            Category? category = await _categoryRepository.GetByFilterAsync(x => x.CategoryNo == categoryNo, include: x => x.Include(x => x.Products));
            _rules.CategoryExists(category);
            foreach (var product in category!.Products)
            {
                product.Deleted = DateTime.Now;
                await _productRepository.DeleteAsync(product);
            }
            category.Deleted = DateTime.Now;
            await _categoryRepository.DeleteAsync(category);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(category);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                Message = $"Category deleted successfully! ({category.Products.Count} product is affected)",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public Response<ResultCategoryResponseDto> TDeleteById(int id)
    {
        try
        {
            Category? category = _categoryRepository.GetByFilter(x => x.Id == id, include: x => x.Include(x => x.Products));
            _rules.CategoryExists(category);
            foreach (var product in category!.Products)
            {
                product.Deleted = DateTime.Now;
                _productRepository.Delete(product);
            }
            category.Deleted = DateTime.Now;
            _categoryRepository.Delete(category);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(category);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                Message = $"Category deleted successfully! ({category.Products.Count} product is affected)",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }

    }

    public async Task<Response<ResultCategoryResponseDto>> TDeleteByIdAsync(int id)
    {
        try
        {
            Category? category = await _categoryRepository.GetByFilterAsync(x => x.Id == id, include: x => x.Include(x => x.Products));
            _rules.CategoryExists(category);
            foreach (var product in category!.Products)
            {
                product.Deleted = DateTime.Now;
                await _productRepository.DeleteAsync(product);
            }
            category.Deleted = DateTime.Now;
            await _categoryRepository.DeleteAsync(category);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(category);

            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                Message = $"Category deleted successfully! ({category.Products.Count} product is affected)",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }

    }

    public Response<ResultCategoryResponseDto> TDeleteFromDatabaseByCategoryNo(short categoryNo)
    {
        try
        {
            Category? category = _categoryRepository.GetByFilter(x => x.CategoryNo == categoryNo);
            _rules.CategoryExists(category, isDeleteFromDatabase: true);
            _categoryRepository.DeleteFromDatabase(category!);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(category!);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                Message = "Category deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }

    }

    public async Task<Response<ResultCategoryResponseDto>> TDeleteFromDatabaseByCategoryNoAsync(short categoryNo)
    {
        try
        {
            Category? category = await _categoryRepository.GetByFilterAsync(x => x.CategoryNo == categoryNo);
            _rules.CategoryExists(category, isDeleteFromDatabase: true);
            await _categoryRepository.DeleteFromDatabaseAsync(category!);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(category!);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                Message = "Category deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }

    }

    public Response<ResultCategoryResponseDto> TDeleteFromDatabaseById(int id)
    {
        try
        {
            Category? category = _categoryRepository.GetByFilter(x => x.Id == id);
            _rules.CategoryExists(category, isDeleteFromDatabase: true);
            _categoryRepository.DeleteFromDatabase(category!);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(category!);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                Message = "Category deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultCategoryResponseDto>> TDeleteFromDatabaseByIdAsync(int id)
    {
        try
        {
            Category? category = await _categoryRepository.GetByFilterAsync(x => x.Id == id);
            _rules.CategoryExists(category, isDeleteFromDatabase: true);
            await _categoryRepository.DeleteFromDatabaseAsync(category!);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(category!);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                Message = "Category deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<List<ResultCategoryResponseDto>> TGetAll(Expression<Func<Category, bool>>? predicate = null, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        List<Category> categories = _categoryRepository.GetAll(predicate ?? (x => x.Deleted == null), include ?? (x => x.Include(x => x.Products)));
        List<ResultCategoryResponseDto> response = categories.Select(x => ResultCategoryResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultCategoryResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<Response<List<ResultCategoryResponseDto>>> TGetAllAsync(Expression<Func<Category, bool>>? predicate = null, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        List<Category> categories = await _categoryRepository.GetAllAsync(predicate ?? (x => x.Deleted == null), include ?? (x => x.Include(x => x.Products)));
        List<ResultCategoryResponseDto> response = categories.Select(x => ResultCategoryResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultCategoryResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public Response<ResultCategoryResponseDto> TGetByFilter(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        try
        {
            Category? category = _categoryRepository.GetByFilter(predicate, include);
            _rules.CategoryExists(category);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(category!);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }

    }

    public async Task<Response<ResultCategoryResponseDto>> TGetByFilterAsync(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        try
        {
            Category? category = await _categoryRepository.GetByFilterAsync(predicate, include);
            _rules.CategoryExists(category);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(category!);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultCategoryResponseDto> TGetById(int id, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        try
        {
            Category? category = _categoryRepository.GetById(id, include);
            _rules.CategoryExists(category);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(category!);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public async Task<Response<ResultCategoryResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        try
        {
            Category? category = await _categoryRepository.GetByIdAsync(id, include);
            _rules.CategoryExists(category);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(category!);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultCategoryResponseDto> TUpdate(UpdateCategoryRequestDto updateCategoryRequestDto)
    {
        try
        {
            _rules.CategoryNoMustBeThreeCharacter(updateCategoryRequestDto.CategoryNo);
            _rules.CategoryNoMustBeUnique(updateCategoryRequestDto.CategoryNo, updateCategoryRequestDto.Id);
            _rules.CategoryNameCanNotBeNullOrWhiteSpace(updateCategoryRequestDto.Name);
            _rules.CategoryNameMustBeUnique(updateCategoryRequestDto.Name);
            Category updateCategory = UpdateCategoryRequestDto.ConvertToEntity(updateCategoryRequestDto);
            Category updatedCategory = _categoryRepository.Update(updateCategory);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(updatedCategory);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                Message = "Category updated successfully!",
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }

    }

    public async Task<Response<ResultCategoryResponseDto>> TUpdateAsync(UpdateCategoryRequestDto updateCategoryRequestDto)
    {
        try
        {
            _rules.CategoryNoMustBeThreeCharacter(updateCategoryRequestDto.CategoryNo);
            _rules.CategoryNoMustBeUnique(updateCategoryRequestDto.CategoryNo, updateCategoryRequestDto.Id);
            _rules.CategoryNameCanNotBeNullOrWhiteSpace(updateCategoryRequestDto.Name);
            _rules.CategoryNameMustBeUnique(updateCategoryRequestDto.Name);
            Category updateCategory = UpdateCategoryRequestDto.ConvertToEntity(updateCategoryRequestDto);
            Category updatedCategory = await _categoryRepository.UpdateAsync(updateCategory);
            ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(updatedCategory);
            return new Response<ResultCategoryResponseDto>()
            {
                Data = response,
                Message = "Category updated successfully!",
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCategoryResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}