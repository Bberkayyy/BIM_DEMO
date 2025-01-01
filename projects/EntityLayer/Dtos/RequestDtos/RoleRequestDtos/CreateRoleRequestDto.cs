using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.RoleRequestDtos;

public record CreateRoleRequestDto(string Name) : IRequestDto
{
    public static Role ConvertToEntity(CreateRoleRequestDto createRoleRequestDto)
    {
        return new Role
        {
            Name = createRoleRequestDto.Name,
            Created = DateTime.Now,
        };
    }
}
