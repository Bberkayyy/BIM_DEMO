using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.StoreRequestDtos;

public record UpdateStoreRequestDto(int Id, int StoreNo, string Name, string Address, int PhoneNumber, DateTime Created)
{
    public static Store ConvertToEntity(UpdateStoreRequestDto updateStoreRequestDto)
    {
        return new Store()
        {
            Id = updateStoreRequestDto.Id,
            StoreNo = updateStoreRequestDto.StoreNo,
            Name = updateStoreRequestDto.Name,
            Address = updateStoreRequestDto.Address,
            PhoneNumber = updateStoreRequestDto.PhoneNumber,
            Created = updateStoreRequestDto.Created,
            Updated = DateTime.Now
        };
    }
}
