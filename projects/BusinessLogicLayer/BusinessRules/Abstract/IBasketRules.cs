using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Abstract;

public interface IBasketRules
{
    void BasketExists(Basket? basket);
    void BasketItemsExists(List<BasketItem> basketItems);
    void UserExists(int userCode);
}
