using Core.Persistence.Repositories;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.UserRoleRepositories;

public class UserRoleRepository : EfRepositoryBase<BaseDbContext, UserRole, int>, IUserRoleRepository
{
    public UserRoleRepository(BaseDbContext context) : base(context)
    {
    }
}
