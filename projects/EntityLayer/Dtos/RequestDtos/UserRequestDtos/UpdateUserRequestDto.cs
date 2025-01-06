using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.UserRequestDtos;

public record UpdateUserRequestDto(int Id, string StoreNo, string Name, string Surname, string IdentityNumber, int UserCode, int Password, DateTime Created)
{
    public static User ConvertToEntity(UpdateUserRequestDto updateUserRequestDto, int storeId)
    {
        return new User()
        {
            Id = updateUserRequestDto.Id,
            StoreId = storeId,
            Name = updateUserRequestDto.Name,
            Surname = updateUserRequestDto.Surname,
            IdentityNumber = updateUserRequestDto.IdentityNumber,
            UserCode = updateUserRequestDto.UserCode,
            Password = updateUserRequestDto.Password,
            Created = updateUserRequestDto.Created,
            Updated = DateTime.Now
        };
    }
}
