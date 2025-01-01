using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using Core.Shared;
using DataAccessLayer.Repositories.RoleRepositories;
using EntityLayer.Dtos.RequestDtos.RoleRequestDtos;
using EntityLayer.Dtos.ResponseDtos.RoleResponseDtos;
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

public class RoleManager : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleRules _rules;

    public RoleManager(IRoleRepository roleRepository, IRoleRules rules)
    {
        _roleRepository = roleRepository;
        _rules = rules;
    }

    public Response<ResultRoleResponseDto> TCreate(CreateRoleRequestDto createRoleRequestDto)
    {
        try
        {
            _rules.NameCannotBeNullOrWhiteSpace(createRoleRequestDto.Name);
            _rules.NameMustBeUnique(createRoleRequestDto.Name);
            Role createRole = CreateRoleRequestDto.ConvertToEntity(createRoleRequestDto);
            Role createdRole = _roleRepository.Create(createRole);
            ResultRoleResponseDto response = ResultRoleResponseDto.ConvertToResponse(createdRole);
            return new Response<ResultRoleResponseDto>
            {
                Data = response,
                Message = "Role created successfully!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public async Task<Response<ResultRoleResponseDto>> TCreateAsync(CreateRoleRequestDto createRoleRequestDto)
    {
        try
        {
            _rules.NameCannotBeNullOrWhiteSpace(createRoleRequestDto.Name);
            _rules.NameMustBeUnique(createRoleRequestDto.Name);
            Role createRole = CreateRoleRequestDto.ConvertToEntity(createRoleRequestDto);
            Role createdRole = await _roleRepository.CreateAsync(createRole);
            ResultRoleResponseDto response = ResultRoleResponseDto.ConvertToResponse(createdRole);
            return new Response<ResultRoleResponseDto>
            {
                Data = response,
                Message = "Role created successfully!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<ResultRoleResponseDto> TDeleteById(int id)
    {
        try
        {
            Role? role = _roleRepository.GetByFilter(x => x.Id == id, x => x.Include(x => x.UserRoles));
            _rules.RoleExists(role);
            foreach (var userRole in role!.UserRoles)
            {
                userRole.Deleted = DateTime.Now;
                //userrolerepo.delete
            }
            role.Deleted = DateTime.Now;
            _roleRepository.Delete(role);
            ResultRoleResponseDto response = ResultRoleResponseDto.ConvertToResponse(role);
            return new Response<ResultRoleResponseDto>
            {
                Data = response,
                Message = $"Role deleted successfully! ({role.UserRoles.Count} users with this role where affected.)",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultRoleResponseDto>> TDeleteByIdAsync(int id)
    {
        try
        {
            Role? role = await _roleRepository.GetByFilterAsync(x => x.Id == id, x => x.Include(x => x.UserRoles));
            _rules.RoleExists(role);
            foreach (var userRole in role!.UserRoles)
            {
                userRole.Deleted = DateTime.Now;
                //await userrolerepo.delete
            }
            role.Deleted = DateTime.Now;
            await _roleRepository.DeleteAsync(role);
            ResultRoleResponseDto response = ResultRoleResponseDto.ConvertToResponse(role);
            return new Response<ResultRoleResponseDto>
            {
                Data = response,
                Message = $"Role deleted successfully! ({role.UserRoles.Count} users with this role where affected.)",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultRoleResponseDto> TDeleteFromDatabaseById(int id)
    {
        try
        {
            Role? role = _roleRepository.GetByFilter(x => x.Id == id);
            _rules.RoleExists(role, true);
            _roleRepository.DeleteFromDatabase(role!);
            ResultRoleResponseDto response = ResultRoleResponseDto.ConvertToResponse(role!);
            return new Response<ResultRoleResponseDto>
            {
                Data = response,
                Message = "Role deleted from database successfully.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultRoleResponseDto>> TDeleteFromDatabaseByIdAsync(int id)
    {
        try
        {
            Role? role = await _roleRepository.GetByFilterAsync(x => x.Id == id);
            _rules.RoleExists(role, true);
            await _roleRepository.DeleteFromDatabaseAsync(role!);
            ResultRoleResponseDto response = ResultRoleResponseDto.ConvertToResponse(role!);
            return new Response<ResultRoleResponseDto>
            {
                Data = response,
                Message = "Role deleted from database successfully.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<List<ResultRoleResponseDto>> TGetAll(Expression<Func<Role, bool>>? predicate = null, Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? include = null)
    {
        List<Role> roles = _roleRepository.GetAll(predicate ?? (x => x.Deleted == null), include ?? (x => x.Include(x => x.UserRoles)));
        List<ResultRoleResponseDto> response = roles.Select(x => ResultRoleResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultRoleResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<List<ResultRoleResponseDto>>> TGetAllAsync(Expression<Func<Role, bool>>? predicate = null, Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? include = null)
    {
        List<Role> roles = await _roleRepository.GetAllAsync(predicate ?? (x => x.Deleted == null), include ?? (x => x.Include(x => x.UserRoles)));
        List<ResultRoleResponseDto> response = roles.Select(x => ResultRoleResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultRoleResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ResultRoleResponseDto> TGetByFilter(Expression<Func<Role, bool>> predicate, Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? include = null)
    {
        try
        {
            Role? role = _roleRepository.GetByFilter(predicate, include);
            _rules.RoleExists(role);
            ResultRoleResponseDto response = ResultRoleResponseDto.ConvertToResponse(role!);
            return new Response<ResultRoleResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultRoleResponseDto>> TGetByFilterAsync(Expression<Func<Role, bool>> predicate, Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? include = null)
    {
        try
        {
            Role? role = await _roleRepository.GetByFilterAsync(predicate, include);
            _rules.RoleExists(role);
            ResultRoleResponseDto response = ResultRoleResponseDto.ConvertToResponse(role!);
            return new Response<ResultRoleResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultRoleResponseDto> TGetById(int id, Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? include = null)
    {
        try
        {
            Role? role = _roleRepository.GetById(id, include);
            _rules.RoleExists(role);
            ResultRoleResponseDto response = ResultRoleResponseDto.ConvertToResponse(role!);
            return new Response<ResultRoleResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultRoleResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? include = null)
    {
        try
        {
            Role? role = await _roleRepository.GetByIdAsync(id, include);
            _rules.RoleExists(role);
            ResultRoleResponseDto response = ResultRoleResponseDto.ConvertToResponse(role!);
            return new Response<ResultRoleResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultRoleResponseDto> TUpdate(UpdateRoleRequestDto updateRoleRequestDto)
    {
        try
        {
            _rules.NameCannotBeNullOrWhiteSpace(updateRoleRequestDto.Name);
            _rules.NameMustBeUnique(updateRoleRequestDto.Name, updateRoleRequestDto.Id);
            Role updateRole = UpdateRoleRequestDto.ConvertToEntity(updateRoleRequestDto);
            Role updatedRole = _roleRepository.Update(updateRole);
            ResultRoleResponseDto response = ResultRoleResponseDto.ConvertToResponse(updatedRole);
            return new Response<ResultRoleResponseDto>
            {
                Data = response,
                Message = "Role updated successfully!",
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
        }
        catch (Exception e)
        {
            return new Response<ResultRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public async Task<Response<ResultRoleResponseDto>> TUpdateAsync(UpdateRoleRequestDto updateRoleRequestDto)
    {
        try
        {
            _rules.NameCannotBeNullOrWhiteSpace(updateRoleRequestDto.Name);
            _rules.NameMustBeUnique(updateRoleRequestDto.Name, updateRoleRequestDto.Id);
            Role updateRole = UpdateRoleRequestDto.ConvertToEntity(updateRoleRequestDto);
            Role updatedRole = await _roleRepository.UpdateAsync(updateRole);
            ResultRoleResponseDto response = ResultRoleResponseDto.ConvertToResponse(updatedRole);
            return new Response<ResultRoleResponseDto>
            {
                Data = response,
                Message = "Role updated successfully!",
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
        }
        catch (Exception e)
        {
            return new Response<ResultRoleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
