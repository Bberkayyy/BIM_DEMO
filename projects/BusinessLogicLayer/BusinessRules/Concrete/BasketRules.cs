using BusinessLogicLayer.BusinessRules.Abstract;
using Core.CrossCuttingConcerns;
using DataAccessLayer.Repositories.UserRepositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Concrete;

public class BasketRules : IBasketRules
{
    private readonly IUserRepository _userRepository;

    public BasketRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void BasketExists(Basket? basket)
    {
        if (basket == null)
            throw new BusinessException("Basket not found!");
    }

    public void BasketItemsExists(List<BasketItem> basketItems)
    {
        if (!basketItems.Any())
            throw new BusinessException("Basket items not found!");
    }

    public void UserExists(int userCode)
    {
        User? user = _userRepository.GetByFilter(x => x.UserCode == userCode);
        if (user == null)
            throw new BusinessException("User not found!");
    }
}
