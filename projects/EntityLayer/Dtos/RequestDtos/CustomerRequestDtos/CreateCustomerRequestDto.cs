using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.CustomerRequestDtos;

public record CreateCustomerRequestDto(string IdentityNumber, string FullName, string Address, string TaxOffice, string PhoneNumber, string? Email) : IRequestDto
{
    public static Customer ConvertToEntity(CreateCustomerRequestDto createCustomerRequestDto)
    {
        return new Customer
        {
            IdentityNumber = createCustomerRequestDto.IdentityNumber,
            FullName = createCustomerRequestDto.FullName,
            Address = createCustomerRequestDto.Address,
            TaxOffice = createCustomerRequestDto.TaxOffice,
            PhoneNumber = createCustomerRequestDto.PhoneNumber,
            Email = createCustomerRequestDto.Email,
            Created = DateTime.Now
        };
    }
}
