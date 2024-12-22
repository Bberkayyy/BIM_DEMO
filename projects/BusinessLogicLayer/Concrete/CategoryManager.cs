using BusinessLogicLayer.Abstract;
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

    public CategoryManager(ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public Response<ResultCategoryResponseDto> TCreate(CreateCategoryRequestDto createCategoryRequestDto)
    {
        Category createCategory = CreateCategoryRequestDto.ConvertToEntity(createCategoryRequestDto);
        Category createdCategory = _categoryRepository.Create(createCategory);
        ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(createdCategory);
        return new Response<ResultCategoryResponseDto>()
        {
            Data = response,
            Message = "Category created successfully!",
            StatusCode = System.Net.HttpStatusCode.Created
        };
    }

    public async Task<Response<ResultCategoryResponseDto>> TCreateAsync(CreateCategoryRequestDto createCategoryRequestDto)
    {
        Category createCategory = CreateCategoryRequestDto.ConvertToEntity(createCategoryRequestDto);
        Category createdCategory = await _categoryRepository.CreateAsync(createCategory);
        ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(createdCategory);
        return new Response<ResultCategoryResponseDto>()
        {
            Data = response,
            Message = "Category created successfully!",
            StatusCode = System.Net.HttpStatusCode.Created
        };
    }

    public Response<ResultCategoryResponseDto> TDeleteByCategoryNo(short categoryNo)
    {
        Category? category = _categoryRepository.GetByFilter(x => x.CategoryNo == categoryNo, include: x => x.Include(x => x.Products));
        if (category != null)
        {
            foreach (var product in category.Products)
            {
                product.Deleted = DateTime.Now;
                _productRepository.Delete(product);
            }
            _categoryRepository.Delete(category);
        }
        return new Response<ResultCategoryResponseDto>()
        {
            Data = category != null ? ResultCategoryResponseDto.ConvertToResponse(category) : null,
            Message = category != null ? $"Category deleted successfully! ({category.Products.Count} product is affected)" : "Category not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultCategoryResponseDto>> TDeleteByCategoryNoAsync(short categoryNo)
    {
        Category? category = await _categoryRepository.GetByFilterAsync(x => x.CategoryNo == categoryNo, include: x => x.Include(x => x.Products));
        if (category != null)
        {
            foreach (var product in category.Products)
            {
                product.Deleted = DateTime.Now;
                await _productRepository.DeleteAsync(product);
            }
            await _categoryRepository.DeleteAsync(category);
        }
        return new Response<ResultCategoryResponseDto>()
        {
            Data = category != null ? ResultCategoryResponseDto.ConvertToResponse(category) : null,
            Message = category != null ? $"Category deleted successfully! ({category.Products.Count} product is affected)" : "Category not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultCategoryResponseDto> TDeleteById(int id)
    {
        Category? category = _categoryRepository.GetByFilter(x => x.Id == id, include: x => x.Include(x => x.Products));
        if (category != null)
        {
            foreach (var product in category.Products)
            {
                product.Deleted = DateTime.Now;
                _productRepository.Delete(product);
            }
            _categoryRepository.Delete(category);
        }
        return new Response<ResultCategoryResponseDto>()
        {
            Data = category != null ? ResultCategoryResponseDto.ConvertToResponse(category) : null,
            Message = category != null ? $"Category deleted successfully! ({category.Products.Count} product is affected)" : "Category not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultCategoryResponseDto>> TDeleteByIdAsync(int id)
    {
        Category? category = await _categoryRepository.GetByFilterAsync(x => x.Id == id, include: x => x.Include(x => x.Products));
        if (category != null)
        {
            foreach (var product in category.Products)
            {
                product.Deleted = DateTime.Now;
                await _productRepository.DeleteAsync(product);
            }
            await _categoryRepository.DeleteAsync(category);
        }
        return new Response<ResultCategoryResponseDto>()
        {
            Data = category != null ? ResultCategoryResponseDto.ConvertToResponse(category) : null,
            Message = category != null ? $"Category deleted successfully! ({category.Products.Count} product is affected)" : "Category not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultCategoryResponseDto> TDeleteFromDatabaseByCategoryNo(short categoryNo)
    {
        Category? category = _categoryRepository.GetByFilter(x => x.CategoryNo == categoryNo);
        if (category != null)
            _categoryRepository.DeleteFromDatabase(category);
        return new Response<ResultCategoryResponseDto>()
        {
            Data = category != null ? ResultCategoryResponseDto.ConvertToResponse(category) : null,
            Message = category != null ? "Category deleted successfully!" : "Category not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultCategoryResponseDto>> TDeleteFromDatabaseByCategoryNoAsync(short categoryNo)
    {
        Category? category = await _categoryRepository.GetByFilterAsync(x => x.CategoryNo == categoryNo);
        if (category != null)
            await _categoryRepository.DeleteFromDatabaseAsync(category);
        return new Response<ResultCategoryResponseDto>()
        {
            Data = category != null ? ResultCategoryResponseDto.ConvertToResponse(category) : null,
            Message = category != null ? "Category deleted successfully!" : "Category not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultCategoryResponseDto> TDeleteFromDatabaseById(int id)
    {
        Category? category = _categoryRepository.GetByFilter(x => x.Id == id);
        if (category != null)
            _categoryRepository.DeleteFromDatabase(category);
        return new Response<ResultCategoryResponseDto>()
        {
            Data = category != null ? ResultCategoryResponseDto.ConvertToResponse(category) : null,
            Message = category != null ? "Category deleted successfully!" : "Category not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultCategoryResponseDto>> TDeleteFromDatabaseByIdAsync(int id)
    {
        Category? category = await _categoryRepository.GetByFilterAsync(x => x.Id == id);
        if (category != null)
            await _categoryRepository.DeleteFromDatabaseAsync(category);
        return new Response<ResultCategoryResponseDto>()
        {
            Data = category != null ? ResultCategoryResponseDto.ConvertToResponse(category) : null,
            Message = category != null ? "Category deleted successfully!" : "Category not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<List<ResultCategoryResponseDto>> TGetAll(Expression<Func<Category, bool>>? predicate = null, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        List<Category> categories = _categoryRepository.GetAll(x => x.Deleted == null, x => x.Include(x => x.Products));
        List<ResultCategoryResponseDto> response = categories.Select(x => ResultCategoryResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultCategoryResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<Response<List<ResultCategoryResponseDto>>> TGetAllAsync(Expression<Func<Category, bool>>? predicate = null, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        List<Category> categories = await _categoryRepository.GetAllAsync(x => x.Deleted == null, include: x => x.Include(x => x.Products));
        List<ResultCategoryResponseDto> response = categories.Select(x => ResultCategoryResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultCategoryResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public Response<ResultCategoryResponseDto> TGetByFilter(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        Category? category = _categoryRepository.GetByFilter(predicate, include);
        return new Response<ResultCategoryResponseDto>()
        {
            Data = category != null ? ResultCategoryResponseDto.ConvertToResponse(category) : null,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<Response<ResultCategoryResponseDto>> TGetByFilterAsync(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        Category? category = await _categoryRepository.GetByFilterAsync(predicate, include);
        return new Response<ResultCategoryResponseDto>()
        {
            Data = category != null ? ResultCategoryResponseDto.ConvertToResponse(category) : null,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public Response<ResultCategoryResponseDto> TGetById(int id, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        Category? category = _categoryRepository.GetById(id, include);
        return new Response<ResultCategoryResponseDto>()
        {
            Data = category != null ? ResultCategoryResponseDto.ConvertToResponse(category) : null,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<Response<ResultCategoryResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        Category? category = await _categoryRepository.GetByIdAsync(id, include);
        return new Response<ResultCategoryResponseDto>()
        {
            Data = category != null ? ResultCategoryResponseDto.ConvertToResponse(category) : null,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public Response<ResultCategoryResponseDto> TUpdate(UpdateCategoryRequestDto updateCategoryRequestDto)
    {
        Category updateCategory = UpdateCategoryRequestDto.ConvertToEntity(updateCategoryRequestDto);
        Category updatedCategory = _categoryRepository.Update(updateCategory);
        ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(updatedCategory);
        return new Response<ResultCategoryResponseDto>()
        {
            Data = response,
            Message = "Category updated successfully!",
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<ResultCategoryResponseDto>> TUpdateAsync(UpdateCategoryRequestDto updateCategoryRequestDto)
    {
        Category updateCategory = UpdateCategoryRequestDto.ConvertToEntity(updateCategoryRequestDto);
        Category updatedCategory = await _categoryRepository.UpdateAsync(updateCategory);
        ResultCategoryResponseDto response = ResultCategoryResponseDto.ConvertToResponse(updatedCategory);
        return new Response<ResultCategoryResponseDto>()
        {
            Data = response,
            Message = "Category updated successfully!",
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }
}