using Core.Persistence.DtoBaseModel;
using EntityLayer.Dtos.ResponseDtos.BasketItemResponseDtos;
using EntityLayer.Dtos.ResponseDtos.BasketPaymentResponseDtos;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.BasketResponseDtos;

public record ResultBasketResponseDto(int Id, int UserCode, List<ResultBasketItemResponseDto> BasketItems, List<ResultBasketPaymentResponseDto> PaymentTypes, DateTime Created, DateTime? Updated, DateTime? Deleted) : IResponseDto
{
    public static ResultBasketResponseDto ConvertToResponse(Basket entity)
    {
        return new ResultBasketResponseDto(Id: entity.Id,
                                           UserCode: entity.UserCode,
                                           BasketItems: entity.BasketItems.Any() ? entity.BasketItems.Select(x => ResultBasketItemResponseDto.ConvertToResponse(x)).ToList() : new List<ResultBasketItemResponseDto>(),
                                           PaymentTypes: entity.BasketPayments.Any() ? ResultBasketPaymentResponseDto.ConvertToResponse(entity.BasketPayments) : new List<ResultBasketPaymentResponseDto>(),
                                           Created: entity.Created,
                                           Updated: entity.Updated,
                                           Deleted: entity.Deleted);
    }
}
