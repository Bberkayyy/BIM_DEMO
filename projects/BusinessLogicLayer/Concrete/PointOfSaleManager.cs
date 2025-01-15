using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using BusinessLogicLayer.Extensions.Dtos.PosDtos;
using Core.CrossCuttingConcerns;
using Core.Shared;
using DataAccessLayer.Repositories.CustomerRepositories;
using DataAccessLayer.Repositories.PointOfSaleRepositories;
using DataAccessLayer.Repositories.ProductRepositories;
using EntityLayer.Dtos.RequestDtos.CustomerRequestDtos;
using EntityLayer.Dtos.RequestDtos.GiveBackListRequestDtos;
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
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IGiveBackListService _giveBackListService;

    public PointOfSaleManager(IPointOfSaleRepository pointOfSaleRepository, IPointOfSaleRules rules, IProductRepository productRepository, ICustomerRepository customerRepository, IGiveBackListService giveBackListService)
    {
        _pointOfSaleRepository = pointOfSaleRepository;
        _rules = rules;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
        _giveBackListService = giveBackListService;
    }

    public Response<ResultPointOfSaleResponseDto> TAdvanceWithdrawal(AdvanceWithdrawalDto advanceWithdrawalDto)
    {
        try
        {
            PointOfSale? pos = _pointOfSaleRepository.GetByFilter(x => x.UserCode == advanceWithdrawalDto.userCode && x.TillId == advanceWithdrawalDto.tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos!);
            pos!.AdvanceWithdrawalTotal += advanceWithdrawalDto.advanceAmount;
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

    public async Task<Response<ResultPointOfSaleResponseDto>> TAdvanceWithdrawalAsync(AdvanceWithdrawalDto advanceWithdrawalDto)
    {
        try
        {
            PointOfSale? pos = await _pointOfSaleRepository.GetByFilterAsync(x => x.UserCode == advanceWithdrawalDto.userCode && x.TillId == advanceWithdrawalDto.tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos!);
            pos!.AdvanceWithdrawalTotal += advanceWithdrawalDto.advanceAmount;
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

    public Response<ResultPointOfSaleResponseDto> TCashWithdrawal(CashWithdrawalDto cashWithdrawalDto)
    {
        try
        {
            PointOfSale? pos = _pointOfSaleRepository.GetByFilter(x => x.UserCode == cashWithdrawalDto.userCode && x.TillId == cashWithdrawalDto.tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos!);
            pos!.CashWithdrawalTotal += cashWithdrawalDto.cashAmount;
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

    public async Task<Response<ResultPointOfSaleResponseDto>> TCashWithdrawalAsync(CashWithdrawalDto cashWithdrawalDto)
    {
        try
        {
            PointOfSale? pos = await _pointOfSaleRepository.GetByFilterAsync(x => x.UserCode == cashWithdrawalDto.userCode && x.TillId == cashWithdrawalDto.tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos!);
            pos!.CashWithdrawalTotal += cashWithdrawalDto.cashAmount;
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

    public Response<ResultPointOfSaleResponseDto> TGiveBack(GiveBackDto giveBackDto)
    {
        try
        {
            PointOfSale? pos = _pointOfSaleRepository.GetByFilter(x => x.UserCode == giveBackDto.userCode && x.TillId == giveBackDto.tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos);
            Product? product = _productRepository.GetByFilter(x => x.BarcodeNo == giveBackDto.productBarcodeNo);
            _rules.ProductExists(product);
            if (product!.Weight != 0)
                product.Weight += giveBackDto.productQuantity;
            else
                product.Stock += (int)giveBackDto.productQuantity;
            _productRepository.Update(product);
            if (giveBackDto.isReturnCash)
                pos!.GiveBackTotal += giveBackDto.giveBackAmount;
            else
                pos!.CreditCardPaymentTotal -= giveBackDto.giveBackAmount;
            _pointOfSaleRepository.Update(pos);
            Customer? customer = _customerRepository.GetByFilter(x => x.IdentityNumber == giveBackDto.createCustomerRequestDto.IdentityNumber);
            if (customer != null)
                _customerRepository.Update(customer);
            else
                _customerRepository.Create(CreateCustomerRequestDto.ConvertToEntity(giveBackDto.createCustomerRequestDto));
            CreateGiveBackListRequestDto complated = new CreateGiveBackListRequestDto(customer!.IdentityNumber, product.BarcodeNo, giveBackDto.ReasonForReturn, giveBackDto.isReturnCash, giveBackDto.productQuantity, giveBackDto.giveBackAmount);
            _giveBackListService.TCreate(complated);
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

    public async Task<Response<ResultPointOfSaleResponseDto>> TGiveBackAsync(GiveBackDto giveBackDto)
    {
        try
        {
            PointOfSale? pos = await _pointOfSaleRepository.GetByFilterAsync(x => x.UserCode == giveBackDto.userCode && x.TillId == giveBackDto.tillId && x.Deleted == null);
            _rules.PointOfSaleExists(pos);
            Product? product = await _productRepository.GetByFilterAsync(x => x.BarcodeNo == giveBackDto.productBarcodeNo);
            _rules.ProductExists(product);
            if (product!.Weight != 0)
                product.Weight += giveBackDto.productQuantity;
            else
                product.Stock += (int)giveBackDto.productQuantity;
            _productRepository.Update(product);
            if (giveBackDto.isReturnCash)
                pos!.GiveBackTotal += giveBackDto.giveBackAmount;
            else
                pos!.CreditCardPaymentTotal -= giveBackDto.giveBackAmount;
            _pointOfSaleRepository.Update(pos);
            Customer? customer = _customerRepository.GetByFilter(x => x.IdentityNumber == giveBackDto.createCustomerRequestDto.IdentityNumber);
            if (customer != null)
                _customerRepository.Update(customer);
            else
                _customerRepository.Create(CreateCustomerRequestDto.ConvertToEntity(giveBackDto.createCustomerRequestDto));
            CreateGiveBackListRequestDto complated = new CreateGiveBackListRequestDto(customer!.IdentityNumber, product.BarcodeNo, giveBackDto.ReasonForReturn, giveBackDto.isReturnCash, giveBackDto.productQuantity, giveBackDto.giveBackAmount);
            _giveBackListService.TCreate(complated);
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
