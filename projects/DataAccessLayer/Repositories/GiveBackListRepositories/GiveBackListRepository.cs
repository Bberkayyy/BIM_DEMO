using Core.Persistence.Repositories;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.GiveBackListRepositories;

public class GiveBackListRepository : EfRepositoryBase<BaseDbContext, GiveBackList, int>, IGiveBackListRepository
{
    public GiveBackListRepository(BaseDbContext context) : base(context)
    {
    }
}
