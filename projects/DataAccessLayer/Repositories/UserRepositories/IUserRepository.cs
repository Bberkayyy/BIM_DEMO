using Core.Persistence.Repositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.UserRepositories;

public interface IUserRepository : IRepositoryBase<User, int>, IRepositoryBaseAsync<User, int>
{
}
