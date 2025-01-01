using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using BusinessLogicLayer.Extensions;
using Core.Shared;
using DataAccessLayer.Repositories.StoreRepositories;
using DataAccessLayer.Repositories.UserRepositories;
using EntityLayer.Dtos.RequestDtos.StoreRequestDtos;
using EntityLayer.Dtos.ResponseDtos.StoreResponseDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Concrete;

public class StoreManager : IStoreService
{
    private readonly IStoreRepository _storeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IStoreRules _rules;

    public StoreManager(IStoreRepository storeRepository, IUserRepository userRepository, IStoreRules rules)
    {
        _storeRepository = storeRepository;
        _userRepository = userRepository;
        _rules = rules;
    }

    public Response<ResultStoreResponseDto> TCreate(CreateStoreRequestDto createStoreRequestDto)
    {
        try
        {
            int cityCode = _rules.GetCityCode(createStoreRequestDto.City);
            string storeNo = _rules.GenerateStoreNo(cityCode);
            _rules.NameCannotBeNullOrWhiteSpace(createStoreRequestDto.Name);
            _rules.NameMustBeUnique(createStoreRequestDto.Name);
            _rules.PhoneNumberCannotBeNullOrWhiteSpace(createStoreRequestDto.PhoneNumber);
            _rules.PhoneNumberMustBeElevenCharacter(createStoreRequestDto.PhoneNumber);
            _rules.AddressCannotBeNullOrWhiteSpace(createStoreRequestDto.Address);
            _rules.AddressMustBeMinTwentyCharacter(createStoreRequestDto.Address);
            Store createStore = CreateStoreRequestDto.ConvertToEntity(createStoreRequestDto, storeNo!);
            Store createdStore = _storeRepository.Create(createStore);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(createdStore);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                Message = "Store created successfully!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }

    }

    public async Task<Response<ResultStoreResponseDto>> TCreateAsync(CreateStoreRequestDto createStoreRequestDto)
    {
        try
        {
            int cityCode = _rules.GetCityCode(createStoreRequestDto.City);
            string storeNo = _rules.GenerateStoreNo(cityCode);
            _rules.NameMustBeUnique(createStoreRequestDto.Name);
            _rules.PhoneNumberMustBeElevenCharacter(createStoreRequestDto.PhoneNumber);
            _rules.AddressMustBeMinTwentyCharacter(createStoreRequestDto.Address);
            Store createStore = CreateStoreRequestDto.ConvertToEntity(createStoreRequestDto, storeNo!);
            Store createdStore = await _storeRepository.CreateAsync(createStore);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(createdStore);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                Message = "Store created successfully!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }

    }

    public Response<ResultStoreResponseDto> TDeleteById(int id)
    {
        try
        {
            Store? store = _storeRepository.GetByFilter(x => x.Id == id, include: x => x.Include(x => x.Users));
            _rules.StoreExists(store);
            _rules.StoreUserMustBeZeroWhenStoreIsDeleted(store!.Users.Count, store.Users);
            store.Deleted = DateTime.Now;
            _storeRepository.Delete(store);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(store);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                Message = "Store deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultStoreResponseDto>> TDeleteByIdAsync(int id)
    {
        try
        {
            Store? store = await _storeRepository.GetByFilterAsync(x => x.Id == id, include: x => x.Include(x => x.Users));
            _rules.StoreExists(store);
            _rules.StoreUserMustBeZeroWhenStoreIsDeleted(store!.Users.Count, store.Users);
            store.Deleted = DateTime.Now;
            await _storeRepository.DeleteAsync(store);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(store);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                Message = "Store deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultStoreResponseDto> TDeleteByStoreNo(string storeNo)
    {
        try
        {
            Store? store = _storeRepository.GetByFilter(x => x.StoreNo == storeNo, include: x => x.Include(x => x.Users));
            _rules.StoreExists(store);
            _rules.StoreUserMustBeZeroWhenStoreIsDeleted(store!.Users.Count, store.Users);
            store.Deleted = DateTime.Now;
            _storeRepository.Delete(store);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(store);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                Message = "Store deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultStoreResponseDto>> TDeleteByStoreNoAsync(string storeNo)
    {
        try
        {
            Store? store = await _storeRepository.GetByFilterAsync(x => x.StoreNo == storeNo, include: x => x.Include(x => x.Users));
            _rules.StoreExists(store);
            _rules.StoreUserMustBeZeroWhenStoreIsDeleted(store!.Users.Count, store.Users);
            store.Deleted = DateTime.Now;
            await _storeRepository.DeleteAsync(store);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(store);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                Message = "Store deleted successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultStoreResponseDto> TDeleteFromDatabaseById(int id)
    {
        try
        {
            Store? store = _storeRepository.GetByFilter(x => x.Id == id);
            _rules.StoreExists(store, true);
            _storeRepository.DeleteFromDatabase(store!);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(store!);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                Message = "Store deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultStoreResponseDto>> TDeleteFromDatabaseByIdAsync(int id)
    {
        try
        {
            Store? store = await _storeRepository.GetByFilterAsync(x => x.Id == id);
            _rules.StoreExists(store, true);
            await _storeRepository.DeleteFromDatabaseAsync(store!);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(store!);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                Message = "Store deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultStoreResponseDto> TDeleteFromDatabaseByStoreNo(string storeNo)
    {
        try
        {
            Store? store = _storeRepository.GetByFilter(x => x.StoreNo == storeNo);
            _rules.StoreExists(store, true);
            _storeRepository.DeleteFromDatabase(store!);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(store!);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                Message = "Store deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultStoreResponseDto>> TDeleteFromDatabaseByStoreNoAsync(string storeNo)
    {
        try
        {
            Store? store = await _storeRepository.GetByFilterAsync(x => x.StoreNo == storeNo);
            _rules.StoreExists(store, true);
            await _storeRepository.DeleteFromDatabaseAsync(store!);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(store!);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                Message = "Store deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<List<ResultStoreResponseDto>> TGetAll(Expression<Func<Store, bool>>? predicate = null, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null)
    {
        List<Store> stores = _storeRepository.GetAll(predicate ?? (x => x.Deleted == null), include ?? (x => x.Include(x => x.Users)));
        List<ResultStoreResponseDto> response = stores.Select(x => ResultStoreResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultStoreResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<Response<List<ResultStoreResponseDto>>> TGetAllAsync(Expression<Func<Store, bool>>? predicate = null, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null)
    {
        List<Store> stores = await _storeRepository.GetAllAsync(x => x.Deleted == null, x => x.Include(x => x.Users));
        List<ResultStoreResponseDto> response = stores.Select(x => ResultStoreResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultStoreResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public Response<ResultStoreResponseDto> TGetByFilter(Expression<Func<Store, bool>> predicate, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null)
    {
        try
        {
            Store? store = _storeRepository.GetByFilter(predicate, include);
            _rules.StoreExists(store);
            return new Response<ResultStoreResponseDto>()
            {
                Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultStoreResponseDto>> TGetByFilterAsync(Expression<Func<Store, bool>> predicate, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null)
    {
        try
        {
            Store? store = await _storeRepository.GetByFilterAsync(predicate, include);
            _rules.StoreExists(store);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(store!);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultStoreResponseDto> TGetById(int id, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null)
    {
        try
        {
            Store? store = _storeRepository.GetById(id, include);
            _rules.StoreExists(store);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(store!);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultStoreResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null)
    {
        try
        {
            Store? store = await _storeRepository.GetByIdAsync(id, include);
            _rules.StoreExists(store);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(store!);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultStoreResponseDto> TUpdate(UpdateStoreRequestDto updateStoreRequestDto)
    {
        try
        {
            _rules.StoreNoMustBeSixCharacter(updateStoreRequestDto.StoreNo);
            _rules.IsStoreNoValid(updateStoreRequestDto.StoreNo, updateStoreRequestDto.City);
            _rules.StoreNoMustBeUnique(updateStoreRequestDto.StoreNo, updateStoreRequestDto.Id);
            _rules.NameMustBeUnique(updateStoreRequestDto.Name, updateStoreRequestDto.Id);
            _rules.AddressMustBeMinTwentyCharacter(updateStoreRequestDto.Address);
            _rules.PhoneNumberMustBeElevenCharacter(updateStoreRequestDto.PhoneNumber);
            Store updateStore = UpdateStoreRequestDto.ConvertToEntity(updateStoreRequestDto);
            Store updatedStore = _storeRepository.Update(updateStore);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(updatedStore);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                Message = "Store updated successfully!",
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public async Task<Response<ResultStoreResponseDto>> TUpdateAsync(UpdateStoreRequestDto updateStoreRequestDto)
    {
        try
        {
            _rules.StoreNoMustBeSixCharacter(updateStoreRequestDto.StoreNo);
            _rules.StoreNoMustBeDigit(updateStoreRequestDto.StoreNo);
            _rules.IsStoreNoValid(updateStoreRequestDto.StoreNo, updateStoreRequestDto.City);
            _rules.StoreNoMustBeUnique(updateStoreRequestDto.StoreNo, updateStoreRequestDto.Id);
            _rules.NameCannotBeNullOrWhiteSpace(updateStoreRequestDto.Name);
            _rules.NameMustBeUnique(updateStoreRequestDto.Name, updateStoreRequestDto.Id);
            _rules.PhoneNumberCannotBeNullOrWhiteSpace(updateStoreRequestDto.PhoneNumber);
            _rules.PhoneNumberMustBeElevenCharacter(updateStoreRequestDto.PhoneNumber);
            _rules.PhoneNumberMustBeDigit(updateStoreRequestDto.PhoneNumber);
            _rules.AddressCannotBeNullOrWhiteSpace(updateStoreRequestDto.Address);
            _rules.AddressMustBeMinTwentyCharacter(updateStoreRequestDto.Address);
            Store updateStore = UpdateStoreRequestDto.ConvertToEntity(updateStoreRequestDto);
            Store updatedStore = await _storeRepository.UpdateAsync(updateStore);
            ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(updatedStore);
            return new Response<ResultStoreResponseDto>()
            {
                Data = response,
                Message = "Store updated successfully!",
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
        }
        catch (Exception e)
        {
            return new Response<ResultStoreResponseDto>()
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
