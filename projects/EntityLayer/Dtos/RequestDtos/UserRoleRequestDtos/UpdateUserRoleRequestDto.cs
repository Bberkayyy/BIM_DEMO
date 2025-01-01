using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.UserRoleRequestDtos;

public record UpdateUserRoleRequestDto(int Id, int UserId, int RoleId, DateTime Created) : IRequestDto
{
    public static UserRole ConvertToEntity(UpdateUserRoleRequestDto updateUserRoleRequestDto)
    {
        return new UserRole()
        {
            Id = updateUserRoleRequestDto.Id,
            UserId = updateUserRoleRequestDto.UserId,
            RoleId = updateUserRoleRequestDto.RoleId,
            Created = updateUserRoleRequestDto.Created,
            Updated = DateTime.Now
        };
    }
}
