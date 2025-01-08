using Core.Persistence.Repositories;
using DataAccessLayer.Context;
using EntityLayer.Dtos.RequestDtos.BasketItemsRequestDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.BasketRepositories;

public class BasketRepository : EfRepositoryBase<BaseDbContext, Basket, int>, IBasketRepository
{
    public BasketRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<Basket> SaveBasketAsync(Basket basket)
    {
        using IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync();
        try
        {
            await Context.Set<Basket>().AddAsync(basket);
            await Context.SaveChangesAsync();

            foreach (var item in basket.BasketItems)
            {
                item.BasketId = basket.Id;
                item.Deleted = DateTime.Now;
            }
            Context.Set<BasketItem>().UpdateRange(basket.BasketItems);
            await Context.SaveChangesAsync();

            await transaction.CommitAsync();

            //yapılan ödeme türüne göre pos güncellenecek.

            return basket;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
