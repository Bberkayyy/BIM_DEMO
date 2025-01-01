using BusinessLogicLayer.BusinessRules.Abstract;
using Core.CrossCuttingConcerns;
using DataAccessLayer.Repositories.RoleRepositories;
using DataAccessLayer.Repositories.UserRepositories;
using DataAccessLayer.Repositories.UserRoleRepositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Concrete;

public class UserRoleRules : IUserRoleRules
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public UserRoleRules(IRoleRepository roleRepository, IUserRepository userRepository, IUserRoleRepository userRoleRepository)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
    }

    public void RoleExists(int roleId)
    {
        Role? role = _roleRepository.GetById(roleId);
        if (role == null)
            throw new BusinessException("Role not found!");
    }

    public void UserExists(int userId)
    {
        User? user = _userRepository.GetById(userId);
        if (user == null)
            throw new BusinessException("User not found!");
    }

    public void UserRoleExists(UserRole? userRole)
    {
        if (userRole == null)
            throw new BusinessException("User role not fount!");
    }
}
