using Core.Persistence.Repositories;
using DataAccessLayer.Context;
using EntityLayer.Dtos.RequestDtos.BasketItemsRequestDtos;
using EntityLayer.Entities;
using EntityLayer.Enums;
using Microsoft.EntityFrameworkCore;
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

            foreach (var payment in basket.BasketPayments)
            {
                payment.BasketId = basket.Id;
            }
            await Context.Set<BasketPayment>().AddRangeAsync(basket.BasketPayments);
            await Context.SaveChangesAsync();

            PointOfSale? pointOfSale = await Context.Set<PointOfSale>()
            .FirstOrDefaultAsync(p => p.UserCode == basket.UserCode);

            if (pointOfSale != null)
            {
                foreach (var payment in basket.BasketPayments)
                {
                    switch (payment.PaymentType)
                    {
                        case PaymentType.Cash:
                            pointOfSale.CashPaymentTotal += payment.Amount;
                            break;
                        case PaymentType.CreditCart:
                            pointOfSale.CreditCardPaymentTotal += payment.Amount;
                            break;
                        case PaymentType.GiftCard:
                            pointOfSale.GiftCardPaymentTotal += payment.Amount;
                            break;
                        case PaymentType.Other:
                            pointOfSale.OtherPaymentTotal += payment.Amount;
                            break;
                    }
                }
                Context.Set<PointOfSale>().Update(pointOfSale);
                await Context.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            return basket;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
