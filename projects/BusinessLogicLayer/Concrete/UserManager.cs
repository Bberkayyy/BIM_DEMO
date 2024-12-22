using BusinessLogicLayer.Abstract;
using Core.Shared;
using DataAccessLayer.Repositories.UserRepositories;
using EntityLayer.Dtos.RequestDtos.UserRequestDtos;
using EntityLayer.Dtos.ResponseDtos.UserResponseDtos;
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

public class UserManager : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Response<ResultUserResponseDto> TCreate(CreateUserRequestDto createUserRequestDto)
    {
        User createUser = CreateUserRequestDto.ConvertToEntity(createUserRequestDto);
        User createdUser = _userRepository.Create(createUser);
        ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(createdUser);
        return new Response<ResultUserResponseDto>()
        {
            Data = response,
            Message = "User created successfully!",
            StatusCode = System.Net.HttpStatusCode.Created
        };
    }

    public async Task<Response<ResultUserResponseDto>> TCreateAsync(CreateUserRequestDto createUserRequestDto)
    {
        User createUser = CreateUserRequestDto.ConvertToEntity(createUserRequestDto);
        User createdUser = await _userRepository.CreateAsync(createUser);
        ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(createdUser);
        return new Response<ResultUserResponseDto>()
        {
            Data = response,
            Message = "User created successfully!",
            StatusCode = System.Net.HttpStatusCode.Created
        };
    }

    public Response<ResultUserResponseDto> TDeleteById(int id)
    {
        User? user = _userRepository.GetByFilter(x => x.Id == id, x => x.Include(x => x.Store));
        if (user != null)
        {
            user.Deleted = DateTime.Now;
            _userRepository.Delete(user);
        }
        return new Response<ResultUserResponseDto>()
        {
            Data = user != null ? ResultUserResponseDto.ConvertToResponsed(user) : null,
            Message = user != null ? "User deleted successfully!" : "User not found",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultUserResponseDto>> TDeleteByIdAsync(int id)
    {
        User? user = await _userRepository.GetByFilterAsync(x => x.Id == id, x => x.Include(x => x.Store));
        if (user != null)
        {
            user.Deleted = DateTime.Now;
            await _userRepository.DeleteAsync(user);
        }
        return new Response<ResultUserResponseDto>()
        {
            Data = user != null ? ResultUserResponseDto.ConvertToResponsed(user) : null,
            Message = user != null ? "User deleted successfully!" : "User not found",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultUserResponseDto> TDeleteByUserCode(int userCode)
    {
        User? user = _userRepository.GetByFilter(x => x.UserCode == userCode, x => x.Include(x => x.Store));
        if (user != null)
        {
            user.Deleted = DateTime.Now;
            _userRepository.Delete(user);
        }
        return new Response<ResultUserResponseDto>()
        {
            Data = user != null ? ResultUserResponseDto.ConvertToResponsed(user) : null,
            Message = user != null ? "User deleted successfully!" : "User not found",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultUserResponseDto>> TDeleteByUserCodeAsync(int userCode)
    {
        User? user = await _userRepository.GetByFilterAsync(x => x.UserCode == userCode, x => x.Include(x => x.Store));
        if (user != null)
        {
            user.Deleted = DateTime.Now;
            await _userRepository.DeleteAsync(user);
        }
        return new Response<ResultUserResponseDto>()
        {
            Data = user != null ? ResultUserResponseDto.ConvertToResponsed(user) : null,
            Message = user != null ? "User deleted successfully!" : "User not found",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultUserResponseDto> TDeleteFromDatabaseById(int id)
    {
        User? user = _userRepository.GetByFilter(x => x.Id == id, x => x.Include(x => x.Store));
        if (user != null)
            _userRepository.DeleteFromDatabase(user);
        return new Response<ResultUserResponseDto>()
        {
            Data = user != null ? ResultUserResponseDto.ConvertToResponsed(user) : null,
            Message = user != null ? "User deleted successfully!" : "User not found",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultUserResponseDto>> TDeleteFromDatabaseByIdAsync(int id)
    {
        User? user = await _userRepository.GetByFilterAsync(x => x.Id == id, x => x.Include(x => x.Store));
        if (user != null)
            await _userRepository.DeleteFromDatabaseAsync(user);
        return new Response<ResultUserResponseDto>()
        {
            Data = user != null ? ResultUserResponseDto.ConvertToResponsed(user) : null,
            Message = user != null ? "User deleted successfully!" : "User not found",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultUserResponseDto> TDeleteFromDatabaseByUserCode(int userCode)
    {
        User? user = _userRepository.GetByFilter(x => x.UserCode == userCode, x => x.Include(x => x.Store));
        if (user != null)
            _userRepository.DeleteFromDatabase(user);
        return new Response<ResultUserResponseDto>()
        {
            Data = user != null ? ResultUserResponseDto.ConvertToResponsed(user) : null,
            Message = user != null ? "User deleted successfully!" : "User not found",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultUserResponseDto>> TDeleteFromDatabaseByUserCodeAsync(int userCode)
    {
        User? user = await _userRepository.GetByFilterAsync(x => x.UserCode == userCode, x => x.Include(x => x.Store));
        if (user != null)
            await _userRepository.DeleteFromDatabaseAsync(user);
        return new Response<ResultUserResponseDto>()
        {
            Data = user != null ? ResultUserResponseDto.ConvertToResponsed(user) : null,
            Message = user != null ? "User deleted successfully!" : "User not found",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<List<ResultUserResponseDto>> TGetAll(Expression<Func<User, bool>>? predicate = null, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
    {
        List<User> users = _userRepository.GetAll(x => x.Deleted == null, x => x.Include(x => x.Store));
        List<ResultUserResponseDto> response = users.Select(x => ResultUserResponseDto.ConvertToResponsed(x)).ToList();
        return new Response<List<ResultUserResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<List<ResultUserResponseDto>>> TGetAllAsync(Expression<Func<User, bool>>? predicate = null, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
    {
        List<User> users = await _userRepository.GetAllAsync(x => x.Deleted == null, x => x.Include(x => x.Store));
        List<ResultUserResponseDto> response = users.Select(x => ResultUserResponseDto.ConvertToResponsed(x)).ToList();
        return new Response<List<ResultUserResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ResultUserResponseDto> TGetByFilter(Expression<Func<User, bool>> predicate, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
    {
        User? user = _userRepository.GetByFilter(predicate, include);
        return new Response<ResultUserResponseDto>()
        {
            Data = user != null ? ResultUserResponseDto.ConvertToResponsed(user) : null,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<ResultUserResponseDto>> TGetByFilterAsync(Expression<Func<User, bool>> predicate, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
    {
        User? user = await _userRepository.GetByFilterAsync(predicate, include);
        return new Response<ResultUserResponseDto>()
        {
            Data = user != null ? ResultUserResponseDto.ConvertToResponsed(user) : null,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ResultUserResponseDto> TGetById(int id, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
    {
        User? user = _userRepository.GetById(id, include);
        return new Response<ResultUserResponseDto>()
        {
            Data = user != null ? ResultUserResponseDto.ConvertToResponsed(user) : null,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<ResultUserResponseDto>> TGetByIdAsync(int id, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
    {
        User? user = await _userRepository.GetByIdAsync(id, include);
        return new Response<ResultUserResponseDto>()
        {
            Data = user != null ? ResultUserResponseDto.ConvertToResponsed(user) : null,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ResultUserResponseDto> TUpdate(UpdateUserRequestDto updateUserRequestDto)
    {
        User updateUser = UpdateUserRequestDto.ConvertToEntity(updateUserRequestDto);
        User updatedUser = _userRepository.Update(updateUser);
        ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(updatedUser);
        return new Response<ResultUserResponseDto>()
        {
            Data = response,
            Message = " User updated successfully!",
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<ResultUserResponseDto>> TUpdateAsync(UpdateUserRequestDto updateUserRequestDto)
    {
        User updateUser = UpdateUserRequestDto.ConvertToEntity(updateUserRequestDto);
        User updatedUser = await _userRepository.UpdateAsync(updateUser);
        ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(updatedUser);
        return new Response<ResultUserResponseDto>()
        {
            Data = response,
            Message = " User updated successfully!",
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }
}
