using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Abstract;

public interface IUserRoleRules
{
    void UserExists(int userId);
    void RoleExists(int roleId);
    void UserRoleExists(UserRole? userRole);
}
