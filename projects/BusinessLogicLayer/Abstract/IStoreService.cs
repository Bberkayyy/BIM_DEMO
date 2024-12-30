using EntityLayer.Dtos.RequestDtos.StoreRequestDtos;
using EntityLayer.Dtos.RequestDtos;
using EntityLayer.Dtos.ResponseDtos.StoreResponseDtos;
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

public interface IStoreService
{
    Response<ResultStoreResponseDto> TCreate(CreateStoreRequestDto createStoreRequestDto);
    Response<ResultStoreResponseDto> TUpdate(UpdateStoreRequestDto updateStoreRequestDto);
    Response<ResultStoreResponseDto> TDeleteFromDatabaseById(int id);
    Response<ResultStoreResponseDto> TDeleteFromDatabaseByStoreNo(string storeNo);
    Response<ResultStoreResponseDto> TDeleteById(int id);
    Response<ResultStoreResponseDto> TDeleteByStoreNo(string storeNo);
    Response<List<ResultStoreResponseDto>> TGetAll(Expression<Func<Store, bool>>? predicate = null, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null);
    Response<ResultStoreResponseDto> TGetById(int id, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null);
    Response<ResultStoreResponseDto> TGetByFilter(Expression<Func<Store, bool>> predicate, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null);

    Task<Response<ResultStoreResponseDto>> TCreateAsync(CreateStoreRequestDto createStoreRequestDto);
    Task<Response<ResultStoreResponseDto>> TUpdateAsync(UpdateStoreRequestDto updateStoreRequestDto);
    Task<Response<ResultStoreResponseDto>> TDeleteFromDatabaseByIdAsync(int id);
    Task<Response<ResultStoreResponseDto>> TDeleteFromDatabaseByStoreNoAsync(string storeNo);
    Task<Response<ResultStoreResponseDto>> TDeleteByIdAsync(int id);
    Task<Response<ResultStoreResponseDto>> TDeleteByStoreNoAsync(string storeNo);
    Task<Response<List<ResultStoreResponseDto>>> TGetAllAsync(Expression<Func<Store, bool>>? predicate = null, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null);
    Task<Response<ResultStoreResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null);
    Task<Response<ResultStoreResponseDto>> TGetByFilterAsync(Expression<Func<Store, bool>> predicate, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null);
}
