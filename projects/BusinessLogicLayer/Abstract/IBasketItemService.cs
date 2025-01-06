using Core.Shared;
using EntityLayer.Dtos.RequestDtos.BasketItemsRequestDtos;
using EntityLayer.Dtos.ResponseDtos.BasketItemResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstract;

public interface IBasketItemService
{
    Response<ResultBasketItemResponseDto> TAddToBasket(CreateBasketItemRequestDto createBasketItemRequestDto);
    Response<ResultBasketItemResponseDto> TRemoveFromBasket(long? barcodeNo, int? shortCode, decimal quantity = 1);
    Response<List<ResultBasketItemResponseDto>> TGetBasketItems();

    Task<Response<ResultBasketItemResponseDto>> TAddToBasketAsync(CreateBasketItemRequestDto createBasketItemRequestDto);
    Task<Response<ResultBasketItemResponseDto>> TRemoveFromBasketAsync(long? barcodeNo, int? shortCode, decimal quantity = 1);
    Task<Response<List<ResultBasketItemResponseDto>>> TGetBasketItemsAsync();
}
