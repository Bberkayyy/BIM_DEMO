using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.ProductRequestDtos;

public record UpdateProductRequestDto(int Id, long BarcodeNo, int CategoryId, int ShortCode, string Name, decimal Price, int Stock, decimal? Weight, DateTime? Expiration, DateTime? Production, DateTime Created) : IRequestDto
{
    public static Product ConvertToEntity(UpdateProductRequestDto updateProductRequestDto)
    {
        return new Product
        {
            Id = updateProductRequestDto.Id,
            BarcodeNo = updateProductRequestDto.BarcodeNo,
            CategoryId = updateProductRequestDto.CategoryId,
            ShortCode = updateProductRequestDto.ShortCode,
            Name = updateProductRequestDto.Name,
            Price = updateProductRequestDto.Price,
            Stock = updateProductRequestDto.Stock,
            Weight = updateProductRequestDto.Weight,
            Expiration = updateProductRequestDto.Expiration,
            Production = updateProductRequestDto.Production,
            Created = updateProductRequestDto.Created,
            Updated = DateTime.Now,
        };
    }
}
