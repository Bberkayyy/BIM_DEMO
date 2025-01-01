using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Abstract;

public interface IUserRules
{
    void StoreExists(int storeId);
    void IdentityNumberMustBeUnique(string identityNumber, int id = -1);
    void IdentityNumberMustBeElevenCharacter(string identityNumber);
    void IdentityNumberMustBeDigit(string identityNumber);
    void UserExists(User? user, bool isDeletedFromDatabase = false);
    bool UserCodeMustBeUnique(int userCode);
    void UserCodeMustBeUnique(int userCode, int id = -1);
    void UserCodeMustBeSixCharacter(int userCode);
}
