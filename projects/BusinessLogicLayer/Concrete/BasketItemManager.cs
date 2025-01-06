using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using Core.Shared;
using DataAccessLayer.Repositories.BasketItemRespositories;
using DataAccessLayer.Repositories.ProductRepositories;
using EntityLayer.Dtos.RequestDtos.BasketItemsRequestDtos;
using EntityLayer.Dtos.ResponseDtos.BasketItemResponseDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Concrete;

public class BasketItemManager : IBasketItemService
{
    private readonly IBasketItemRepository _basketItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IBasketItemRules _rules;

    public BasketItemManager(IBasketItemRepository basketItemRepository, IProductRepository productRepository, IBasketItemRules rules)
    {
        _basketItemRepository = basketItemRepository;
        _productRepository = productRepository;
        _rules = rules;
    }

    public Response<ResultBasketItemResponseDto> TAddToBasket(CreateBasketItemRequestDto createBasketItemRequestDto)
    {
        try
        {
            _rules.BarcodeNoMustBeDigitAndFourteenCharacter(createBasketItemRequestDto.BarcodeNo);
            _rules.ShortCodeMustBeDigitAndSevenCharacter(createBasketItemRequestDto.ShortCode);
            Product? product = _productRepository.GetByFilter(x => x.BarcodeNo == createBasketItemRequestDto.BarcodeNo || x.ShortCode == createBasketItemRequestDto.ShortCode);
            _rules.ProductExists(product);
            if (product!.Weight != 0)
            {
                product!.Weight -= createBasketItemRequestDto.Quantity;
                _productRepository.Update(product);
            }
            else
            {
                product!.Stock -= (int)createBasketItemRequestDto.Quantity;
                _productRepository.Update(product);
            }
            decimal totalPrice = product!.Price * createBasketItemRequestDto.Quantity;
            BasketItem addBasketItem = CreateBasketItemRequestDto.ConvertToEntity(createBasketItemRequestDto, totalPrice, product.Id);
            BasketItem addedBasketItem = _basketItemRepository.Create(addBasketItem);
            ResultBasketItemResponseDto response = ResultBasketItemResponseDto.ConvertToResponse(addedBasketItem);
            return new Response<ResultBasketItemResponseDto>
            {
                Data = response,
                Message = "Item added to basket.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultBasketItemResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }

    }

    public async Task<Response<ResultBasketItemResponseDto>> TAddToBasketAsync(CreateBasketItemRequestDto createBasketItemRequestDto)
    {
        try
        {
            _rules.BarcodeNoMustBeDigitAndFourteenCharacter(createBasketItemRequestDto.BarcodeNo);
            _rules.ShortCodeMustBeDigitAndSevenCharacter(createBasketItemRequestDto.ShortCode);
            Product? product = _productRepository.GetByFilter(x => x.BarcodeNo == createBasketItemRequestDto.BarcodeNo || x.ShortCode == createBasketItemRequestDto.ShortCode);
            _rules.ProductExists(product);
            if (product!.Weight != 0)
            {
                product!.Weight -= createBasketItemRequestDto.Quantity;
                await _productRepository.UpdateAsync(product);
            }
            else
            {
                product!.Stock -= (int)createBasketItemRequestDto.Quantity;
                await _productRepository.UpdateAsync(product);
            }
            decimal totalPrice = product!.Price * createBasketItemRequestDto.Quantity;
            BasketItem addBasketItem = CreateBasketItemRequestDto.ConvertToEntity(createBasketItemRequestDto, totalPrice, product.Id);
            BasketItem addedBasketItem = await _basketItemRepository.CreateAsync(addBasketItem);
            ResultBasketItemResponseDto response = ResultBasketItemResponseDto.ConvertToResponse(addedBasketItem);
            return new Response<ResultBasketItemResponseDto>
            {
                Data = response,
                Message = "Item added to basket.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultBasketItemResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<List<ResultBasketItemResponseDto>> TGetBasketItems()
    {
        List<BasketItem> basketItems = _basketItemRepository.GetAll(x => x.Deleted == null, x => x.Include(x => x.Product));
        List<ResultBasketItemResponseDto> response = basketItems.Select(x => ResultBasketItemResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultBasketItemResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<List<ResultBasketItemResponseDto>>> TGetBasketItemsAsync()
    {
        List<BasketItem> basketItems = await _basketItemRepository.GetAllAsync(x => x.Deleted == null, x => x.Include(x => x.Product));
        List<ResultBasketItemResponseDto> response = basketItems.Select(x => ResultBasketItemResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultBasketItemResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ResultBasketItemResponseDto> TRemoveFromBasket(long? barcodeNo, int? shortCode, decimal quantity = 1)
    {
        try
        {
            BasketItem? basketItem = _basketItemRepository.GetByFilter(x => x.Product.BarcodeNo == barcodeNo || x.Product.ShortCode == shortCode, x => x.Include(x => x.Product));
            _rules.BasketItemExists(basketItem);
            _rules.QuantityMustBeExists(basketItem!, quantity);
            Product product = basketItem!.Product;
            if (product!.Weight != 0)
            {
                product!.Weight += quantity;
                _productRepository.Update(product);
            }
            else
            {
                product!.Stock += (int)quantity;
                _productRepository.Update(product);
            }
            _basketItemRepository.DeleteFromDatabase(basketItem!);
            return new Response<ResultBasketItemResponseDto>
            {
                Message = "Items deleted from basket.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultBasketItemResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultBasketItemResponseDto>> TRemoveFromBasketAsync(long? barcodeNo, int? shortCode, decimal quantity = 1)
    {
        try
        {
            BasketItem? basketItem = await _basketItemRepository.GetByFilterAsync(x => x.Product.BarcodeNo == barcodeNo || x.Product.ShortCode == shortCode, x => x.Include(x => x.Product));
            _rules.BasketItemExists(basketItem);
            _rules.QuantityMustBeExists(basketItem!, quantity);
            Product product = basketItem!.Product;
            if (product!.Weight != 0)
            {
                product!.Weight += quantity;
                await _productRepository.UpdateAsync(product);
            }
            else
            {
                product!.Stock += (int)quantity;
                await _productRepository.UpdateAsync(product);
            }
            await _basketItemRepository.DeleteFromDatabaseAsync(basketItem!);
            return new Response<ResultBasketItemResponseDto>
            {
                Message = "Items deleted from basket.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultBasketItemResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }
}
