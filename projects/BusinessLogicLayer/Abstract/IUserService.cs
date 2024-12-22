using EntityLayer.Dtos.RequestDtos.UserRequestDtos;
using EntityLayer.Dtos.RequestDtos;
using EntityLayer.Dtos.ResponseDtos.UserResponseDtos;
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

public interface IUserService
{
    Response<ResultUserResponseDto> TCreate(CreateUserRequestDto createUserRequestDto);
    Response<ResultUserResponseDto> TUpdate(UpdateUserRequestDto updateUserRequestDto);
    Response<ResultUserResponseDto> TDeleteFromDatabaseById(int id);
    Response<ResultUserResponseDto> TDeleteFromDatabaseByUserCode(int userCode);
    Response<ResultUserResponseDto> TDeleteById(int id);
    Response<ResultUserResponseDto> TDeleteByUserCode(int userCode);
    Response<List<ResultUserResponseDto>> TGetAll(Expression<Func<User, bool>>? predicate = null, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null);
    Response<ResultUserResponseDto> TGetById(int id, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null);
    Response<ResultUserResponseDto> TGetByFilter(Expression<Func<User, bool>> predicate, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null);

    Task<Response<ResultUserResponseDto>> TCreateAsync(CreateUserRequestDto createUserRequestDto);
    Task<Response<ResultUserResponseDto>> TUpdateAsync(UpdateUserRequestDto updateUserRequestDto);
    Task<Response<ResultUserResponseDto>> TDeleteFromDatabaseByIdAsync(int id);
    Task<Response<ResultUserResponseDto>> TDeleteFromDatabaseByUserCodeAsync(int userCode);
    Task<Response<ResultUserResponseDto>> TDeleteByIdAsync(int id);
    Task<Response<ResultUserResponseDto>> TDeleteByUserCodeAsync(int userCode);
    Task<Response<List<ResultUserResponseDto>>> TGetAllAsync(Expression<Func<User, bool>>? predicate = null, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null);
    Task<Response<ResultUserResponseDto>> TGetByIdAsync(int id, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null);
    Task<Response<ResultUserResponseDto>> TGetByFilterAsync(Expression<Func<User, bool>> predicate, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null);
}
