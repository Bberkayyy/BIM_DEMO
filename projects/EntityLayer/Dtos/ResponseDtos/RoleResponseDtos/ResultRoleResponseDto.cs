using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.RoleResponseDtos;

public record ResultRoleResponseDto(int Id, string Name, DateTime Created, DateTime? Updated, DateTime? Deleted) : IResponseDto
{
    public static ResultRoleResponseDto ConvertToResponse(Role entity)
    {
        return new ResultRoleResponseDto(Id: entity.Id,
                                         Name: entity.Name,
                                         Created: entity.Created,
                                         Updated: entity.Updated,
                                         Deleted: entity.Deleted);
    }
}
