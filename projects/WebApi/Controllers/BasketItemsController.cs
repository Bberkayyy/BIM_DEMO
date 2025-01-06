using BusinessLogicLayer.Abstract;
using Core.Shared;
using EntityLayer.Dtos.RequestDtos.BasketItemsRequestDtos;
using EntityLayer.Dtos.ResponseDtos.BasketItemResponseDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BasketItemsController : BaseController
{
    private readonly IBasketItemService _basketItemService;

    public BasketItemsController(IBasketItemService basketItemService)
    {
        _basketItemService = basketItemService;
    }

    [HttpPost]
    public IActionResult AddToBasket(CreateBasketItemRequestDto createBasketItemRequestDto)
    {
        Response<ResultBasketItemResponseDto> result = _basketItemService.TAddToBasket(createBasketItemRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncAddToBasket(CreateBasketItemRequestDto createBasketItemRequestDto)
    {
        Response<ResultBasketItemResponseDto> result = await _basketItemService.TAddToBasketAsync(createBasketItemRequestDto);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetBasketItems()
    {
        Response<List<ResultBasketItemResponseDto>> result = _basketItemService.TGetBasketItems();
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetBasketItems()
    {
        Response<List<ResultBasketItemResponseDto>> result = await _basketItemService.TGetBasketItemsAsync();
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult RemoveFromBasket(long? barcodeNo, int? shortCode, decimal quantity)
    {
        Response<ResultBasketItemResponseDto> result = _basketItemService.TRemoveFromBasket(barcodeNo, shortCode, quantity);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncRemoveFromBasket(long? barcodeNo, int? shortCode, decimal quantity)
    {
        Response<ResultBasketItemResponseDto> result = await _basketItemService.TRemoveFromBasketAsync(barcodeNo, shortCode, quantity);
        return ActionResultInstance(result);
    }
}
