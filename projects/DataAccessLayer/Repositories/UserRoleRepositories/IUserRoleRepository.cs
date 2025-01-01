using Core.Persistence.Repositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.UserRoleRepositories;

public interface IUserRoleRepository : IRepositoryBase<UserRole, int>, IRepositoryBaseAsync<UserRole, int>
{
}
