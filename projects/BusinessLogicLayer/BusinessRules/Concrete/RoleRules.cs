using BusinessLogicLayer.BusinessRules.Abstract;
using Core.CrossCuttingConcerns;
using DataAccessLayer.Repositories.RoleRepositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Concrete;

public class RoleRules : IRoleRules
{
    private readonly IRoleRepository _roleRepository;

    public RoleRules(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public void NameCannotBeNullOrWhiteSpace(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessException("Please enter a role name!");
    }

    public void NameMustBeUnique(string name)
    {
        Role? role = _roleRepository.GetByFilter(x => x.Name.ToLower() == name.ToLower());
        if (role != null)
            throw new BusinessException($"Role name is already exits! ({name}). Enter a different role name.");
    }

    public void NameMustBeUnique(string name, int id = -1)
    {
        Role? role = _roleRepository.GetByFilter(x => x.Name.ToLower() == name.ToLower());
        if (role != null && role.Id != id)
        {
            if (role.Id != id)
                throw new BusinessException($"Role id cannot be changed!");
            if (role.Name == name)
                throw new BusinessException($"Role name is already exists! ({name}). Enter a different role name");
        }
    }

    public void RoleExists(Role? role, bool isDeletedFromDatabase = false)
    {
        if (role == null)
            throw new BusinessException("Role not found!");
        if (!isDeletedFromDatabase && role.Deleted != null)
            throw new BusinessException($"Role not found! Role deleted at {role.Deleted?.ToString("yyyy-MM-dd HH:mm")}.");
    }
}
