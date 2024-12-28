using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using BusinessLogicLayer.Extensions;
using Core.Shared;
using DataAccessLayer.Repositories.CategoryRepositories;
using DataAccessLayer.Repositories.ProductRepositories;
using EntityLayer.Dtos.RequestDtos.ProductRequestDtos;
using EntityLayer.Dtos.ResponseDtos.CategoryResponseDtos;
using EntityLayer.Dtos.ResponseDtos.ProductResponseDtos;
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

public class ProductManager : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductRules _rules;

    public ProductManager(IProductRepository productRepository, IProductRules rules)
    {
        _productRepository = productRepository;
        _rules = rules;
    }

    public Response<ResultProductResponseDto> TCreate(CreateProductRequestDto createProductRequestDto)
    {
        try
        {
            _rules.CategoryExists(createProductRequestDto.CategoryId);
            int shortCode = GenerateClass.GenerateRandomUniqueShortCode(_rules, _rules.GetSelectedCategoryNo(createProductRequestDto.CategoryId));
            long barcodeNo = GenerateClass.GenerateRandomUniqueBarcodeNo(_rules, shortCode);
            _rules.NameCannotBeNullOrWhiteSpace(createProductRequestDto.Name);
            _rules.StockCannotBeNegative(createProductRequestDto.Stock);
            _rules.WeightCannotBeNegative(createProductRequestDto.Weight);
            _rules.ExpirationTimeCannotBeExpired(createProductRequestDto.Expiration);
            _rules.ProductionTimeCannotBeFuture(createProductRequestDto.Production);

            Product createProduct = CreateProductRequestDto.ConvertToEntity(createProductRequestDto, shortCode, barcodeNo);
            Product createdProduct = _productRepository.Create(createProduct);
            ResultProductResponseDto response = ResultProductResponseDto.ConvertToResponse(createdProduct);
            return new Response<ResultProductResponseDto>()
            {
                Data = response,
                Message = "Product created successfully!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }

    }

    public async Task<Response<ResultProductResponseDto>> TCreateAsync(CreateProductRequestDto createProductRequestDto)
    {
        try
        {
            _rules.CategoryExists(createProductRequestDto.CategoryId);
            int shortCode = GenerateClass.GenerateRandomUniqueShortCode(_rules, _rules.GetSelectedCategoryNo(createProductRequestDto.CategoryId));
            long barcodeNo = GenerateClass.GenerateRandomUniqueBarcodeNo(_rules, shortCode);
            _rules.NameCannotBeNullOrWhiteSpace(createProductRequestDto.Name);
            _rules.StockCannotBeNegative(createProductRequestDto.Stock);
            _rules.WeightCannotBeNegative(createProductRequestDto.Weight);
            _rules.ExpirationTimeCannotBeExpired(createProductRequestDto.Expiration);
            _rules.ProductionTimeCannotBeFuture(createProductRequestDto.Production);
            Product createProduct = CreateProductRequestDto.ConvertToEntity(createProductRequestDto, shortCode, barcodeNo);
            Product createdProduct = await _productRepository.CreateAsync(createProduct);
            ResultProductResponseDto response = ResultProductResponseDto.ConvertToResponse(createdProduct);
            return new Response<ResultProductResponseDto>()
            {
                Data = response,
                Message = "Product created successfully!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }
    }

    public Response<ResultProductResponseDto> TDeleteByBarcodeNo(long barcodeNo)
    {
        try
        {
            Product? product = _productRepository.GetByFilter(x => x.BarcodeNo == barcodeNo, x => x.Include(x => x.Category));
            _rules.ProductExists(product);
            product!.Deleted = DateTime.Now;
            _productRepository.Delete(product);
            return new Response<ResultProductResponseDto>()
            {
                Message = "Product deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public async Task<Response<ResultProductResponseDto>> TDeleteByBarcodeNoAsync(long barcodeNo)
    {
        try
        {
            Product? product = await _productRepository.GetByFilterAsync(x => x.BarcodeNo == barcodeNo, x => x.Include(x => x.Category));
            _rules.ProductExists(product);
            product!.Deleted = DateTime.Now;
            await _productRepository.DeleteAsync(product);
            return new Response<ResultProductResponseDto>()
            {
                Message = "Product deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public Response<ResultProductResponseDto> TDeleteById(int id)
    {
        try
        {
            Product? product = _productRepository.GetByFilter(x => x.Id == id, x => x.Include(x => x.Category));
            _rules.ProductExists(product);
            product!.Deleted = DateTime.Now;
            _productRepository.Delete(product);
            return new Response<ResultProductResponseDto>()
            {
                Message = "Product deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public async Task<Response<ResultProductResponseDto>> TDeleteByIdAsync(int id)
    {
        try
        {
            Product? product = await _productRepository.GetByFilterAsync(x => x.Id == id, x => x.Include(x => x.Category));
            _rules.ProductExists(product);
            product!.Deleted = DateTime.Now;
            await _productRepository.DeleteAsync(product);
            return new Response<ResultProductResponseDto>()
            {
                Message = "Product deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public Response<ResultProductResponseDto> TDeleteByShortCode(int shortCode)
    {
        try
        {
            Product? product = _productRepository.GetByFilter(x => x.ShortCode == shortCode, x => x.Include(x => x.Category));
            _rules.ProductExists(product);
            product!.Deleted = DateTime.Now;
            _productRepository.Delete(product);
            return new Response<ResultProductResponseDto>()
            {
                Message = "Product deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public async Task<Response<ResultProductResponseDto>> TDeleteByShortCodeAsync(int shortCode)
    {
        try
        {
            Product? product = await _productRepository.GetByFilterAsync(x => x.ShortCode == shortCode, x => x.Include(x => x.Category));
            _rules.ProductExists(product);
            product!.Deleted = DateTime.Now;
            await _productRepository.DeleteAsync(product);
            return new Response<ResultProductResponseDto>()
            {
                Message = "Product deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }

    }

    public Response<ResultProductResponseDto> TDeleteFromDatabaseByBarcodeNo(long barcodeNo)
    {
        try
        {
            Product? product = _productRepository.GetByFilter(x => x.BarcodeNo == barcodeNo, x => x.Include(x => x.Category));
            _rules.ProductExists(product, isDeleteFromDatabase: true);
            _productRepository.DeleteFromDatabase(product!);
            return new Response<ResultProductResponseDto>()
            {
                Message = "Product deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public async Task<Response<ResultProductResponseDto>> TDeleteFromDatabaseByBarcodeNoAsync(long barcodeNo)
    {
        try
        {
            Product? product = await _productRepository.GetByFilterAsync(x => x.BarcodeNo == barcodeNo, x => x.Include(x => x.Category));
            _rules.ProductExists(product, isDeleteFromDatabase: true);
            await _productRepository.DeleteFromDatabaseAsync(product!);
            return new Response<ResultProductResponseDto>()
            {
                Message = "Product deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }

    }

    public Response<ResultProductResponseDto> TDeleteFromDatabaseById(int id)
    {
        try
        {
            Product? product = _productRepository.GetByFilter(x => x.Id == id, x => x.Include(x => x.Category));
            _rules.ProductExists(product, true);
            _productRepository.DeleteFromDatabase(product!);
            return new Response<ResultProductResponseDto>()
            {
                Message = "Product deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public async Task<Response<ResultProductResponseDto>> TDeleteFromDatabaseByIdAsync(int id)
    {
        try
        {
            Product? product = await _productRepository.GetByFilterAsync(x => x.Id == id, x => x.Include(x => x.Category));
            _rules.ProductExists(product, true);
            await _productRepository.DeleteFromDatabaseAsync(product!);
            return new Response<ResultProductResponseDto>()
            {
                Message = "Product deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public Response<ResultProductResponseDto> TDeleteFromDatabaseByShortCode(int shortCode)
    {
        try
        {
            Product? product = _productRepository.GetByFilter(x => x.ShortCode == shortCode, x => x.Include(x => x.Category));
            _rules.ProductExists(product, true);
            _productRepository.DeleteFromDatabase(product!);
            return new Response<ResultProductResponseDto>()
            {
                Message = "Product deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public async Task<Response<ResultProductResponseDto>> TDeleteFromDatabaseByShortCodeAsync(int shortCode)
    {
        try
        {
            Product? product = await _productRepository.GetByFilterAsync(x => x.ShortCode == shortCode, x => x.Include(x => x.Category));
            _rules.ProductExists(product, true);
            await _productRepository.DeleteFromDatabaseAsync(product!);
            return new Response<ResultProductResponseDto>()
            {
                Message = "Product deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public Response<List<ResultProductResponseDto>> TGetAll(Expression<Func<Product, bool>>? predicate = null, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        List<Product> products = _productRepository.GetAll(x => x.Deleted == null, x => x.Include(x => x.Category));
        List<ResultProductResponseDto> response = products.Select(x => ResultProductResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultProductResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<List<ResultProductResponseDto>>> TGetAllAsync(Expression<Func<Product, bool>>? predicate = null, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        List<Product> products = await _productRepository.GetAllAsync(x => x.Deleted == null, x => x.Include(x => x.Category));
        List<ResultProductResponseDto> response = products.Select(x => ResultProductResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultProductResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ResultProductResponseDto> TGetByFilter(Expression<Func<Product, bool>> predicate, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        try
        {
            Product? product = _productRepository.GetByFilter(predicate, include);
            _rules.ProductExists(product);
            ResultProductResponseDto response = ResultProductResponseDto.ConvertToResponse(product!);
            return new Response<ResultProductResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public async Task<Response<ResultProductResponseDto>> TGetByFilterAsync(Expression<Func<Product, bool>> predicate, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        try
        {
            Product? product = await _productRepository.GetByFilterAsync(predicate, include);
            _rules.ProductExists(product);
            ResultProductResponseDto response = ResultProductResponseDto.ConvertToResponse(product!);
            return new Response<ResultProductResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public Response<ResultProductResponseDto> TGetById(int id, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        try
        {
            Product? product = _productRepository.GetById(id, include);
            _rules.ProductExists(product);
            ResultProductResponseDto response = ResultProductResponseDto.ConvertToResponse(product!);
            return new Response<ResultProductResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public async Task<Response<ResultProductResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        try
        {
            Product? product = await _productRepository.GetByIdAsync(id, include);
            _rules.ProductExists(product);
            ResultProductResponseDto response = ResultProductResponseDto.ConvertToResponse(product!);
            return new Response<ResultProductResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
            };
        }
    }

    public Response<ResultProductResponseDto> TUpdate(UpdateProductRequestDto updateProductRequestDto)
    {
        try
        {
            _rules.CategoryExists(updateProductRequestDto.CategoryId);
            _rules.BarcodeNoMustBeFourteenCharacter(updateProductRequestDto.BarcodeNo);
            _rules.BarcodeNoMustBeUnique(updateProductRequestDto.BarcodeNo, updateProductRequestDto.Id);
            _rules.ShortCodeMustBeSevenCharacter(updateProductRequestDto.ShortCode);
            _rules.ShortCodeMustBeUnique(updateProductRequestDto.ShortCode, updateProductRequestDto.Id);
            _rules.NameCannotBeNullOrWhiteSpace(updateProductRequestDto.Name);
            _rules.StockCannotBeNegative(updateProductRequestDto.Stock);
            _rules.WeightCannotBeNegative(updateProductRequestDto.Weight);
            _rules.ExpirationTimeCannotBeExpired(updateProductRequestDto.Expiration);
            _rules.ProductionTimeCannotBeFuture(updateProductRequestDto.Production);
            Product updateProduct = UpdateProductRequestDto.ConvertToEntity(updateProductRequestDto);
            Product updatedProduct = _productRepository.Update(updateProduct);
            ResultProductResponseDto response = ResultProductResponseDto.ConvertToResponse(updatedProduct);
            return new Response<ResultProductResponseDto>()
            {
                Data = response,
                Message = "Product updated successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }
    }

    public async Task<Response<ResultProductResponseDto>> TUpdateAsync(UpdateProductRequestDto updateProductRequestDto)
    {
        try
        {
            _rules.CategoryExists(updateProductRequestDto.CategoryId);
            _rules.BarcodeNoMustBeFourteenCharacter(updateProductRequestDto.BarcodeNo);
            _rules.BarcodeNoMustBeUnique(updateProductRequestDto.BarcodeNo, updateProductRequestDto.Id);
            _rules.ShortCodeMustBeSevenCharacter(updateProductRequestDto.ShortCode);
            _rules.ShortCodeMustBeUnique(updateProductRequestDto.ShortCode, updateProductRequestDto.Id);
            _rules.NameCannotBeNullOrWhiteSpace(updateProductRequestDto.Name);
            _rules.StockCannotBeNegative(updateProductRequestDto.Stock);
            _rules.WeightCannotBeNegative(updateProductRequestDto.Weight);
            _rules.ExpirationTimeCannotBeExpired(updateProductRequestDto.Expiration);
            _rules.ProductionTimeCannotBeFuture(updateProductRequestDto.Production);
            Product updateProduct = UpdateProductRequestDto.ConvertToEntity(updateProductRequestDto);
            Product updatedProduct = await _productRepository.UpdateAsync(updateProduct);
            ResultProductResponseDto response = ResultProductResponseDto.ConvertToResponse(updatedProduct);
            return new Response<ResultProductResponseDto>()
            {
                Data = response,
                Message = "Product updated successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultProductResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }

    }
}
