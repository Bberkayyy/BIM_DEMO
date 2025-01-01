using BusinessLogicLayer.Abstract;
using Core.Shared;
using EntityLayer.Dtos.RequestDtos.UserRoleRequestDtos;
using EntityLayer.Dtos.ResponseDtos.UserRoleResponseDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class UserRolesController : BaseController
{
    private readonly IUserRoleService _userRoleService;

    public UserRolesController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }
    [HttpPost]
    public IActionResult Create(CreateUserRoleRequestDto createUserRoleRequestDto)
    {
        Response<ResultUserRoleResponseDto> result = _userRoleService.TCreate(createUserRoleRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncCreate(CreateUserRoleRequestDto createUserRoleRequestDto)
    {
        Response<ResultUserRoleResponseDto> result = await _userRoleService.TCreateAsync(createUserRoleRequestDto);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteFromDatabase(int id)
    {
        Response<ResultUserRoleResponseDto> result = _userRoleService.TDeleteFromDatabaseById(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteFromDatabase(int id)
    {
        Response<ResultUserRoleResponseDto> result = await _userRoleService.TDeleteFromDatabaseByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        Response<List<ResultUserRoleResponseDto>> result = _userRoleService.TGetAll();
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetAll()
    {
        Response<List<ResultUserRoleResponseDto>> result = await _userRoleService.TGetAllAsync();
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetById(int id)
    {
        Response<ResultUserRoleResponseDto> result = _userRoleService.TGetById(id);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetById(int id)
    {
        Response<ResultUserRoleResponseDto> result = await _userRoleService.TGetByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpPut]
    public IActionResult Update(UpdateUserRoleRequestDto updateUserRoleRequestDto)
    {
        Response<ResultUserRoleResponseDto> result = _userRoleService.TUpdate(updateUserRoleRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPut]
    public async Task<IActionResult> AsyncUpdate(UpdateUserRoleRequestDto updateUserRoleRequestDto)
    {
        Response<ResultUserRoleResponseDto> result = await _userRoleService.TUpdateAsync(updateUserRoleRequestDto);
        return ActionResultInstance(result);
    }
}
