using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.UserRoleRequestDtos;

public record CreateUserRoleRequestDto(int UserId, int RoleId) : IRequestDto
{
    public static UserRole ConvertToEntity(CreateUserRoleRequestDto createUserRoleRequestDto)
    {
        return new UserRole()
        {
            UserId = createUserRoleRequestDto.UserId,
            RoleId = createUserRoleRequestDto.RoleId,
            Created = DateTime.Now
        };
    }
}
