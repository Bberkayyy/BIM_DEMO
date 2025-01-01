using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using BusinessLogicLayer.Extensions;
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
    private readonly IUserRules _rules;

    public UserManager(IUserRepository userRepository, IUserRules rules)
    {
        _userRepository = userRepository;
        _rules = rules;
    }

    public Response<ResultUserResponseDto> TCreate(CreateUserRequestDto createUserRequestDto)
    {
        try
        {
            _rules.StoreExists(createUserRequestDto.StoreId);
            _rules.IdentityNumberMustBeElevenCharacter(createUserRequestDto.IdentityNumber);
            _rules.IdentityNumberMustBeDigit(createUserRequestDto.IdentityNumber);
            _rules.IdentityNumberMustBeUnique(createUserRequestDto.IdentityNumber);
            int userCode = GenerateClass.GenerateRandomUniqueUserCode(_rules);
            int password = GenerateClass.GenerateRandomUserPassword();
            User createUser = CreateUserRequestDto.ConvertToEntity(createUserRequestDto, userCode, password);
            User createdUser = _userRepository.Create(createUser);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(createdUser);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                Message = "User created successfully!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }

    }

    public async Task<Response<ResultUserResponseDto>> TCreateAsync(CreateUserRequestDto createUserRequestDto)
    {
        try
        {
            _rules.StoreExists(createUserRequestDto.StoreId);
            _rules.IdentityNumberMustBeElevenCharacter(createUserRequestDto.IdentityNumber);
            _rules.IdentityNumberMustBeDigit(createUserRequestDto.IdentityNumber);
            _rules.IdentityNumberMustBeUnique(createUserRequestDto.IdentityNumber);
            int userCode = GenerateClass.GenerateRandomUniqueUserCode(_rules);
            int password = GenerateClass.GenerateRandomUserPassword();
            User createUser = CreateUserRequestDto.ConvertToEntity(createUserRequestDto, userCode, password);
            User createdUser = await _userRepository.CreateAsync(createUser);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(createdUser);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                Message = "User created successfully!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<ResultUserResponseDto> TDeleteById(int id)
    {
        try
        {
            User? user = _userRepository.GetByFilter(x => x.Id == id, x => x.Include(x => x.Store));
            _rules.UserExists(user);
            user!.Deleted = DateTime.Now;
            _userRepository.Delete(user);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(user);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                Message = "User deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.NoContent
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultUserResponseDto>> TDeleteByIdAsync(int id)
    {
        try
        {
            User? user = await _userRepository.GetByFilterAsync(x => x.Id == id, x => x.Include(x => x.Store));
            _rules.UserExists(user);
            user!.Deleted = DateTime.Now;
            await _userRepository.DeleteAsync(user);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(user);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                Message = "User deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.NoContent
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultUserResponseDto> TDeleteByUserCode(int userCode)
    {
        try
        {
            User? user = _userRepository.GetByFilter(x => x.UserCode == userCode, x => x.Include(x => x.Store));
            _rules.UserExists(user);
            user!.Deleted = DateTime.Now;
            _userRepository.Delete(user);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(user);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                Message = "User deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.NoContent
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultUserResponseDto>> TDeleteByUserCodeAsync(int userCode)
    {
        try
        {
            User? user = await _userRepository.GetByFilterAsync(x => x.UserCode == userCode, x => x.Include(x => x.Store));
            _rules.UserExists(user);
            user!.Deleted = DateTime.Now;
            await _userRepository.DeleteAsync(user);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(user);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                Message = "User deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.NoContent
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultUserResponseDto> TDeleteFromDatabaseById(int id)
    {
        try
        {
            User? user = _userRepository.GetByFilter(x => x.Id == id, x => x.Include(x => x.Store));
            _rules.UserExists(user, true);
            _userRepository.DeleteFromDatabase(user!);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(user!);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                Message = "User deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.NoContent
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultUserResponseDto>> TDeleteFromDatabaseByIdAsync(int id)
    {
        try
        {
            User? user = await _userRepository.GetByFilterAsync(x => x.Id == id, x => x.Include(x => x.Store));
            _rules.UserExists(user, true);
            await _userRepository.DeleteFromDatabaseAsync(user!);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(user!);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                Message = "User deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.NoContent
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultUserResponseDto> TDeleteFromDatabaseByUserCode(int userCode)
    {
        try
        {
            User? user = _userRepository.GetByFilter(x => x.UserCode == userCode, x => x.Include(x => x.Store));
            _rules.UserExists(user, true);
            _userRepository.DeleteFromDatabase(user!);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(user!);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                Message = "User deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.NoContent
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultUserResponseDto>> TDeleteFromDatabaseByUserCodeAsync(int userCode)
    {
        try
        {
            User? user = await _userRepository.GetByFilterAsync(x => x.UserCode == userCode, x => x.Include(x => x.Store));
            _rules.UserExists(user, true);
            await _userRepository.DeleteFromDatabaseAsync(user!);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(user!);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                Message = "User deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.NoContent
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
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
        try
        {
            User? user = _userRepository.GetByFilter(predicate, include);
            _rules.UserExists(user);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(user!);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultUserResponseDto>> TGetByFilterAsync(Expression<Func<User, bool>> predicate, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
    {
        try
        {
            User? user = await _userRepository.GetByFilterAsync(predicate, include);
            _rules.UserExists(user);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(user!);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultUserResponseDto> TGetById(int id, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
    {
        try
        {
            User? user = _userRepository.GetById(id, include);
            _rules.UserExists(user);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(user!);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultUserResponseDto>> TGetByIdAsync(int id, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
    {
        try
        {
            User? user = await _userRepository.GetByIdAsync(id, include);
            _rules.UserExists(user);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(user!);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultUserResponseDto> TUpdate(UpdateUserRequestDto updateUserRequestDto)
    {
        try
        {
            _rules.StoreExists(updateUserRequestDto.StoreId);
            _rules.IdentityNumberMustBeElevenCharacter(updateUserRequestDto.IdentityNumber);
            _rules.IdentityNumberMustBeDigit(updateUserRequestDto.IdentityNumber);
            _rules.IdentityNumberMustBeUnique(updateUserRequestDto.IdentityNumber, updateUserRequestDto.Id);
            _rules.UserCodeMustBeSixCharacter(updateUserRequestDto.UserCode);
            _rules.UserCodeMustBeUnique(updateUserRequestDto.UserCode, updateUserRequestDto.Id);
            User updateUser = UpdateUserRequestDto.ConvertToEntity(updateUserRequestDto);
            User updatedUser = _userRepository.Update(updateUser);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(updatedUser);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                Message = "User updated successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public async Task<Response<ResultUserResponseDto>> TUpdateAsync(UpdateUserRequestDto updateUserRequestDto)
    {
        try
        {
            _rules.StoreExists(updateUserRequestDto.StoreId);
            _rules.IdentityNumberMustBeElevenCharacter(updateUserRequestDto.IdentityNumber);
            _rules.IdentityNumberMustBeDigit(updateUserRequestDto.IdentityNumber);
            _rules.IdentityNumberMustBeUnique(updateUserRequestDto.IdentityNumber, updateUserRequestDto.Id);
            _rules.UserCodeMustBeSixCharacter(updateUserRequestDto.UserCode);
            _rules.UserCodeMustBeUnique(updateUserRequestDto.UserCode, updateUserRequestDto.Id);
            User updateUser = UpdateUserRequestDto.ConvertToEntity(updateUserRequestDto);
            User updatedUser = await _userRepository.UpdateAsync(updateUser);
            ResultUserResponseDto response = ResultUserResponseDto.ConvertToResponsed(updatedUser);
            return new Response<ResultUserResponseDto>()
            {
                Data = response,
                Message = "User updated successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
