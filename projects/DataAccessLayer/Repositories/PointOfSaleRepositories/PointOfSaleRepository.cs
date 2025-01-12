using Core.Persistence.Repositories;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.PointOfSaleRepositories;

public class PointOfSaleRepository : EfRepositoryBase<BaseDbContext, PointOfSale, int>, IPointOfSaleRepository
{
    public PointOfSaleRepository(BaseDbContext context) : base(context)
    {
    }
}
