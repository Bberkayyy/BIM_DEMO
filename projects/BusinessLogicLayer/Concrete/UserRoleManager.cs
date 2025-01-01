using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using Core.Shared;
using DataAccessLayer.Repositories.UserRoleRepositories;
using EntityLayer.Dtos.RequestDtos.UserRoleRequestDtos;
using EntityLayer.Dtos.ResponseDtos.UserRoleResponseDtos;
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

public class UserRoleManager : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserRoleRules _rules;

    public UserRoleManager(IUserRoleRepository userRoleRepository, IUserRoleRules rules)
    {
        _userRoleRepository = userRoleRepository;
        _rules = rules;
    }

    public Response<ResultUserRoleResponseDto> TCreate(CreateUserRoleRequestDto createUserRoleRequestDto)
    {
        try
        {
            _rules.UserExists(createUserRoleRequestDto.UserId);
            _rules.RoleExists(createUserRoleRequestDto.RoleId);
            UserRole createUserRole = CreateUserRoleRequestDto.ConvertToEntity(createUserRoleRequestDto);
            UserRole createdUserRole = _userRoleRepository.Create(createUserRole);
            ResultUserRoleResponseDto response = ResultUserRoleResponseDto.ConvertToResponse(createdUserRole);
            return new Response<ResultUserRoleResponseDto>
            {
                Data = response,
                Message = "User role created successfully!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public async Task<Response<ResultUserRoleResponseDto>> TCreateAsync(CreateUserRoleRequestDto createUserRoleRequestDto)
    {
        try
        {
            _rules.UserExists(createUserRoleRequestDto.UserId);
            _rules.RoleExists(createUserRoleRequestDto.RoleId);
            UserRole createUserRole = CreateUserRoleRequestDto.ConvertToEntity(createUserRoleRequestDto);
            UserRole createdUserRole = await _userRoleRepository.CreateAsync(createUserRole);
            ResultUserRoleResponseDto response = ResultUserRoleResponseDto.ConvertToResponse(createdUserRole);
            return new Response<ResultUserRoleResponseDto>
            {
                Data = response,
                Message = "User role created successfully!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<ResultUserRoleResponseDto> TDeleteFromDatabaseById(int id)
    {
        try
        {
            UserRole? userRole = _userRoleRepository.GetByFilter(x => x.Id == id, x => x.Include(x => x.User).Include(x => x.Role));
            _rules.UserRoleExists(userRole);
            _userRoleRepository.DeleteFromDatabase(userRole!);
            ResultUserRoleResponseDto response = ResultUserRoleResponseDto.ConvertToResponse(userRole!);
            return new Response<ResultUserRoleResponseDto>
            {
                Data = response,
                Message = $"User role deleted successfully! (The '{userRole!.Role.Name}' role of the user with user code '{userRole!.User.UserCode}' has been removed.)",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultUserRoleResponseDto>> TDeleteFromDatabaseByIdAsync(int id)
    {
        try
        {
            UserRole? userRole = await _userRoleRepository.GetByFilterAsync(x => x.Id == id, x => x.Include(x => x.User).Include(x => x.Role));
            _rules.UserRoleExists(userRole);
            await _userRoleRepository.DeleteFromDatabaseAsync(userRole!);
            ResultUserRoleResponseDto response = ResultUserRoleResponseDto.ConvertToResponse(userRole!);
            return new Response<ResultUserRoleResponseDto>
            {
                Data = response,
                Message = $"User role deleted successfully! (The '{userRole!.Role.Name}' role of the user with user code '{userRole!.User.UserCode}' has been removed.)",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<List<ResultUserRoleResponseDto>> TGetAll(Expression<Func<UserRole, bool>>? predicate = null, Func<IQueryable<UserRole>, IIncludableQueryable<UserRole, object>>? include = null)
    {
        List<UserRole> userRoles = _userRoleRepository.GetAll(predicate, include ?? (x => x.Include(x => x.Role).Include(x => x.User)));
        List<ResultUserRoleResponseDto> response = userRoles.Select(x => ResultUserRoleResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultUserRoleResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<List<ResultUserRoleResponseDto>>> TGetAllAsync(Expression<Func<UserRole, bool>>? predicate = null, Func<IQueryable<UserRole>, IIncludableQueryable<UserRole, object>>? include = null)
    {
        List<UserRole> userRoles = await _userRoleRepository.GetAllAsync(predicate, include ?? (x => x.Include(x => x.Role).Include(x => x.User)));
        List<ResultUserRoleResponseDto> response = userRoles.Select(x => ResultUserRoleResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultUserRoleResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ResultUserRoleResponseDto> TGetByFilter(Expression<Func<UserRole, bool>> predicate, Func<IQueryable<UserRole>, IIncludableQueryable<UserRole, object>>? include = null)
    {
        try
        {
            UserRole? userRole = _userRoleRepository.GetByFilter(predicate, include);
            _rules.UserRoleExists(userRole);
            ResultUserRoleResponseDto response = ResultUserRoleResponseDto.ConvertToResponse(userRole!);
            return new Response<ResultUserRoleResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultUserRoleResponseDto>> TGetByFilterAsync(Expression<Func<UserRole, bool>> predicate, Func<IQueryable<UserRole>, IIncludableQueryable<UserRole, object>>? include = null)
    {
        try
        {
            UserRole? userRole = await _userRoleRepository.GetByFilterAsync(predicate, include);
            _rules.UserRoleExists(userRole);
            ResultUserRoleResponseDto response = ResultUserRoleResponseDto.ConvertToResponse(userRole!);
            return new Response<ResultUserRoleResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultUserRoleResponseDto> TGetById(int id, Func<IQueryable<UserRole>, IIncludableQueryable<UserRole, object>>? include = null)
    {
        try
        {
            UserRole? userRole = _userRoleRepository.GetById(id, include);
            _rules.UserRoleExists(userRole);
            ResultUserRoleResponseDto response = ResultUserRoleResponseDto.ConvertToResponse(userRole!);
            return new Response<ResultUserRoleResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultUserRoleResponseDto>> TGetByIdAsync(int id, Func<IQueryable<UserRole>, IIncludableQueryable<UserRole, object>>? include = null)
    {
        try
        {
            UserRole? userRole = await _userRoleRepository.GetByIdAsync(id, include);
            _rules.UserRoleExists(userRole);
            ResultUserRoleResponseDto response = ResultUserRoleResponseDto.ConvertToResponse(userRole!);
            return new Response<ResultUserRoleResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultUserRoleResponseDto> TUpdate(UpdateUserRoleRequestDto updateUserRoleRequestDto)
    {
        try
        {
            _rules.UserExists(updateUserRoleRequestDto.UserId);
            _rules.RoleExists(updateUserRoleRequestDto.RoleId);
            UserRole updateUserRole = UpdateUserRoleRequestDto.ConvertToEntity(updateUserRoleRequestDto);
            UserRole updatedUserRole = _userRoleRepository.Update(updateUserRole);
            ResultUserRoleResponseDto response = ResultUserRoleResponseDto.ConvertToResponse(updateUserRole);
            return new Response<ResultUserRoleResponseDto>
            {
                Data = response,
                Message = "User role updated successfully!",
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public async Task<Response<ResultUserRoleResponseDto>> TUpdateAsync(UpdateUserRoleRequestDto updateUserRoleRequestDto)
    {
        try
        {
            _rules.UserExists(updateUserRoleRequestDto.UserId);
            _rules.RoleExists(updateUserRoleRequestDto.RoleId);
            UserRole updateUserRole = UpdateUserRoleRequestDto.ConvertToEntity(updateUserRoleRequestDto);
            UserRole updatedUserRole = await _userRoleRepository.UpdateAsync(updateUserRole);
            ResultUserRoleResponseDto response = ResultUserRoleResponseDto.ConvertToResponse(updateUserRole);
            return new Response<ResultUserRoleResponseDto>
            {
                Data = response,
                Message = "User role updated successfully!",
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
        }
        catch (Exception e)
        {
            return new Response<ResultUserRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
