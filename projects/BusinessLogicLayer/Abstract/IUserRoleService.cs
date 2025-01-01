using Core.Shared;
using EntityLayer.Dtos.RequestDtos.UserRoleRequestDtos;
using EntityLayer.Dtos.ResponseDtos.UserRoleResponseDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstract;

public interface IUserRoleService
{
    Response<ResultUserRoleResponseDto> TCreate(CreateUserRoleRequestDto createUserRoleRequestDto);
    Response<ResultUserRoleResponseDto> TUpdate(UpdateUserRoleRequestDto updateUserRoleRequestDto);
    Response<ResultUserRoleResponseDto> TDeleteFromDatabaseById(int id);
    Response<List<ResultUserRoleResponseDto>> TGetAll(Expression<Func<UserRole, bool>>? predicate = null, Func<IQueryable<UserRole>, IIncludableQueryable<UserRole, object>>? include = null);
    Response<ResultUserRoleResponseDto> TGetById(int id, Func<IQueryable<UserRole>, IIncludableQueryable<UserRole, object>>? include = null);
    Response<ResultUserRoleResponseDto> TGetByFilter(Expression<Func<UserRole, bool>> predicate, Func<IQueryable<UserRole>, IIncludableQueryable<UserRole, object>>? include = null);

    Task<Response<ResultUserRoleResponseDto>> TCreateAsync(CreateUserRoleRequestDto createUserRoleRequestDto);
    Task<Response<ResultUserRoleResponseDto>> TUpdateAsync(UpdateUserRoleRequestDto updateUserRoleRequestDto);
    Task<Response<ResultUserRoleResponseDto>> TDeleteFromDatabaseByIdAsync(int id);
    Task<Response<List<ResultUserRoleResponseDto>>> TGetAllAsync(Expression<Func<UserRole, bool>>? predicate = null, Func<IQueryable<UserRole>, IIncludableQueryable<UserRole, object>>? include = null);
    Task<Response<ResultUserRoleResponseDto>> TGetByIdAsync(int id, Func<IQueryable<UserRole>, IIncludableQueryable<UserRole, object>>? include = null);
    Task<Response<ResultUserRoleResponseDto>> TGetByFilterAsync(Expression<Func<UserRole, bool>> predicate, Func<IQueryable<UserRole>, IIncludableQueryable<UserRole, object>>? include = null);
}
