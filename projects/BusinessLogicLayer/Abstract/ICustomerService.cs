using Core.Shared;
using EntityLayer.Dtos.RequestDtos.CustomerRequestDtos;
using EntityLayer.Dtos.ResponseDtos.CustomerResponseDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstract;

public interface ICustomerService
{
    Response<ResultCustomerResponseDto> TCreate(CreateCustomerRequestDto createCustomerRequestDto);
    Response<List<ResultCustomerResponseDto>> TGetAll(Expression<Func<Customer, bool>>? predicate = null, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null);
    Response<ResultCustomerResponseDto> TGetById(int id, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null);
    Response<ResultCustomerResponseDto> TGetByFilter(Expression<Func<Customer, bool>> predicate, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null);

    Task<Response<ResultCustomerResponseDto>> TCreateAsync(CreateCustomerRequestDto createCustomerRequestDto);
    Task<Response<List<ResultCustomerResponseDto>>> TGetAllAsync(Expression<Func<Customer, bool>>? predicate = null, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null);
    Task<Response<ResultCustomerResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null);
    Task<Response<ResultCustomerResponseDto>> TGetByFilterAsync(Expression<Func<Customer, bool>> predicate, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null);
}
