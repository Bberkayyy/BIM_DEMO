using Core.Persistence.Repositories;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.UserRepositories;

public class UserRepository : EfRepositoryBase<BaseDbContext, User, int>, IUserRepository
{
    public UserRepository(BaseDbContext context) : base(context)
    {
    }
}
