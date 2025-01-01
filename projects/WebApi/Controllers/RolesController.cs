using BusinessLogicLayer.Abstract;
using Core.Shared;
using EntityLayer.Dtos.RequestDtos.RoleRequestDtos;
using EntityLayer.Dtos.ResponseDtos.RoleResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class RolesController : BaseController
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    [HttpPost]
    public IActionResult Create(CreateRoleRequestDto createRoleRequestDto)
    {
        Response<ResultRoleResponseDto> result = _roleService.TCreate(createRoleRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncCreate(CreateRoleRequestDto createRoleRequestDto)
    {
        Response<ResultRoleResponseDto> result = await _roleService.TCreateAsync(createRoleRequestDto);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteById(int id)
    {
        Response<ResultRoleResponseDto> result = _roleService.TDeleteById(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteById(int id)
    {
        Response<ResultRoleResponseDto> result = await _roleService.TDeleteByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteFromDatabaseById(int id)
    {
        Response<ResultRoleResponseDto> result = _roleService.TDeleteFromDatabaseById(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteFromDatabaseById(int id)
    {
        Response<ResultRoleResponseDto> result = await _roleService.TDeleteFromDatabaseByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        Response<List<ResultRoleResponseDto>> result = _roleService.TGetAll();
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetAll()
    {
        Response<List<ResultRoleResponseDto>> result = await _roleService.TGetAllAsync();
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetByName(string name)
    {
        Response<ResultRoleResponseDto> result = _roleService.TGetByFilter(x => x.Name == name);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetByName(string name)
    {
        Response<ResultRoleResponseDto> result = await _roleService.TGetByFilterAsync(x => x.Name == name);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetById(int id)
    {
        Response<ResultRoleResponseDto> result = _roleService.TGetById(id);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetById(int id)
    {
        Response<ResultRoleResponseDto> result = await _roleService.TGetByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpPut]
    public IActionResult Update(UpdateRoleRequestDto updateRoleRequestDto)
    {
        Response<ResultRoleResponseDto> result = _roleService.TUpdate(updateRoleRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPut]
    public async Task<IActionResult> AsyncUpdate(UpdateRoleRequestDto updateRoleRequestDto)
    {
        Response<ResultRoleResponseDto> result = await _roleService.TUpdateAsync(updateRoleRequestDto);
        return ActionResultInstance(result);
    }
}
