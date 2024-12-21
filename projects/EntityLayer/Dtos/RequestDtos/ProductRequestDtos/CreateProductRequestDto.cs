using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.ProductRequestDtos;

public record CreateProductRequestDto(long BarcodeNo, short CategoryNo, int ShortCode, string Name, decimal Price, int Stock, decimal Weight, DateTime Expiration, DateTime Production) : IRequestDto
{
    public static Product ConvertToEntity(CreateProductRequestDto createProductRequestDto)
    {
        return new Product
        {
            BarcodeNo = createProductRequestDto.BarcodeNo,
            CategoryNo = createProductRequestDto.CategoryNo,
            ShortCode = createProductRequestDto.ShortCode,
            Name = createProductRequestDto.Name,
            Price = createProductRequestDto.Price,
            Stock = createProductRequestDto.Stock,
            Weight = createProductRequestDto.Weight,
            Expiration = createProductRequestDto.Expiration,
            Production = createProductRequestDto.Production,
            Created = DateTime.Now,
        };
    }
}
