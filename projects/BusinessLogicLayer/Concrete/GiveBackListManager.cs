using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using Core.Shared;
using DataAccessLayer.Repositories.GiveBackListRepositories;
using EntityLayer.Dtos.RequestDtos.GiveBackListRequestDtos;
using EntityLayer.Dtos.ResponseDtos.GiveBackListResponseDtos;
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

public class GiveBackListManager : IGiveBackListService
{
    private readonly IGiveBackListRepository _giveBackListRepository;
    private readonly IGiveBackListRules _rules;

    public GiveBackListManager(IGiveBackListRepository giveBackListRepository, IGiveBackListRules rules)
    {
        _giveBackListRepository = giveBackListRepository;
        _rules = rules;
    }

    public Response<ResultGiveBackListResponseDto> TCreate(CreateGiveBackListRequestDto createGiveBackListRequestDto)
    {
        try
        {
            _rules.ProductExists(createGiveBackListRequestDto.ProductBarcodeNo);
            _rules.CustomerExists(createGiveBackListRequestDto.CustomerIdentityNumber);
            GiveBackList createGiveBack = CreateGiveBackListRequestDto.ConvertToEntity(createGiveBackListRequestDto);
            GiveBackList createdGiveBack = _giveBackListRepository.Create(createGiveBack);
            ResultGiveBackListResponseDto response = ResultGiveBackListResponseDto.ConvertToResponse(createdGiveBack);
            return new Response<ResultGiveBackListResponseDto>
            {
                Data = response,
                Message = "Return transaction was recorded successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultGiveBackListResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public async Task<Response<ResultGiveBackListResponseDto>> TCreateAsync(CreateGiveBackListRequestDto createGiveBackListRequestDto)
    {
        try
        {
            _rules.ProductExists(createGiveBackListRequestDto.ProductBarcodeNo);
            _rules.CustomerExists(createGiveBackListRequestDto.CustomerIdentityNumber);
            GiveBackList createGiveBack = CreateGiveBackListRequestDto.ConvertToEntity(createGiveBackListRequestDto);
            GiveBackList createdGiveBack = await _giveBackListRepository.CreateAsync(createGiveBack);
            ResultGiveBackListResponseDto response = ResultGiveBackListResponseDto.ConvertToResponse(createdGiveBack);
            return new Response<ResultGiveBackListResponseDto>
            {
                Data = response,
                Message = "Return transaction was recorded successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultGiveBackListResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<List<ResultGiveBackListResponseDto>> TGetAll(Expression<Func<GiveBackList, bool>>? predicate = null, Func<IQueryable<GiveBackList>, IIncludableQueryable<GiveBackList, object>>? include = null)
    {
        List<GiveBackList> giveBackLists = _giveBackListRepository.GetAll(predicate, x => x.Include(x => x.Customer).Include(x => x.Product));
        List<ResultGiveBackListResponseDto> response = giveBackLists.Select(x => ResultGiveBackListResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultGiveBackListResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<List<ResultGiveBackListResponseDto>>> TGetAllAsync(Expression<Func<GiveBackList, bool>>? predicate = null, Func<IQueryable<GiveBackList>, IIncludableQueryable<GiveBackList, object>>? include = null)
    {
        List<GiveBackList> giveBackLists = await _giveBackListRepository.GetAllAsync(predicate, x => x.Include(x => x.Customer).Include(x => x.Product));
        List<ResultGiveBackListResponseDto> response = giveBackLists.Select(x => ResultGiveBackListResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultGiveBackListResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ResultGiveBackListResponseDto> TGetByFilter(Expression<Func<GiveBackList, bool>> predicate, Func<IQueryable<GiveBackList>, IIncludableQueryable<GiveBackList, object>>? include = null)
    {
        try
        {
            GiveBackList? giveback = _giveBackListRepository.GetByFilter(predicate, x => x.Include(x => x.Customer).Include(x => x.Product));
            _rules.GiveBackExists(giveback);
            ResultGiveBackListResponseDto response = ResultGiveBackListResponseDto.ConvertToResponse(giveback!);
            return new Response<ResultGiveBackListResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultGiveBackListResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }
    }

    public async Task<Response<ResultGiveBackListResponseDto>> TGetByFilterAsync(Expression<Func<GiveBackList, bool>> predicate, Func<IQueryable<GiveBackList>, IIncludableQueryable<GiveBackList, object>>? include = null)
    {
        try
        {
            GiveBackList? giveback = await _giveBackListRepository.GetByFilterAsync(predicate, x => x.Include(x => x.Customer).Include(x => x.Product));
            _rules.GiveBackExists(giveback);
            ResultGiveBackListResponseDto response = ResultGiveBackListResponseDto.ConvertToResponse(giveback!);
            return new Response<ResultGiveBackListResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultGiveBackListResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }
    }

    public Response<ResultGiveBackListResponseDto> TGetById(int id, Func<IQueryable<GiveBackList>, IIncludableQueryable<GiveBackList, object>>? include = null)
    {
        try
        {
            GiveBackList? giveback = _giveBackListRepository.GetById(id, x => x.Include(x => x.Customer).Include(x => x.Product));
            _rules.GiveBackExists(giveback);
            ResultGiveBackListResponseDto response = ResultGiveBackListResponseDto.ConvertToResponse(giveback!);
            return new Response<ResultGiveBackListResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultGiveBackListResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }
    }

    public async Task<Response<ResultGiveBackListResponseDto>> TGetByIdAsync(int id, Func<IQueryable<GiveBackList>, IIncludableQueryable<GiveBackList, object>>? include = null)
    {
        try
        {
            GiveBackList? giveback = await _giveBackListRepository.GetByIdAsync(id, x => x.Include(x => x.Customer).Include(x => x.Product));
            _rules.GiveBackExists(giveback);
            ResultGiveBackListResponseDto response = ResultGiveBackListResponseDto.ConvertToResponse(giveback!);
            return new Response<ResultGiveBackListResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultGiveBackListResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }
    }
}
