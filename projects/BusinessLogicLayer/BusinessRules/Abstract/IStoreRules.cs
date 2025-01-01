using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Abstract;

public interface IStoreRules
{
    bool StoreNoMustBeUnique(string storeNo);
    void StoreNoMustBeDigit(string storeNo);
    void StoreNoMustBeUnique(string storeNo, int id = -1);
    void StoreNoMustBeSixCharacter(string storeNo);
    void NameMustBeUnique(string name, int id = -1);
    void AddressMustBeMinTwentyCharacter(string address);
    void PhoneNumberMustBeElevenCharacter(string phoneNumber);
    void StoreExists(Store? store, bool isDeleteFromDatabase = false);
    void IsStoreNoValid(string storeNo, string city);
    void StoreUserMustBeZeroWhenStoreIsDeleted(int storeUser, ICollection<User> users);
    void NameCannotBeNullOrWhiteSpace(string name);
    void AddressCannotBeNullOrWhiteSpace(string address);
    void PhoneNumberCannotBeNullOrWhiteSpace(string phoneNumber);
    void PhoneNumberMustBeDigit(string phoneNumber);
    void CityCannotBeNullOrWhiteSpace(string city);
    int GetCityCode(string city);
}
