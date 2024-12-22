using Core.Shared;
using EntityLayer.Dtos.RequestDtos;
using EntityLayer.Dtos.RequestDtos.CategoryRequestDtos;
using EntityLayer.Dtos.ResponseDtos.CategoryResponseDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstract;

public interface ICategoryService
{
    Response<ResultCategoryResponseDto> TCreate(CreateCategoryRequestDto createCategoryRequestDto);
    Response<ResultCategoryResponseDto> TUpdate(UpdateCategoryRequestDto updateCategoryRequestDto);
    Response<ResultCategoryResponseDto> TDeleteFromDatabaseById(int id);
    Response<ResultCategoryResponseDto> TDeleteFromDatabaseByCategoryNo(short categoryNo);
    Response<ResultCategoryResponseDto> TDeleteById(int id);
    Response<ResultCategoryResponseDto> TDeleteByCategoryNo(short categoryNo);
    Response<List<ResultCategoryResponseDto>> TGetAll(Expression<Func<Category, bool>>? predicate = null, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null);
    Response<ResultCategoryResponseDto> TGetById(int id, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null);
    Response<ResultCategoryResponseDto> TGetByFilter(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null);

    Task<Response<ResultCategoryResponseDto>> TCreateAsync(CreateCategoryRequestDto createCategoryRequestDto);
    Task<Response<ResultCategoryResponseDto>> TUpdateAsync(UpdateCategoryRequestDto updateCategoryRequestDto);
    Task<Response<ResultCategoryResponseDto>> TDeleteFromDatabaseByIdAsync(int id);
    Task<Response<ResultCategoryResponseDto>> TDeleteFromDatabaseByCategoryNoAsync(short categoryNo);
    Task<Response<ResultCategoryResponseDto>> TDeleteByIdAsync(int id);
    Task<Response<ResultCategoryResponseDto>> TDeleteByCategoryNoAsync(short categoryNo);
    Task<Response<List<ResultCategoryResponseDto>>> TGetAllAsync(Expression<Func<Category, bool>>? predicate = null, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null);
    Task<Response<ResultCategoryResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null);
    Task<Response<ResultCategoryResponseDto>> TGetByFilterAsync(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null);
}
