using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.UserRequestDtos;

public record CreateUserRequestDto(int StoreNo, string Name, string Surname, int UserCode, int Password)
{
    public static User ConvertToEntity(CreateUserRequestDto createUserRequestDto)
    {
        return new User()
        {
            StoreNo = createUserRequestDto.StoreNo,
            Name = createUserRequestDto.Name,
            Surname = createUserRequestDto.Surname,
            UserCode = createUserRequestDto.UserCode,
            Password = createUserRequestDto.Password,
            Created = DateTime.Now,
        };
    }
}
