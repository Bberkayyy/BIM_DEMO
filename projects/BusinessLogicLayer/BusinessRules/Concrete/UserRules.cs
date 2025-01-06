using BusinessLogicLayer.BusinessRules.Abstract;
using Core.CrossCuttingConcerns;
using DataAccessLayer.Repositories.StoreRepositories;
using DataAccessLayer.Repositories.UserRepositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Concrete;

public class UserRules : IUserRules
{
    private readonly IUserRepository _userRepository;
    private readonly IStoreRepository _storeRepository;

    public UserRules(IUserRepository userRepository, IStoreRepository storeRepository)
    {
        _userRepository = userRepository;
        _storeRepository = storeRepository;
    }

    public int GetStoreId(string storeNo)
    {
        return _storeRepository.GetByFilter(x => x.StoreNo == storeNo)!.Id;
    }

    public void IdentityNumberMustBeDigit(string identityNumber)
    {
        if (!identityNumber.All(char.IsDigit))
            throw new BusinessException("Identity number must consist of digit only!");
    }

    public void IdentityNumberMustBeElevenCharacter(string identityNumber)
    {
        if (identityNumber.Length != 11)
            throw new BusinessException($"Identity number must be eleven character! ({identityNumber.Length})");
    }

    public void IdentityNumberMustBeUnique(string identityNumber, int id = -1)
    {
        User? user = _userRepository.GetByFilter(x => x.IdentityNumber == identityNumber);
        if (user != null && user.Id != id)
        {
            if (user.Id != id)
                throw new BusinessException("User id cannot be changed!");
            throw new BusinessException($"A user with the identity number {identityNumber} exists!");
        }
    }

    public void StoreExists(string storeNo)
    {
        Store? store = _storeRepository.GetByFilter(x => x.StoreNo == storeNo);
        if (store == null)
            throw new BusinessException("Store not found!");
    }

    public void UserCodeMustBeSixCharacter(int userCode)
    {
        if (userCode < 100000 || userCode > 999999)
            throw new BusinessException($"User code must be between 100000 and 999999. Please enter a number in this range. ({userCode})");
    }

    public bool UserCodeMustBeUnique(int userCode)
    {
        return _userRepository.EntityExists(x => x.UserCode == userCode);
    }

    public void UserCodeMustBeUnique(int userCode, int id = -1)
    {
        User? user = _userRepository.GetByFilter(x => x.UserCode == userCode);
        if (user != null && user.Id != id)
        {
            if (user.Id != id)
                throw new BusinessException("User id cannot be changed!");
            throw new BusinessException($"A user with the user code {userCode} exists!");
        }
    }

    public void UserExists(User? user, bool isDeletedFromDatabase = false)
    {
        if (user == null)
            throw new BusinessException("User not found!");
        if (!isDeletedFromDatabase && user.Deleted != null)
            throw new BusinessException($"User not found! User deleted at {user.Deleted?.ToString("yyyy-MM-dd HH:mm")}.");
    }
}
