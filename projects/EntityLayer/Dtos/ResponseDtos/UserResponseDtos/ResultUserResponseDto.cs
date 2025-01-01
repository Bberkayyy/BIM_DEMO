using EntityLayer.Dtos.ResponseDtos.StoreResponseDtos;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.UserResponseDtos;

public record ResultUserResponseDto(int Id, string Name, string Surname, string IdentityNumber, int UserCode, int Password, DateTime Created, DateTime? Updated, DateTime? Deleted, ResultStoreResponseDto? Store)
{
    public static ResultUserResponseDto ConvertToResponsed(User entity)
    {
        return new ResultUserResponseDto(Id: entity.Id,
                                         Name: entity.Name,
                                         Surname: entity.Surname,
                                         IdentityNumber: entity.IdentityNumber,
                                         UserCode: entity.UserCode,
                                         Password: entity.Password,
                                         Created: entity.Created,
                                         Updated: entity.Updated,
                                         Deleted: entity.Deleted,
                                         Store: entity.Store != null ? ResultStoreResponseDto.ConvertToResponse(entity.Store) : null);
    }
}
