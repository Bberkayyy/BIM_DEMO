using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.StoreRequestDtos;

public record CreateStoreRequestDto(int StoreNo, string Name, string Address, int PhoneNumber, DateTime Created)
{
    public static Store ConvertToEntity(CreateStoreRequestDto createStoreRequestDto)
    {
        return new Store()
        {
            StoreNo = createStoreRequestDto.StoreNo,
            Name = createStoreRequestDto.Name,
            Address = createStoreRequestDto.Address,
            PhoneNumber = createStoreRequestDto.PhoneNumber,
            Created = createStoreRequestDto.Created
        };
    }
}
