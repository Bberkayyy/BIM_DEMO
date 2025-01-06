using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.BasketItemsRequestDtos;

public record CreateBasketItemRequestDto(long? BarcodeNo, int? ShortCode, decimal Quantity = 1) : IRequestDto
{
    public static BasketItem ConvertToEntity(CreateBasketItemRequestDto createBasketItemRequestDto, decimal totalPrice, int productId)
    {
        return new BasketItem()
        {
            ProductId = productId,
            Quantity = createBasketItemRequestDto.Quantity,
            TotalPrice = totalPrice,
            Created = DateTime.Now
        };
    }
}
