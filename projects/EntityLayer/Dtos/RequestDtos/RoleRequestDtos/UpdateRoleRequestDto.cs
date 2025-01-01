using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.RoleRequestDtos;

public record UpdateRoleRequestDto(int Id, string Name, DateTime Created) : IRequestDto
{
    public static Role ConvertToEntity(UpdateRoleRequestDto updateRoleRequestDto)
    {
        return new Role
        {
            Id = updateRoleRequestDto.Id,
            Name = updateRoleRequestDto.Name,
            Created = updateRoleRequestDto.Created,
            Updated = DateTime.Now
        };
    }
}
