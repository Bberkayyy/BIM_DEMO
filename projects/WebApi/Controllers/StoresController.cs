using BusinessLogicLayer.Abstract;
using Core.Shared;
using EntityLayer.Dtos.RequestDtos.StoreRequestDtos;
using EntityLayer.Dtos.ResponseDtos.CategoryResponseDtos;
using EntityLayer.Dtos.ResponseDtos.StoreResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class StoresController : BaseController
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        Response<List<ResultStoreResponseDto>> result = _storeService.TGetAll();
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetAll()
    {
        Response<List<ResultStoreResponseDto>> result = await _storeService.TGetAllAsync();
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetById(int id)
    {
        Response<ResultStoreResponseDto> result = _storeService.TGetById(id);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetById(int id)
    {
        Response<ResultStoreResponseDto> result = await _storeService.TGetByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetByStoreNo(string storeNo)
    {
        Response<ResultStoreResponseDto> result = _storeService.TGetByFilter(x => x.StoreNo == storeNo);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetByStoreNo(string storeNo)
    {
        Response<ResultStoreResponseDto> result = await _storeService.TGetByFilterAsync(x => x.StoreNo == storeNo);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetByName(string name)
    {
        Response<ResultStoreResponseDto> result = _storeService.TGetByFilter(x => x.Name == name);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetByName(string name)
    {
        Response<ResultStoreResponseDto> result = await _storeService.TGetByFilterAsync(x => x.Name == name);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetCityStores(string city)
    {
        Response<List<ResultStoreResponseDto>> result = _storeService.TGetAll(x => x.City.ToLower().Equals(city.ToLower()) && x.Deleted == null);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetCityStores(string city)
    {
        Response<List<ResultStoreResponseDto>> result = await _storeService.TGetAllAsync(x => x.City.ToLower().Equals(city.ToLower()) && x.Deleted == null);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public IActionResult Create(CreateStoreRequestDto createStoreRequestDto)
    {
        Response<ResultStoreResponseDto> result = _storeService.TCreate(createStoreRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncCreate(CreateStoreRequestDto createStoreRequestDto)
    {
        Response<ResultStoreResponseDto> result = await _storeService.TCreateAsync(createStoreRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPut]
    public IActionResult Update(UpdateStoreRequestDto updateStoreRequestDto)
    {
        Response<ResultStoreResponseDto> result = _storeService.TUpdate(updateStoreRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPut]
    public async Task<IActionResult> AsyncUpdate(UpdateStoreRequestDto updateStoreRequestDto)
    {
        Response<ResultStoreResponseDto> result = await _storeService.TUpdateAsync(updateStoreRequestDto);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteById(int id)
    {
        Response<ResultStoreResponseDto> result = _storeService.TDeleteById(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteById(int id)
    {
        Response<ResultStoreResponseDto> result = await _storeService.TDeleteByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteByStoreNo(string storeNo)
    {
        Response<ResultStoreResponseDto> result = _storeService.TDeleteByStoreNo(storeNo);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteByStoreNo(string storeNo)
    {
        Response<ResultStoreResponseDto> result = await _storeService.TDeleteByStoreNoAsync(storeNo);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteFromDatabaseById(int id)
    {
        Response<ResultStoreResponseDto> result = _storeService.TDeleteFromDatabaseById(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteFromDatabaseById(int id)
    {
        Response<ResultStoreResponseDto> result = await
            _storeService.TDeleteFromDatabaseByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteFromDatabaseByStoreNo(string storeNo)
    {
        Response<ResultStoreResponseDto> result = _storeService.TDeleteFromDatabaseByStoreNo(storeNo);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteFromDatabaseByStoreNo(string storeNo)
    {
        Response<ResultStoreResponseDto> result = await
            _storeService.TDeleteFromDatabaseByStoreNoAsync(storeNo);
        return ActionResultInstance(result);
    }
}
