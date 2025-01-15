using Core.Persistence.Repositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.GiveBackListRepositories;

public interface IGiveBackListRepository : IRepositoryBase<GiveBackList, int>, IRepositoryBaseAsync<GiveBackList, int>
{
}
