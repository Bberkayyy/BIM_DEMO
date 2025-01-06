using Core.Persistence.Repositories;
using EntityLayer.Dtos.RequestDtos.BasketItemsRequestDtos;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.BasketRepositories;

public interface IBasketRepository : IRepositoryBase<Basket, int>, IRepositoryBaseAsync<Basket, int>
{
    Task<Basket> SaveBasketAsync(Basket basket);
}
