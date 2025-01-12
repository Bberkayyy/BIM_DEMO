using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using Core.CrossCuttingConcerns;
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

    public Response<ResultPointOfSaleResponseDto> TAdvanceWithdrawal(int userCode, int tillId, decimal advanceAmount)
    {
        try
        {
            PointOfSale? pos = _pointOfSaleRepository.GetByFilter(x => x.UserCode == userCode && x.TillId == tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos!);
            pos!.AdvanceWithdrawalTotal += advanceAmount;
            _pointOfSaleRepository.Update(pos);
            return new Response<ResultPointOfSaleResponseDto>
            {
                Message = "Advance withdrawal is successfully processed!",
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

    public async Task<Response<ResultPointOfSaleResponseDto>> TAdvanceWithdrawalAsync(int userCode, int tillId, decimal advanceAmount)
    {
        try
        {
            PointOfSale? pos = await _pointOfSaleRepository.GetByFilterAsync(x => x.UserCode == userCode && x.TillId == tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos!);
            pos!.AdvanceWithdrawalTotal += advanceAmount;
            _pointOfSaleRepository.Update(pos);
            return new Response<ResultPointOfSaleResponseDto>
            {
                Message = "Advance withdrawal is successfully processed!",
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

    public Response<ResultPointOfSaleResponseDto> TCashWithdrawal(int userCode, int tillId, decimal cashAmount)
    {
        try
        {
            PointOfSale? pos = _pointOfSaleRepository.GetByFilter(x => x.UserCode == userCode && x.TillId == tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos!);
            pos!.CashWithdrawalTotal += cashAmount;
            _pointOfSaleRepository.Update(pos);
            return new Response<ResultPointOfSaleResponseDto>
            {
                Message = "Cash withdrawal is successfully processed!",
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

    public async Task<Response<ResultPointOfSaleResponseDto>> TCashWithdrawalAsync(int userCode, int tillId, decimal cashAmount)
    {
        try
        {
            PointOfSale? pos = await _pointOfSaleRepository.GetByFilterAsync(x => x.UserCode == userCode && x.TillId == tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos!);
            pos!.CashWithdrawalTotal += cashAmount;
            _pointOfSaleRepository.Update(pos);
            return new Response<ResultPointOfSaleResponseDto>
            {
                Message = "Cash withdrawal is successfully processed!",
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

    public Response<ResultPointOfSaleResponseDto> TGiveBack(int userCode, int tillId, decimal giveBackAmount, bool isReturnInCash)
    {
        try
        {
            PointOfSale? pos = _pointOfSaleRepository.GetByFilter(x => x.UserCode == userCode && x.TillId == tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos!);
            if (isReturnInCash)
                pos!.GiveBackTotal += giveBackAmount;
            else
                pos!.CreditCardPaymentTotal -= giveBackAmount;
            _pointOfSaleRepository.Update(pos);
            return new Response<ResultPointOfSaleResponseDto>
            {
                Message = "Give back is successfully processed!",
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

    public async Task<Response<ResultPointOfSaleResponseDto>> TGiveBackAsync(int userCode, int tillId, decimal giveBackAmount, bool isReturnInCash)
    {
        try
        {
            PointOfSale? pos = await _pointOfSaleRepository.GetByFilterAsync(x => x.UserCode == userCode && x.TillId == tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos!);
            if (isReturnInCash)
                pos!.GiveBackTotal += giveBackAmount;
            else
                pos!.CreditCardPaymentTotal -= giveBackAmount;
            await _pointOfSaleRepository.UpdateAsync(pos);
            return new Response<ResultPointOfSaleResponseDto>
            {
                Message = "Cash withdrawal is successfully processed!",
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

    public Response<ResultPointOfSaleResponseDto> TCashierExitReport(int userCode, int tillId)
    {
        try
        {
            PointOfSale? pos = _pointOfSaleRepository.GetByFilter(x => x.UserCode == userCode && x.TillId == tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos!);
            pos!.SaleTotal = pos.CashPaymentTotal + pos.CreditCardPaymentTotal + pos.OtherPaymentTotal + pos.GiftCardPaymentTotal;
            pos.DrawerTotal = pos.CashPaymentTotal - pos.CashWithdrawalTotal + pos.OtherPaymentTotal;
            PointOfSale? exitPos = _pointOfSaleRepository.Update(pos);
            ResultPointOfSaleResponseDto response = ResultPointOfSaleResponseDto.ConvertToResponse(exitPos);
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
    public async Task<Response<ResultPointOfSaleResponseDto>> TCashierExitReportAsync(int userCode, int tillId)
    {
        try
        {
            PointOfSale? pos = _pointOfSaleRepository.GetByFilter(x => x.UserCode == userCode && x.TillId == tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos!);
            pos!.SaleTotal = pos.CashPaymentTotal + pos.CreditCardPaymentTotal + pos.OtherPaymentTotal + pos.GiftCardPaymentTotal;
            pos.DrawerTotal = pos.CashPaymentTotal - pos.CashWithdrawalTotal + pos.OtherPaymentTotal;
            PointOfSale? exitPos = await _pointOfSaleRepository.UpdateAsync(pos);
            ResultPointOfSaleResponseDto response = ResultPointOfSaleResponseDto.ConvertToResponse(exitPos);
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
