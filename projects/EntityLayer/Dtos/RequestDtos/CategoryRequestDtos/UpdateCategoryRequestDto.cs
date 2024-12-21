using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.CategoryRequestDtos;

public record UpdateCategoryRequestDto(int Id, short CategoryNo, string Name, DateTime Created) : IRequestDto
{
    public static Category ConvertToEntity(UpdateCategoryRequestDto updateCategoryRequestDto)
    {
        return new Category
        {
            Id = updateCategoryRequestDto.Id,
            CategoryNo = updateCategoryRequestDto.CategoryNo,
            Name = updateCategoryRequestDto.Name,
            Created = updateCategoryRequestDto.Created,
            Updated = DateTime.Now,
        };
    }
}
