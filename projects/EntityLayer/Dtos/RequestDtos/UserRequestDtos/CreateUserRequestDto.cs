using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.UserRequestDtos;

public record CreateUserRequestDto(int StoreId, string Name, string Surname, string IdentityNumber)
{
    public static User ConvertToEntity(CreateUserRequestDto createUserRequestDto, int userCode, int password)
    {
        return new User()
        {
            StoreId = createUserRequestDto.StoreId,
            Name = createUserRequestDto.Name,
            Surname = createUserRequestDto.Surname,
            IdentityNumber = createUserRequestDto.IdentityNumber,
            UserCode = userCode,
            Password = password,
            Created = DateTime.Now,
        };
    }
}
