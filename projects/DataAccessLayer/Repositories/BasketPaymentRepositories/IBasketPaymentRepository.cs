using Core.Persistence.Repositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.BasketPaymentRepositories;

public interface IBasketPaymentRepository : IRepositoryBase<BasketPayment, int>, IRepositoryBaseAsync<BasketPayment, int>
{
}
