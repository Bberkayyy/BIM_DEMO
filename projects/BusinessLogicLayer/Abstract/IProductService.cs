using EntityLayer.Dtos.RequestDtos.ProductRequestDtos;
using EntityLayer.Dtos.RequestDtos;
using EntityLayer.Dtos.ResponseDtos.ProductResponseDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Shared;

namespace BusinessLogicLayer.Abstract;

public interface IProductService
{
    Response<ResultProductResponseDto> TCreate(CreateProductRequestDto createProductRequestDto);
    Response<ResultProductResponseDto> TUpdate(UpdateProductRequestDto updateProductRequestDto);
    Response<ResultProductResponseDto> TDeleteFromDatabaseById(int id);
    Response<ResultProductResponseDto> TDeleteFromDatabaseByShortCode(int shortCode);
    Response<ResultProductResponseDto> TDeleteFromDatabaseByBarcodeNo(long barcodeNo);
    Response<ResultProductResponseDto> TDeleteById(int id);
    Response<ResultProductResponseDto> TDeleteByShortCode(int shortCode);
    Response<ResultProductResponseDto> TDeleteByBarcodeNo(long barcodeNo);
    Response<List<ResultProductResponseDto>> TGetAll(Expression<Func<Product, bool>>? predicate = null, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null);
    Response<ResultProductResponseDto> TGetById(int id, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null);
    Response<ResultProductResponseDto> TGetByFilter(Expression<Func<Product, bool>> predicate, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null);

    Task<Response<ResultProductResponseDto>> TCreateAsync(CreateProductRequestDto createProductRequestDto);
    Task<Response<ResultProductResponseDto>> TUpdateAsync(UpdateProductRequestDto updateProductRequestDto);
    Task<Response<ResultProductResponseDto>> TDeleteFromDatabaseByIdAsync(int id);
    Task<Response<ResultProductResponseDto>> TDeleteFromDatabaseByShortCodeAsync(int shortCode);
    Task<Response<ResultProductResponseDto>> TDeleteFromDatabaseByBarcodeNoAsync(long barcodeNo);
    Task<Response<ResultProductResponseDto>> TDeleteByIdAsync(int id);
    Task<Response<ResultProductResponseDto>> TDeleteByShortCodeAsync(int shortCode);
    Task<Response<ResultProductResponseDto>> TDeleteByBarcodeNoAsync(long barcodeNo);
    Task<Response<List<ResultProductResponseDto>>> TGetAllAsync(Expression<Func<Product, bool>>? predicate = null, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null);
    Task<Response<ResultProductResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null);
    Task<Response<ResultProductResponseDto>> TGetByFilterAsync(Expression<Func<Product, bool>> predicate, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null);
}
