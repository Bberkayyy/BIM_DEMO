using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.ProductRequestDtos;

public record CreateProductRequestDto(int CategoryId, string Name, decimal Price, int Stock, decimal? Weight, DateTime? Expiration, DateTime? Production) : IRequestDto
{
    public static Product ConvertToEntity(CreateProductRequestDto createProductRequestDto, int shortCode, long barcodeNo)
    {
        return new Product
        {
            BarcodeNo = barcodeNo,
            CategoryId = createProductRequestDto.CategoryId,
            ShortCode = shortCode,
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
