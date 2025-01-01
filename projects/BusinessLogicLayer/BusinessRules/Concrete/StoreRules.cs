using BusinessLogicLayer.BusinessRules.Abstract;
using BusinessLogicLayer.Extensions.Enums;
using Core.CrossCuttingConcerns;
using DataAccessLayer.Repositories.StoreRepositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Concrete;

public class StoreRules : IStoreRules
{
    private readonly IStoreRepository _storeRepository;

    public StoreRules(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public void IsStoreNoValid(string storeNo, string city)
    {
        string cityCodeInStoreNo = storeNo[..2];
        int cityCodeFromEnum = GetCityCode(city);

        if (cityCodeFromEnum.ToString() != cityCodeInStoreNo)
            throw new BusinessException($"The entered city name does not match the store no. Store no should start with city code!({cityCodeInStoreNo}-{cityCodeFromEnum})");
    }

    public void PhoneNumberMustBeElevenCharacter(string phoneNumber)
    {
        if (phoneNumber.Length != 11)
            throw new BusinessException("Phone number must be 11 character.");
    }

    public void AddressMustBeMinTwentyCharacter(string address)
    {
        if (address.Length <= 20)
            throw new BusinessException($"Store address length must be min twenty character. Enter a different store address! ({address})");
    }

    public void StoreExists(Store? store, bool isDeleteFromDatabase = false)
    {
        if (store == null)
            throw new BusinessException("Store not found!");
        if (!isDeleteFromDatabase && store.Deleted != null)
            throw new BusinessException($"Store not found! Store deleted at {store.Deleted?.ToString("yyyy-MM-dd HH:mm")}.");
    }

    public void NameMustBeUnique(string name, int id = -1)
    {
        Store? store = _storeRepository.GetByFilter(x => x.Name.ToLower() == name.ToLower());
        if (store != null && store.Id != id)
        {
            if (store.Id != id)
                throw new BusinessException("Store id cannot be changed!");
            throw new BusinessException($"Store name is already exists!({name}). Enter a different store name.");
        }
    }

    public void StoreNoMustBeSixCharacter(string storeNo)
    {
        if (storeNo.Length != 6)
            throw new BusinessException($"Store no must be six character! ({storeNo.Length})");
    }

    public bool StoreNoMustBeUnique(string storeNo)
    {
        return _storeRepository.EntityExists(x => x.StoreNo == storeNo);
    }

    public void StoreNoMustBeUnique(string storeNo, int id = -1)
    {
        Store? store = _storeRepository.GetByFilter(x => x.StoreNo == storeNo);
        if (store != null && store.Id != id)
        {
            if (store.Id != id)
                throw new BusinessException($"Store id cannot be changed!");
            if (store.StoreNo == storeNo)
                throw new BusinessException($"Store no is already exists! ({store}). Enter a different store no.");
        }
    }

    public void StoreUserMustBeZeroWhenStoreIsDeleted(int storeUser, ICollection<User> users)
    {
        if (storeUser > 0)
        {
            string userNames = string.Join("-", users.Select(x => x.Name));
            throw new BusinessException($"Store user count must be zero when store is deleted. Please transfer the users to another store! ({userNames})");
        }
    }

    public void NameCannotBeNullOrWhiteSpace(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessException("Please enter a store name!");
    }

    public void AddressCannotBeNullOrWhiteSpace(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new BusinessException("Please enter a store address!");
    }

    public void PhoneNumberCannotBeNullOrWhiteSpace(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new BusinessException("Please enter a store phone number!");
    }

    public void CityCannotBeNullOrWhiteSpace(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new BusinessException("Please enter a city name!");
    }

    public int GetCityCode(string city)
    {
        string cityToSearch = city.Trim().ToLower();

        CityCodes? cityCodeExists = cityAliases.ContainsKey(cityToSearch) ? cityAliases[cityToSearch] : Enum.GetValues<CityCodes>()
          .Cast<CityCodes>()
          .FirstOrDefault(c => string.Equals(c.ToString(), cityToSearch, StringComparison.OrdinalIgnoreCase));

        if (cityCodeExists == null)
            throw new BusinessException("Enter a valid city name!");
        if (cityCodeExists.HasValue)
            return (int)cityCodeExists.Value;
        throw new BusinessException("Enter a valid city name!");
    }

    public void StoreNoMustBeDigit(string storeNo)
    {
        if (!storeNo.All(char.IsDigit))
            throw new BusinessException("Store no must consist of digit only!");
    }

    public void PhoneNumberMustBeDigit(string phoneNumber)
    {
        if (!phoneNumber.All(char.IsDigit))
            throw new BusinessException("Phone number must consist of digit only!");
    }

    private readonly Dictionary<string, CityCodes> cityAliases = new(StringComparer.OrdinalIgnoreCase)
    {
        {"afyon",CityCodes.Afyonkarahisar },
        {"antep",CityCodes.Gaziantep },
        {"maraş",CityCodes.Kahramanmaraş },
        {"urfa",CityCodes.Şanlıurfa },
    };
}