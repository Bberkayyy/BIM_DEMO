using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.UserRequestDtos;

public record UpdateUserRequestDto(int Id, int StoreNo, string Name, string Surname, int UserCode, int Password, DateTime Created)
{
    public static User ConvertToEntity(UpdateUserRequestDto updateUserRequestDto)
    {
        return new User()
        {
            Id = updateUserRequestDto.Id,
            StoreNo = updateUserRequestDto.StoreNo,
            Name = updateUserRequestDto.Name,
            Surname = updateUserRequestDto.Surname,
            UserCode = updateUserRequestDto.UserCode,
            Password = updateUserRequestDto.Password,
            Created = updateUserRequestDto.Created,
            Updated = DateTime.Now
        };
    }
}
