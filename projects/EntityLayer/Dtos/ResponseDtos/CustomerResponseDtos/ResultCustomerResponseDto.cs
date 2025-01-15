using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.CustomerResponseDtos;

public record ResultCustomerResponseDto(int Id, string IdentityNumber, string FullName, string Address, string TaxOffice, string PhoneNumber, string? Email, DateTime Created) : IResponseDto
{
    public static ResultCustomerResponseDto ConvertToResponse(Customer entity)
    {
        return new ResultCustomerResponseDto(
            Id: entity.Id,
            IdentityNumber: entity.IdentityNumber,
            FullName: entity.FullName,
            Address: entity.Address,
            TaxOffice: entity.TaxOffice,
            PhoneNumber: entity.PhoneNumber,
            Email: entity.Email,
            Created: entity.Created);
    }
}
