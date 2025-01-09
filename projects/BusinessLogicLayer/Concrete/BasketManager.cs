using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using Core.Shared;
using DataAccessLayer.Repositories.BasketItemRespositories;
using DataAccessLayer.Repositories.BasketPaymentRepositories;
using DataAccessLayer.Repositories.BasketRepositories;
using DataAccessLayer.Repositories.ProductRepositories;
using EntityLayer.Dtos.RequestDtos.BasketItemsRequestDtos;
using EntityLayer.Dtos.RequestDtos.BasketPaymentRequestDtos;
using EntityLayer.Dtos.RequestDtos.BasketRequestDtos;
using EntityLayer.Dtos.ResponseDtos.BasketItemResponseDtos;
using EntityLayer.Dtos.ResponseDtos.BasketPaymentResponseDtos;
using EntityLayer.Dtos.ResponseDtos.BasketResponseDtos;
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

public class BasketManager : IBasketService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IBasketItemRepository _basketItemRepository;
    private readonly IBasketPaymentRepository _basketPaymentRepository;
    private readonly IBasketRules _rules;

    public BasketManager(IBasketRepository basketRepository, IBasketItemRepository basketItemRepository, IBasketRules rules, IBasketPaymentRepository basketPaymentRepository)
    {
        _basketRepository = basketRepository;
        _basketItemRepository = basketItemRepository;
        _rules = rules;
        _basketPaymentRepository = basketPaymentRepository;
    }

    public Response<ResultBasketResponseDto> TDeleteFromDatabase(int id)
    {
        try
        {
            Basket? basket = _basketRepository.GetById(id, x => x.Include(x => x.BasketItems));
            _rules.BasketExists(basket);
            _basketRepository.DeleteFromDatabase(basket!);
            return new Response<ResultBasketResponseDto>
            {
                Message = "Basket deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultBasketResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultBasketResponseDto>> TDeleteFromDatabaseAsync(int id)
    {
        try
        {
            Basket? basket = await _basketRepository.GetByIdAsync(id);
            _rules.BasketExists(basket);
            await _basketRepository.DeleteFromDatabaseAsync(basket!);
            return new Response<ResultBasketResponseDto>
            {
                Message = "Basket deleted from database successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultBasketResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<List<ResultBasketResponseDto>> TGetAll(Expression<Func<Basket, bool>>? predicate = null, Func<IQueryable<Basket>, IIncludableQueryable<Basket, object>>? include = null)
    {
        List<Basket> baskets = _basketRepository.GetAll(predicate, x => x.Include(x => x.User).Include(x => x.BasketItems).ThenInclude(bi => bi.Product));
        List<ResultBasketResponseDto> response = baskets.Select(x => ResultBasketResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultBasketResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<List<ResultBasketResponseDto>>> TGetAllAsync(Expression<Func<Basket, bool>>? predicate = null, Func<IQueryable<Basket>, IIncludableQueryable<Basket, object>>? include = null)
    {
        List<Basket> baskets = await _basketRepository.GetAllAsync(predicate, x => x.Include(x => x.User).Include(x => x.BasketItems).ThenInclude(bi => bi.Product));
        List<ResultBasketResponseDto> response = baskets.Select(x => ResultBasketResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultBasketResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<ResultBasketResponseDto>> TSaveBasketAsync(CreateBasketRequestDto createBasketRequestDto)
    {       
        try
        {
            _rules.UserExists(createBasketRequestDto.userCode);
            List<BasketItem> basketItems = _basketItemRepository.GetAll(x => x.Deleted == null, x => x.Include(x => x.Product));
            _rules.BasketItemsExists(basketItems);
            List<BasketPayment> basketPayment = CreateBasketPaymentRequestDto.ConvertToEntity(new CreateBasketPaymentRequestDto(createBasketRequestDto.userCode, createBasketRequestDto.paymentDetails));
            Basket saveBasket = CreateBasketRequestDto.ConvertToEntity(createBasketRequestDto, basketItems,basketPayment);
            Basket savedBasket = await _basketRepository.SaveBasketAsync(saveBasket);
            ResultBasketResponseDto response = ResultBasketResponseDto.ConvertToResponse(savedBasket);
            return new Response<ResultBasketResponseDto>
            {
                Data = response,
                Message = "Basket saved successfully!",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultBasketResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
