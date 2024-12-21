using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.CategoryRequestDtos;

public record CreateCategoryRequestDto(short CategoryNo, string Name) : IRequestDto
{
    public static Category ConvertToEntity(CreateCategoryRequestDto createCategoryRequestDto)
    {
        return new Category()
        {
            CategoryNo = createCategoryRequestDto.CategoryNo,
            Name = createCategoryRequestDto.Name,
            Created = DateTime.Now,
        };
    }
}
