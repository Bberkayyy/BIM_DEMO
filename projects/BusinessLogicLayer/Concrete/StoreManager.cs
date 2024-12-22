using BusinessLogicLayer.Abstract;
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

    public StoreManager(IStoreRepository storeRepository, IUserRepository userRepository)
    {
        _storeRepository = storeRepository;
        _userRepository = userRepository;
    }

    public Response<ResultStoreResponseDto> TCreate(CreateStoreRequestDto createStoreRequestDto)
    {
        Store createStore = CreateStoreRequestDto.ConvertToEntity(createStoreRequestDto);
        Store createdStore = _storeRepository.Create(createStore);
        ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(createdStore);
        return new Response<ResultStoreResponseDto>()
        {
            Data = response,
            Message = "Store created successfully!",
            StatusCode = System.Net.HttpStatusCode.Created
        };
    }

    public async Task<Response<ResultStoreResponseDto>> TCreateAsync(CreateStoreRequestDto createStoreRequestDto)
    {
        Store createStore = CreateStoreRequestDto.ConvertToEntity(createStoreRequestDto);
        Store createdStore = await _storeRepository.CreateAsync(createStore);
        ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(createdStore);
        return new Response<ResultStoreResponseDto>()
        {
            Data = response,
            Message = "Store created successfully!",
            StatusCode = System.Net.HttpStatusCode.Created
        };
    }

    public Response<ResultStoreResponseDto> TDeleteById(int id)
    {
        Store? store = _storeRepository.GetByFilter(x => x.Id == id, include: x => x.Include(x => x.Users));
        if (store != null)
        {
            foreach (var user in store.Users)
            {
                user.Deleted = DateTime.Now;
                _userRepository.Delete(user);
            }
            _storeRepository.Delete(store);
        }
        return new Response<ResultStoreResponseDto>()
        {
            Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
            Message = store != null ? $"Store deleted successfully! ({store.Users.Count} product is affected)" : "Store not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultStoreResponseDto>> TDeleteByIdAsync(int id)
    {
        Store? store = await _storeRepository.GetByFilterAsync(x => x.Id == id, include: x => x.Include(x => x.Users));
        if (store != null)
        {
            foreach (var user in store.Users)
            {
                user.Deleted = DateTime.Now;
                await _userRepository.DeleteAsync(user);
            }
            await _storeRepository.DeleteAsync(store);
        }
        return new Response<ResultStoreResponseDto>()
        {
            Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
            Message = store != null ? $"Store deleted successfully! ({store.Users.Count} product is affected)" : "Store not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultStoreResponseDto> TDeleteByStoreNo(int storeNo)
    {
        Store? store = _storeRepository.GetByFilter(x => x.StoreNo == storeNo, include: x => x.Include(x => x.Users));
        if (store != null)
        {
            foreach (var user in store.Users)
            {
                user.Deleted = DateTime.Now;
                _userRepository.Delete(user);
            }
            _storeRepository.Delete(store);
        }
        return new Response<ResultStoreResponseDto>()
        {
            Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
            Message = store != null ? $"Store deleted successfully! ({store.Users.Count} product is affected)" : "Store not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultStoreResponseDto>> TDeleteByStoreNoAsync(int storeNo)
    {
        Store? store = await _storeRepository.GetByFilterAsync(x => x.StoreNo == storeNo, include: x => x.Include(x => x.Users));
        if (store != null)
        {
            foreach (var user in store.Users)
            {
                user.Deleted = DateTime.Now;
                await _userRepository.DeleteAsync(user);
            }
            await _storeRepository.DeleteAsync(store);
        }
        return new Response<ResultStoreResponseDto>()
        {
            Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
            Message = store != null ? $"Store deleted successfully! ({store.Users.Count} product is affected)" : "Store not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultStoreResponseDto> TDeleteFromDatabaseById(int id)
    {
        Store? store = _storeRepository.GetByFilter(x => x.Id == id);
        if (store != null)
            _storeRepository.DeleteFromDatabase(store);
        return new Response<ResultStoreResponseDto>()
        {
            Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
            Message = store != null ? "Store deleted successfully!" : "Store not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultStoreResponseDto>> TDeleteFromDatabaseByIdAsync(int id)
    {
        Store? store = await _storeRepository.GetByFilterAsync(x => x.Id == id);
        if (store != null)
            await _storeRepository.DeleteFromDatabaseAsync(store);
        return new Response<ResultStoreResponseDto>()
        {
            Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
            Message = store != null ? "Store deleted successfully!" : "Store not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<ResultStoreResponseDto> TDeleteFromDatabaseByStoreNo(int storeNo)
    {
        Store? store = _storeRepository.GetByFilter(x => x.StoreNo == storeNo);
        if (store != null)
            _storeRepository.DeleteFromDatabase(store);
        return new Response<ResultStoreResponseDto>()
        {
            Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
            Message = store != null ? "Store deleted successfully!" : "Store not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public async Task<Response<ResultStoreResponseDto>> TDeleteFromDatabaseByStoreNoAsync(int storeNo)
    {
        Store? store = await _storeRepository.GetByFilterAsync(x => x.StoreNo == storeNo);
        if (store != null)
            await _storeRepository.DeleteFromDatabaseAsync(store);
        return new Response<ResultStoreResponseDto>()
        {
            Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
            Message = store != null ? "Store deleted successfully!" : "Store not found!",
            StatusCode = System.Net.HttpStatusCode.NoContent
        };
    }

    public Response<List<ResultStoreResponseDto>> TGetAll(Expression<Func<Store, bool>>? predicate = null, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null)
    {
        List<Store> stores = _storeRepository.GetAll(x => x.Deleted == null, x => x.Include(x => x.Users));
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
        Store? store = _storeRepository.GetByFilter(predicate, include);
        return new Response<ResultStoreResponseDto>()
        {
            Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<Response<ResultStoreResponseDto>> TGetByFilterAsync(Expression<Func<Store, bool>> predicate, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null)
    {
        Store? store = await _storeRepository.GetByFilterAsync(predicate, include);
        return new Response<ResultStoreResponseDto>()
        {
            Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public Response<ResultStoreResponseDto> TGetById(int id, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null)
    {
        Store? store = _storeRepository.GetById(id, include);
        return new Response<ResultStoreResponseDto>()
        {
            Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<Response<ResultStoreResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Store>, IIncludableQueryable<Store, object>>? include = null)
    {
        Store? store = await _storeRepository.GetByIdAsync(id, include);
        return new Response<ResultStoreResponseDto>()
        {
            Data = store != null ? ResultStoreResponseDto.ConvertToResponse(store) : null,
            StatusCode = System.Net.HttpStatusCode.OK,
        };
    }

    public Response<ResultStoreResponseDto> TUpdate(UpdateStoreRequestDto updateStoreRequestDto)
    {
        Store updateStore = UpdateStoreRequestDto.ConvertToEntity(updateStoreRequestDto);
        Store updatedStore = _storeRepository.Update(updateStore);
        ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(updatedStore);
        return new Response<ResultStoreResponseDto>()
        {
            Data = response,
            Message = "Store updated successfully!",
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<ResultStoreResponseDto>> TUpdateAsync(UpdateStoreRequestDto updateStoreRequestDto)
    {
        Store updateStore = UpdateStoreRequestDto.ConvertToEntity(updateStoreRequestDto);
        Store updatedStore = await _storeRepository.UpdateAsync(updateStore);
        ResultStoreResponseDto response = ResultStoreResponseDto.ConvertToResponse(updatedStore);
        return new Response<ResultStoreResponseDto>()
        {
            Data = response,
            Message = "Store updated successfully!",
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }
}
