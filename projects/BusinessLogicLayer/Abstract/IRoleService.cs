using Core.Shared;
using EntityLayer.Dtos.RequestDtos.RoleRequestDtos;
using EntityLayer.Dtos.ResponseDtos.RoleResponseDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstract;

public interface IRoleService
{
    Response<ResultRoleResponseDto> TCreate(CreateRoleRequestDto createRoleRequestDto);
    Response<ResultRoleResponseDto> TUpdate(UpdateRoleRequestDto updateRoleRequestDto);
    Response<ResultRoleResponseDto> TDeleteFromDatabaseById(int id);
    Response<ResultRoleResponseDto> TDeleteById(int id);
    Response<List<ResultRoleResponseDto>> TGetAll(Expression<Func<Role, bool>>? predicate = null, Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? include = null);
    Response<ResultRoleResponseDto> TGetById(int id, Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? include = null);
    Response<ResultRoleResponseDto> TGetByFilter(Expression<Func<Role, bool>> predicate, Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? include = null);

    Task<Response<ResultRoleResponseDto>> TCreateAsync(CreateRoleRequestDto createRoleRequestDto);
    Task<Response<ResultRoleResponseDto>> TUpdateAsync(UpdateRoleRequestDto updateRoleRequestDto);
    Task<Response<ResultRoleResponseDto>> TDeleteFromDatabaseByIdAsync(int id);
    Task<Response<ResultRoleResponseDto>> TDeleteByIdAsync(int id);
    Task<Response<List<ResultRoleResponseDto>>> TGetAllAsync(Expression<Func<Role, bool>>? predicate = null, Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? include = null);
    Task<Response<ResultRoleResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? include = null);
    Task<Response<ResultRoleResponseDto>> TGetByFilterAsync(Expression<Func<Role, bool>> predicate, Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? include = null);
}
