using Core.Persistence.DtoBaseModel;
using EntityLayer.Dtos.ResponseDtos.RoleResponseDtos;
using EntityLayer.Dtos.ResponseDtos.UserResponseDtos;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.UserRoleResponseDtos;

public record ResultUserRoleResponseDto(int Id, ResultUserResponseDto User, ResultRoleResponseDto Role, DateTime Created, DateTime? Updated, DateTime? Deleted) : IResponseDto
{
    public static ResultUserRoleResponseDto ConvertToResponse(UserRole entity)
    {
        return new ResultUserRoleResponseDto(
            Id: entity.Id,
            User: ResultUserResponseDto.ConvertToResponsed(entity.User),
            Role: ResultRoleResponseDto.ConvertToResponse(entity.Role),
            Created: entity.Created,
            Updated: entity.Updated,
            Deleted: entity.Deleted);
    }
}
