using Core.Shared;
using EntityLayer.Dtos.RequestDtos.BasketItemsRequestDtos;
using EntityLayer.Dtos.RequestDtos.BasketRequestDtos;
using EntityLayer.Dtos.ResponseDtos.BasketItemResponseDtos;
using EntityLayer.Dtos.ResponseDtos.BasketResponseDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstract;

public interface IBasketService
{
    Task<Response<ResultBasketResponseDto>> TSaveBasketAsync(CreateBasketRequestDto createBasketRequestDto);

    Response<List<ResultBasketResponseDto>> TGetAll(Expression<Func<Basket, bool>>? predicate = null, Func<IQueryable<Basket>, IIncludableQueryable<Basket, object>>? include = null);
    Response<ResultBasketResponseDto> TDeleteFromDatabase(int id);

    Task<Response<List<ResultBasketResponseDto>>> TGetAllAsync(Expression<Func<Basket, bool>>? predicate = null, Func<IQueryable<Basket>, IIncludableQueryable<Basket, object>>? include = null);
    Task<Response<ResultBasketResponseDto>> TDeleteFromDatabaseAsync(int id);

}
