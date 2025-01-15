using Core.Shared;
using EntityLayer.Dtos.RequestDtos.GiveBackListRequestDtos;
using EntityLayer.Dtos.ResponseDtos.GiveBackListResponseDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstract;

public interface IGiveBackListService
{
    Response<ResultGiveBackListResponseDto> TCreate(CreateGiveBackListRequestDto createGiveBackListRequestDto);
    Response<List<ResultGiveBackListResponseDto>> TGetAll(Expression<Func<GiveBackList, bool>>? predicate = null, Func<IQueryable<GiveBackList>, IIncludableQueryable<GiveBackList, object>>? include = null);
    Response<ResultGiveBackListResponseDto> TGetById(int id, Func<IQueryable<GiveBackList>, IIncludableQueryable<GiveBackList, object>>? include = null);
    Response<ResultGiveBackListResponseDto> TGetByFilter(Expression<Func<GiveBackList, bool>> predicate, Func<IQueryable<GiveBackList>, IIncludableQueryable<GiveBackList, object>>? include = null);

    Task<Response<ResultGiveBackListResponseDto>> TCreateAsync(CreateGiveBackListRequestDto createGiveBackListRequestDto);
    Task<Response<List<ResultGiveBackListResponseDto>>> TGetAllAsync(Expression<Func<GiveBackList, bool>>? predicate = null, Func<IQueryable<GiveBackList>, IIncludableQueryable<GiveBackList, object>>? include = null);
    Task<Response<ResultGiveBackListResponseDto>> TGetByIdAsync(int id, Func<IQueryable<GiveBackList>, IIncludableQueryable<GiveBackList, object>>? include = null);
    Task<Response<ResultGiveBackListResponseDto>> TGetByFilterAsync(Expression<Func<GiveBackList, bool>> predicate, Func<IQueryable<GiveBackList>, IIncludableQueryable<GiveBackList, object>>? include = null);
}
