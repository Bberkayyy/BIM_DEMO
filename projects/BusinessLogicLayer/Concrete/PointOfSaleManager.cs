using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using Core.Shared;
using DataAccessLayer.Repositories.PointOfSaleRepositories;
using EntityLayer.Dtos.RequestDtos.PointOfSaleRequestDtos;
using EntityLayer.Dtos.ResponseDtos.PointOfSaleResponseDtos;
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

public class PointOfSaleManager : IPointOfSaleService
{
    private readonly IPointOfSaleRepository _pointOfSaleRepository;
    private readonly IPointOfSaleRules _rules;

    public PointOfSaleManager(IPointOfSaleRepository pointOfSaleRepository, IPointOfSaleRules rules)
    {
        _pointOfSaleRepository = pointOfSaleRepository;
        _rules = rules;
    }

    public Response<ResultPointOfSaleResponseDto> TCashierCheckIn(CreatePointOfSaleRequestDto createPointOfSaleRequestDto)
    {
        try
        {
            _rules.UserExists(createPointOfSaleRequestDto.UserCode);
            _rules.TillExists(createPointOfSaleRequestDto.TillId);
            PointOfSale createPos = CreatePointOfSaleRequestDto.ConvertToEntity(createPointOfSaleRequestDto);
            PointOfSale createdPod = _pointOfSaleRepository.Create(createPos);
            ResultPointOfSaleResponseDto response = ResultPointOfSaleResponseDto.ConvertToResponse(createdPod);
            return new Response<ResultPointOfSaleResponseDto>
            {
                Data = response,
                Message = "Cashier check-in is successfull!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultPointOfSaleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public async Task<Response<ResultPointOfSaleResponseDto>> TCashierCheckInAsync(CreatePointOfSaleRequestDto createPointOfSaleRequestDto)
    {
        try
        {
            _rules.UserExists(createPointOfSaleRequestDto.UserCode);
            _rules.TillExists(createPointOfSaleRequestDto.TillId);
            PointOfSale createPos = CreatePointOfSaleRequestDto.ConvertToEntity(createPointOfSaleRequestDto);
            PointOfSale createdPod = await _pointOfSaleRepository.CreateAsync(createPos);
            ResultPointOfSaleResponseDto response = ResultPointOfSaleResponseDto.ConvertToResponse(createdPod);
            return new Response<ResultPointOfSaleResponseDto>
            {
                Data = response,
                Message = "Cashier check-in is successfull!",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultPointOfSaleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }

    }

    public Response<ResultPointOfSaleResponseDto> TCashierCheckOutByUserCode(int userCode)
    {
        try
        {
            PointOfSale? pos = _pointOfSaleRepository.GetByFilter(x => x.UserCode == userCode && x.Deleted == null, x => x.Include(x => x.User).Include(x => x.Till));
            _rules.PointOfSaleExists(pos!);
            _pointOfSaleRepository.Delete(pos!);
            ResultPointOfSaleResponseDto response = ResultPointOfSaleResponseDto.ConvertToResponse(pos!);
            return new Response<ResultPointOfSaleResponseDto>
            {
                Data = response,
                Message = "Cashier check-out completed successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultPointOfSaleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }

    }

    public async Task<Response<ResultPointOfSaleResponseDto>> TCashierCheckOutByUserCodeAsync(int userCode)
    {
        try
        {
            PointOfSale? pos = await _pointOfSaleRepository.GetByFilterAsync(x => x.UserCode == userCode && x.Deleted == null, x => x.Include(x => x.User).Include(x => x.Till));
            _rules.PointOfSaleExists(pos!);
            await _pointOfSaleRepository.DeleteAsync(pos!);
            ResultPointOfSaleResponseDto response = ResultPointOfSaleResponseDto.ConvertToResponse(pos!);
            return new Response<ResultPointOfSaleResponseDto>
            {
                Data = response,
                Message = "Cashier check-out completed successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultPointOfSaleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }

    }

    public Response<List<ResultPointOfSaleResponseDto>> TGetAll(Expression<Func<PointOfSale, bool>>? predicate = null, Func<IQueryable<PointOfSale>, IIncludableQueryable<PointOfSale, object>>? include = null)
    {
        List<PointOfSale> pointOfSales = _pointOfSaleRepository.GetAll(predicate, x => x.Include(x => x.User).Include(x => x.Till));
        List<ResultPointOfSaleResponseDto> response = pointOfSales.Select(x => ResultPointOfSaleResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultPointOfSaleResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<List<ResultPointOfSaleResponseDto>>> TGetAllAsync(Expression<Func<PointOfSale, bool>>? predicate = null, Func<IQueryable<PointOfSale>, IIncludableQueryable<PointOfSale, object>>? include = null)
    {
        List<PointOfSale> pointOfSales = await _pointOfSaleRepository.GetAllAsync(predicate, x => x.Include(x => x.User).Include(x => x.Till));
        List<ResultPointOfSaleResponseDto> response = pointOfSales.Select(x => ResultPointOfSaleResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultPointOfSaleResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ResultPointOfSaleResponseDto> TGetByFilter(Expression<Func<PointOfSale, bool>> predicate, Func<IQueryable<PointOfSale>, IIncludableQueryable<PointOfSale, object>>? include = null)
    {
        try
        {
            PointOfSale? pos = _pointOfSaleRepository.GetByFilter(predicate, include);
            _rules.PointOfSaleExists(pos!);
            ResultPointOfSaleResponseDto response = ResultPointOfSaleResponseDto.ConvertToResponse(pos!);
            return new Response<ResultPointOfSaleResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultPointOfSaleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }

    }

    public async Task<Response<ResultPointOfSaleResponseDto>> TGetByFilterAsync(Expression<Func<PointOfSale, bool>> predicate, Func<IQueryable<PointOfSale>, IIncludableQueryable<PointOfSale, object>>? include = null)
    {
        try
        {
            PointOfSale? pos = await _pointOfSaleRepository.GetByFilterAsync(predicate, include);
            _rules.PointOfSaleExists(pos!);
            ResultPointOfSaleResponseDto response = ResultPointOfSaleResponseDto.ConvertToResponse(pos!);
            return new Response<ResultPointOfSaleResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultPointOfSaleResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }

    }
}
