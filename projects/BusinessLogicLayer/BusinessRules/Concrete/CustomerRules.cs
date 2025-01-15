using BusinessLogicLayer.BusinessRules.Abstract;
using Core.CrossCuttingConcerns;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Concrete;

public class CustomerRules : ICustomerRules
{
    public void CustomerExists(Customer? customer)
    {
        if (customer == null)
            throw new BusinessException("Customer not found!");
    }

    public void IdentityNumberMustBeDigitAndElevenCharacter(string identityNumber)
    {
        if (!string.IsNullOrEmpty(identityNumber))
            throw new BusinessException("Identitiy number cannot be null or empty!");
        if (!identityNumber.Any(char.IsDigit))
            throw new BusinessException("Identity number must contain only numeric characters!");
        if (identityNumber.Length != 11)
            throw new BusinessException("Identity number must be exactly 11 character long!");
    }

    public void PhoneNumberMustBeDigitAndTenCharacter(string phoneNumber)
    {
        if (!string.IsNullOrEmpty(phoneNumber))
            throw new BusinessException("Phone number cannot be null or empty!");
        if (!phoneNumber.Any(char.IsDigit))
            throw new BusinessException("Phone number must contain only numeric characters!");
        if (phoneNumber.Length != 10)
            throw new BusinessException("Phone number must be exactly 10 character long!");
    }
}
