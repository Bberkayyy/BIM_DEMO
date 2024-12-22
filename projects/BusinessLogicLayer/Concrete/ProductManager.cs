using BusinessLogicLayer.Abstract;
using Core.Shared;
using DataAccessLayer.Repositories.ProductRepositories;
using EntityLayer.Dtos.RequestDtos.ProductRequestDtos;
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

    public ProductManager(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Response<ResultProductResponseDto> TCreate(CreateProductRequestDto createProductRequestDto)
    {
        Product createProduct = CreateProductRequestDto.ConvertToEntity(createProductRequestDto);
        Product createdProduct = _productRepository.Create(createProduct);
        ResultProductResponseDto response = ResultProductResponseDto.ConvertToResponse(createdProduct);
        return new Response<ResultProductResponseDto>()
        {
            Data = response,
            Message = "Product created successfully!",
            StatusCode = System.Net.HttpStatusCode.Created
        };
    }

    public async Task<Response<ResultProductResponseDto>> TCreateAsync(CreateProductRequestDto createProductRequestDto)
    {
        Product createProduct = CreateProductRequestDto.ConvertToEntity(createProductRequestDto);
        Product createdProduct = await _productRepository.CreateAsync(createProduct);
        ResultProductResponseDto response = ResultProductResponseDto.ConvertToResponse(createdProduct);
        return new Response<ResultProductResponseDto>()
        {
            Data = response,
            Message = "Product created successfully!",
            StatusCode = System.Net.HttpStatusCode.Created
        };
    }

    public Response<ResultProductResponseDto> TDeleteByBarcodeNo(long barcodeNo)
    {
        Product? product = _productRepository.GetByFilter(x => x.BarcodeNo == barcodeNo, x => x.Include(x => x.Category));
        if (product != null)
        {
            product.Deleted = DateTime.Now;
            _productRepository.Delete(product);
        }
        return new Response<ResultProductResponseDto>()
        {
            Message = product != null ? "Product deleted successfully!" : "Product not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultProductResponseDto>> TDeleteByBarcodeNoAsync(long barcodeNo)
    {
        Product? product = await _productRepository.GetByFilterAsync(x => x.BarcodeNo == barcodeNo, x => x.Include(x => x.Category));
        if (product != null)
        {
            product.Deleted = DateTime.Now;
            await _productRepository.DeleteAsync(product);
        }
        return new Response<ResultProductResponseDto>()
        {
            Message = product != null ? "Product deleted successfully!" : "Product not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultProductResponseDto> TDeleteById(int id)
    {
        Product? product = _productRepository.GetByFilter(x => x.Id == id, x => x.Include(x => x.Category));
        if (product != null)
        {
            product.Deleted = DateTime.Now;
            _productRepository.Delete(product);
        }
        return new Response<ResultProductResponseDto>()
        {
            Message = product != null ? "Product deleted successfully!" : "Product not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultProductResponseDto>> TDeleteByIdAsync(int id)
    {
        Product? product = await _productRepository.GetByFilterAsync(x => x.Id == id, x => x.Include(x => x.Category));
        if (product != null)
        {
            product.Deleted = DateTime.Now;
            await _productRepository.DeleteAsync(product);
        }
        return new Response<ResultProductResponseDto>()
        {
            Message = product != null ? "Product deleted successfully!" : "Product not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultProductResponseDto> TDeleteByShortCode(int shortCode)
    {
        Product? product = _productRepository.GetByFilter(x => x.ShortCode == shortCode, x => x.Include(x => x.Category));
        if (product != null)
        {
            product.Deleted = DateTime.Now;
            _productRepository.Delete(product);
        }
        return new Response<ResultProductResponseDto>()
        {
            Message = product != null ? "Product deleted successfully!" : "Product not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultProductResponseDto>> TDeleteByShortCodeAsync(int shortCode)
    {
        Product? product = await _productRepository.GetByFilterAsync(x => x.ShortCode == shortCode, x => x.Include(x => x.Category));
        if (product != null)
        {
            product.Deleted = DateTime.Now;
            await _productRepository.DeleteAsync(product);
        }
        return new Response<ResultProductResponseDto>()
        {
            Message = product != null ? "Product deleted successfully!" : "Product not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultProductResponseDto> TDeleteFromDatabaseByBarcodeNo(long barcodeNo)
    {
        Product? product = _productRepository.GetByFilter(x => x.BarcodeNo == barcodeNo, x => x.Include(x => x.Category));
        if (product != null)
            _productRepository.DeleteFromDatabase(product);
        return new Response<ResultProductResponseDto>()
        {
            Message = product != null ? "Product deleted successfully!" : "Product not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultProductResponseDto>> TDeleteFromDatabaseByBarcodeNoAsync(long barcodeNo)
    {
        Product? product = await _productRepository.GetByFilterAsync(x => x.BarcodeNo == barcodeNo, x => x.Include(x => x.Category));
        if (product != null)
            await _productRepository.DeleteFromDatabaseAsync(product);
        return new Response<ResultProductResponseDto>()
        {
            Message = product != null ? "Product deleted successfully!" : "Product not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultProductResponseDto> TDeleteFromDatabaseById(int id)
    {
        Product? product = _productRepository.GetByFilter(x => x.Id == id, x => x.Include(x => x.Category));
        if (product != null)
            _productRepository.DeleteFromDatabase(product);
        return new Response<ResultProductResponseDto>()
        {
            Message = product != null ? "Product deleted successfully!" : "Product not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultProductResponseDto>> TDeleteFromDatabaseByIdAsync(int id)
    {
        Product? product = await _productRepository.GetByFilterAsync(x => x.Id == id, x => x.Include(x => x.Category));
        if (product != null)
            await _productRepository.DeleteFromDatabaseAsync(product);
        return new Response<ResultProductResponseDto>()
        {
            Message = product != null ? "Product deleted successfully!" : "Product not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultProductResponseDto> TDeleteFromDatabaseByShortCode(int shortCode)
    {
        Product? product = _productRepository.GetByFilter(x => x.ShortCode == shortCode, x => x.Include(x => x.Category));
        if (product != null)
            _productRepository.DeleteFromDatabase(product);
        return new Response<ResultProductResponseDto>()
        {
            Message = product != null ? "Product deleted successfully!" : "Product not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultProductResponseDto>> TDeleteFromDatabaseByShortCodeAsync(int shortCode)
    {
        Product? product = await _productRepository.GetByFilterAsync(x => x.ShortCode == shortCode, x => x.Include(x => x.Category));
        if (product != null)
            await _productRepository.DeleteFromDatabaseAsync(product);
        return new Response<ResultProductResponseDto>()
        {
            Message = product != null ? "Product deleted successfully!" : "Product not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
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
        Product? product = _productRepository.GetByFilter(predicate, include);
        return new Response<ResultProductResponseDto>()
        {
            Data = product != null ? ResultProductResponseDto.ConvertToResponse(product) : null,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<ResultProductResponseDto>> TGetByFilterAsync(Expression<Func<Product, bool>> predicate, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        Product? product = await _productRepository.GetByFilterAsync(predicate, include);
        return new Response<ResultProductResponseDto>()
        {
            Data = product != null ? ResultProductResponseDto.ConvertToResponse(product) : null,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ResultProductResponseDto> TGetById(int id, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        Product? product = _productRepository.GetById(id, include);
        return new Response<ResultProductResponseDto>()
        {
            Data = product != null ? ResultProductResponseDto.ConvertToResponse(product) : null,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<ResultProductResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        Product? product = await _productRepository.GetByIdAsync(id, include);
        return new Response<ResultProductResponseDto>()
        {
            Data = product != null ? ResultProductResponseDto.ConvertToResponse(product) : null,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ResultProductResponseDto> TUpdate(UpdateProductRequestDto updateProductRequestDto)
    {
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

    public async Task<Response<ResultProductResponseDto>> TUpdateAsync(UpdateProductRequestDto updateProductRequestDto)
    {
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
}
