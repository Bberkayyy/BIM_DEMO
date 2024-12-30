using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.StoreRequestDtos;

public record CreateStoreRequestDto(string City, string Name, string Address, string PhoneNumber)
{
    public static Store ConvertToEntity(CreateStoreRequestDto createStoreRequestDto, string storeNo)
    {
        return new Store()
        {
            City = createStoreRequestDto.City,
            StoreNo = storeNo,
            Name = createStoreRequestDto.Name,
            Address = createStoreRequestDto.Address,
            PhoneNumber = createStoreRequestDto.PhoneNumber,
            Created = DateTime.Now,
        };
    }
}
