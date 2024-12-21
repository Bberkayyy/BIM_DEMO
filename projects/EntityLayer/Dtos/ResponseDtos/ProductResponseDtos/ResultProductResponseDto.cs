using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.ProductResponseDtos;

public record ResultProductResponseDto(int Id, long BarcodeNo, short CategoryNo, int ShortCode, string Name, decimal Price, int Stock, decimal? Weight, DateTime Expiration, DateTime Production, DateTime Created, DateTime? Updated, DateTime? Deleted, Category? Category)
{
    public static ResultProductResponseDto ConvertToResponse(Product entity)
    {
        return new ResultProductResponseDto(Id: entity.Id,
                                            BarcodeNo: entity.BarcodeNo,
                                            CategoryNo: entity.CategoryNo,
                                            ShortCode: entity.ShortCode,
                                            Name: entity.Name,
                                            Price: entity.Price,
                                            Stock: entity.Stock,
                                            Weight: entity.Weight,
                                            Expiration: entity.Expiration,
                                            Production: entity.Production,
                                            Created: entity.Created,
                                            Updated: entity.Updated,
                                            Deleted: entity.Deleted,
                                            Category: entity.Category);
    }
}
