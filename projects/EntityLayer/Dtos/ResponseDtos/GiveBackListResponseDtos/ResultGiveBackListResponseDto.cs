using Core.Persistence.DtoBaseModel;
using EntityLayer.Dtos.ResponseDtos.ProductResponseDtos;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.GiveBackListResponseDtos;

public record ResultGiveBackListResponseDto(int Id, string CustomerIdentityNumber, string CustomerFullName, ResultProductResponseDto? Product, string ReasonForReturn, bool IsReturnCash, decimal ProductQuantity, decimal ProductPrice, DateTime Created) : IResponseDto
{
    public static ResultGiveBackListResponseDto ConvertToResponse(GiveBackList entity)
    {
        return new ResultGiveBackListResponseDto(Id: entity.Id,
                                                 CustomerIdentityNumber: entity.CustomerIdentityNumber,
                                                 CustomerFullName: entity.Customer.FullName,
                                                 Product: entity.Product != null ? ResultProductResponseDto.ConvertToResponse(entity.Product) : null,
                                                 ReasonForReturn: entity.ReasonForReturn,
                                                 IsReturnCash: entity.IsReturnCash,
                                                 ProductQuantity: entity.ProductQuantity,
                                                 ProductPrice: entity.ProductPrice,
                                                 Created: entity.Created);
    }
}
