using Core.Persistence.Repositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.ProductRepositories;

public interface IProductRepository : IRepositoryBase<Product, int>, IRepositoryBaseAsync<Product, int>
{
}
