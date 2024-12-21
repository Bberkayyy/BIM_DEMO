using Core.Persistence.Repositories;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.ProductRepositories;

public class ProductRepository : EfRepositoryBase<BaseDbContext, Product, int>, IProductRepository
{
    public ProductRepository(BaseDbContext context) : base(context)
    {
    }
}
