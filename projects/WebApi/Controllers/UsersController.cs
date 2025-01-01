using BusinessLogicLayer.Abstract;
using Core.Shared;
using EntityLayer.Dtos.RequestDtos.UserRequestDtos;
using EntityLayer.Dtos.ResponseDtos.UserResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class UsersController : BaseController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        Response<List<ResultUserResponseDto>> result = _userService.TGetAll();
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetAll()
    {
        Response<List<ResultUserResponseDto>> result = await _userService.TGetAllAsync();
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetById(int id)
    {
        Response<ResultUserResponseDto> result = _userService.TGetById(id);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetById(int id)
    {
        Response<ResultUserResponseDto> result = await _userService.TGetByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetByUserCode(int userCode)
    {
        Response<ResultUserResponseDto> result = _userService.TGetByFilter(x => x.UserCode == userCode);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetByUserCode(int userCode)
    {
        Response<ResultUserResponseDto> result = await _userService.TGetByFilterAsync(x => x.UserCode == userCode);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public IActionResult Create(CreateUserRequestDto createUserRequestDto)
    {
        Response<ResultUserResponseDto> result = _userService.TCreate(createUserRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncCreate(CreateUserRequestDto createUserRequestDto)
    {
        Response<ResultUserResponseDto> result = await _userService.TCreateAsync(createUserRequestDto);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteById(int id)
    {
        Response<ResultUserResponseDto> result = _userService.TDeleteById(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteById(int id)
    {
        Response<ResultUserResponseDto> result = await _userService.TDeleteByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteByUserCode(int userCode)
    {
        Response<ResultUserResponseDto> result = _userService.TDeleteByUserCode(userCode);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteByUserCode(int userCode)
    {
        Response<ResultUserResponseDto> result = await _userService.TDeleteByUserCodeAsync(userCode);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteFromDatabaseById(int id)
    {
        Response<ResultUserResponseDto> result = _userService.TDeleteFromDatabaseById(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteFromDatabaseById(int id)
    {
        Response<ResultUserResponseDto> result = await _userService.TDeleteFromDatabaseByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteFromDatabaseByUserCode(int userCode)
    {
        Response<ResultUserResponseDto> result = _userService.TDeleteFromDatabaseByUserCode(userCode);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteFromDatabaseByUserCode(int userCode)
    {
        Response<ResultUserResponseDto> result = await _userService.TDeleteFromDatabaseByUserCodeAsync(userCode);
        return ActionResultInstance(result);
    }
    [HttpPut]
    public IActionResult Update(UpdateUserRequestDto updateUserRequestDto)
    {
        Response<ResultUserResponseDto> result = _userService.TUpdate(updateUserRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPut]
    public async Task<IActionResult> AsyncUpdate(UpdateUserRequestDto updateUserRequestDto)
    {
        Response<ResultUserResponseDto> result = await _userService.TUpdateAsync(updateUserRequestDto);
        return ActionResultInstance(result);
    }
}
