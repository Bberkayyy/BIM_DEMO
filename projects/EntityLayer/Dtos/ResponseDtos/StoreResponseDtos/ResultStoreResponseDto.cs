using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.StoreResponseDtos;

public record ResultStoreResponseDto(int Id, string City, string StoreNo, string Name, string Address, string PhoneNumber, DateTime Created, DateTime? Updated, DateTime? Deleted, int UserCount)
{
    public static ResultStoreResponseDto ConvertToResponse(Store entity)
    {
        return new ResultStoreResponseDto(Id: entity.Id,
                                          City: entity.City,
                                          StoreNo: entity.StoreNo,
                                          Name: entity.Name,
                                          Address: entity.Address,
                                          PhoneNumber: entity.PhoneNumber,
                                          Created: entity.Created,
                                          Updated: entity.Updated,
                                          Deleted: entity.Deleted,
                                          UserCount: entity.Users.Count);
    }
}
