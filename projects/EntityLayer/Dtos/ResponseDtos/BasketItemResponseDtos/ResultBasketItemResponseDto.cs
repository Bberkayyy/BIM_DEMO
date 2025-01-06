using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.BasketItemResponseDtos;

public record ResultBasketItemResponseDto(long BarcodeNo, int ShortCode, string Name, decimal Price, decimal Quantity, decimal TotalPrice) : IResponseDto
{
    public static ResultBasketItemResponseDto ConvertToResponse(BasketItem entity)
    {
        return new ResultBasketItemResponseDto(BarcodeNo: entity.Product.BarcodeNo,
                                               ShortCode: entity.Product.ShortCode,
                                               Name: entity.Product.Name,
                                               Price: entity.Product.Price,
                                               Quantity: entity.Quantity,
                                               TotalPrice: entity.TotalPrice);
    }
}
