using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Abstract;

public interface IRoleRules
{
    void NameCannotBeNullOrWhiteSpace(string name);
    void NameMustBeUnique(string name);
    void NameMustBeUnique(string name, int id = -1);
    void RoleExists(Role? role, bool isDeletedFromDatabase = false);
}
