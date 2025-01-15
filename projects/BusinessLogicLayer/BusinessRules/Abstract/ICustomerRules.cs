using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Abstract;

public interface ICustomerRules
{
    void CustomerExists(Customer? customer);
    void IdentityNumberMustBeDigitAndElevenCharacter(string identityNumber);
    void PhoneNumberMustBeDigitAndTenCharacter(string phoneNumber);
}
