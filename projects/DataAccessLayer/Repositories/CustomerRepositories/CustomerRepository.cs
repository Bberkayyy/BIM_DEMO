using Core.Persistence.Repositories;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.CustomerRepositories;

public class CustomerRepository : EfRepositoryBase<BaseDbContext, Customer, int>, ICustomerRepository
{
    public CustomerRepository(BaseDbContext context) : base(context)
    {
    }
}
