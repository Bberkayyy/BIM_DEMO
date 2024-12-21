using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.UserResponseDtos;

public record ResultUserResponseDto(int Id, int StoreNo, string Name, string Surname, int UserCode, int Password, DateTime Created, DateTime? Updated, DateTime? Deleted, Store? Store)
{
    public static ResultUserResponseDto ConvertToResponsed(User entity)
    {
        return new ResultUserResponseDto(Id: entity.Id, StoreNo: entity.StoreNo, Name: entity.Name, Surname: entity.Surname, UserCode: entity.UserCode, Password: entity.Password, Created: entity.Created, Updated: entity.Updated, Deleted: entity.Deleted, Store: entity.Store);
    }
}
