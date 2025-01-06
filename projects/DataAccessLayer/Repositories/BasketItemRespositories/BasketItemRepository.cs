using Core.Persistence.Repositories;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.BasketItemRespositories;

public class BasketItemRepository : EfRepositoryBase<BaseDbContext, BasketItem, int>, IBasketItemRepository
{
    public BasketItemRepository(BaseDbContext context) : base(context)
    {
    }
}
