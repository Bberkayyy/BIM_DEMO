using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.CategoryResponseDtos;

public record ResultCategoryResponseDto(int Id, short CategoryNo, string Name, DateTime Created, DateTime? Updated, DateTime? Deleted, int ProductCount)
{
    public static ResultCategoryResponseDto ConvertToResponse(Category entity)
    {
        return new ResultCategoryResponseDto(Id: entity.Id,
                                             CategoryNo: entity.CategoryNo,
                                             Name: entity.Name,
                                             Created: entity.Created,
                                             Updated: entity.Updated,
                                             Deleted: entity.Deleted,
                                             ProductCount: entity.Products.Count);
    }
}
