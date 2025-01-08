using BusinessLogicLayer.Abstract;
using Core.Shared;
using EntityLayer.Dtos.RequestDtos.PointOfSaleRequestDtos;
using EntityLayer.Dtos.ResponseDtos.PointOfSaleResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class PointOfSalesController : BaseController
{
    private readonly IPointOfSaleService _pointOfSaleService;

    public PointOfSalesController(IPointOfSaleService pointOfSaleService)
    {
        _pointOfSaleService = pointOfSaleService;
    }

    [HttpPost]
    public IActionResult CashierCheckIn(CreatePointOfSaleRequestDto createPointOfSaleRequestDto)
    {
        Response<ResultPointOfSaleResponseDto> result = _pointOfSaleService.TCashierCheckIn(createPointOfSaleRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncCashierCheckIn(CreatePointOfSaleRequestDto createPointOfSaleRequestDto)
    {
        Response<ResultPointOfSaleResponseDto> result = await _pointOfSaleService.TCashierCheckInAsync(createPointOfSaleRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public IActionResult CashierCheckOut(int userCode)
    {
        Response<ResultPointOfSaleResponseDto> result = _pointOfSaleService.TCashierCheckOutByUserCode(userCode);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncCashierCheckOut(int userCode)
    {
        Response<ResultPointOfSaleResponseDto> result = await _pointOfSaleService.TCashierCheckOutByUserCodeAsync(userCode);
        return ActionResultInstance(result);
    }
}
