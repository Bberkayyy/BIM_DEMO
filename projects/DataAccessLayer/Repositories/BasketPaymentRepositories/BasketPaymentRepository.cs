using Core.Persistence.Repositories;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.BasketPaymentRepositories;

public class BasketPaymentRepository : EfRepositoryBase<BaseDbContext, BasketPayment, int>, IBasketPaymentRepository
{
    public BasketPaymentRepository(BaseDbContext context) : base(context)
    {
    }
}
