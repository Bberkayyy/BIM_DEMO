using BusinessLogicLayer.BusinessRules.Abstract;
using Core.CrossCuttingConcerns;
using DataAccessLayer.Repositories.PointOfSaleRepositories;
using DataAccessLayer.Repositories.TillRepositories;
using DataAccessLayer.Repositories.UserRepositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Concrete;

public class PointOfSaleRules : IPointOfSaleRules
{
    private readonly ITillRepository _tillRepository;
    private readonly IUserRepository _userRepository;


    public PointOfSaleRules(ITillRepository tillRepository, IUserRepository userRepository)
    {
        _tillRepository = tillRepository;
        _userRepository = userRepository;
    }

    public void PointOfSaleExists(PointOfSale pointOfSale)
    {
        if (pointOfSale == null)
            throw new BusinessException("Pos not found!");
    }

    public void TillExists(int tillId)
    {
        Till? till = _tillRepository.GetById(tillId);
        if (till == null)
            throw new BusinessException("Till not found!");
    }

    public void UserExists(int userCode)
    {
        User? user = _userRepository.GetByFilter(x => x.UserCode == userCode);
        if (user == null)
            throw new BusinessException("User not found!");
    }
}
