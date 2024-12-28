using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.CategoryRequestDtos;

public record CreateCategoryRequestDto(string Name) : IRequestDto
{
    public static Category ConvertToEntity(CreateCategoryRequestDto createCategoryRequestDto, short categoryNo)
    {
        return new Category()
        {
            CategoryNo = categoryNo,
            Name = createCategoryRequestDto.Name,
            Created = DateTime.Now,
        };
    }
}
