using BusinessLogicLayer.Abstract;
using Core.Shared;
using EntityLayer.Dtos.RequestDtos.BasketItemsRequestDtos;
using EntityLayer.Dtos.RequestDtos.BasketRequestDtos;
using EntityLayer.Dtos.ResponseDtos.BasketItemResponseDtos;
using EntityLayer.Dtos.ResponseDtos.BasketResponseDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BasketsController : BaseController
{
    private readonly IBasketService _basketService;

    public BasketsController(IBasketService basketService)
    {
        _basketService = basketService;
    }
    [HttpPost]
    public async Task<IActionResult> SaveBasket(CreateBasketRequestDto createBasketRequestDto)
    {
        Response<ResultBasketResponseDto> result = await _basketService.TSaveBasketAsync(createBasketRequestDto);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        Response<List<ResultBasketResponseDto>> result = _basketService.TGetAll();
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetAll()
    {
        Response<List<ResultBasketResponseDto>> result = await _basketService.TGetAllAsync();
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteFromDatabase(int id)
    {
        Response<ResultBasketResponseDto> result = _basketService.TDeleteFromDatabase(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteFromDatabase(int id)
    {
        Response<ResultBasketResponseDto> result = await _basketService.TDeleteFromDatabaseAsync(id);
        return ActionResultInstance(result);
    }
}