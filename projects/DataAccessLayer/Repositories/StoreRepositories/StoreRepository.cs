using Core.Persistence.Repositories;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.StoreRepositories;

public class StoreRepository : EfRepositoryBase<BaseDbContext, Store, int>, IStoreRepository
{
    public StoreRepository(BaseDbContext context) : base(context)
    {
    }
}
