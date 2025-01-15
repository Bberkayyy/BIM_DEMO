using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.GiveBackListRequestDtos;

public record CreateGiveBackListRequestDto(string CustomerIdentityNumber, long ProductBarcodeNo, string ReasonForReturn, bool IsReturnCash, decimal ProductQuantity, decimal ProductPrice) : IRequestDto
{
    public static GiveBackList ConvertToEntity(CreateGiveBackListRequestDto createGiveBackListDto)
    {
        return new GiveBackList
        {
            CustomerIdentityNumber = createGiveBackListDto.CustomerIdentityNumber,
            ProductBarcodeNo = createGiveBackListDto.ProductBarcodeNo,
            ReasonForReturn = createGiveBackListDto.ReasonForReturn,
            IsReturnCash = createGiveBackListDto.IsReturnCash,
            ProductQuantity = createGiveBackListDto.ProductQuantity,
            ProductPrice = createGiveBackListDto.ProductPrice,
            Created = DateTime.Now
        };
    }
}
