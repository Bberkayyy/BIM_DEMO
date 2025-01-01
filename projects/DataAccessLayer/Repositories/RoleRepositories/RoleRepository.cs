using Core.Persistence.Repositories;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.RoleRepositories;

public class RoleRepository : EfRepositoryBase<BaseDbContext, Role, int>, IRoleRepository
{
    public RoleRepository(BaseDbContext context) : base(context)
    {
    }
}
