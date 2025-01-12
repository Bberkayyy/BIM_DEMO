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
    [HttpPost]
    public IActionResult AdvanceWithdrawal(int userCode, int tillId, decimal advanceAmount)
    {
        Response<ResultPointOfSaleResponseDto> result = _pointOfSaleService.TAdvanceWithdrawal(userCode, tillId, advanceAmount);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncAdvanceWithdrawal(int userCode, int tillId, decimal advanceAmount)
    {
        Response<ResultPointOfSaleResponseDto> result = await _pointOfSaleService.TAdvanceWithdrawalAsync(userCode, tillId, advanceAmount);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public IActionResult CashWithdrawal(int userCode, int tillId, decimal cashAmount)
    {
        Response<ResultPointOfSaleResponseDto> result = _pointOfSaleService.TCashWithdrawal(userCode, tillId, cashAmount);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncCashWithdrawal(int userCode, int tillId, decimal cashAmount)
    {
        Response<ResultPointOfSaleResponseDto> result = await _pointOfSaleService.TCashWithdrawalAsync(userCode, tillId, cashAmount);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public IActionResult GiveBack(int userCode, int tillId, decimal giveBackAmount)
    {
        Response<ResultPointOfSaleResponseDto> result = _pointOfSaleService.TGiveBack(userCode, tillId, giveBackAmount);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncGiveBack(int userCode, int tillId, decimal giveBackAmount)
    {
        Response<ResultPointOfSaleResponseDto> result = await _pointOfSaleService.TGiveBackAsync(userCode, tillId, giveBackAmount);
        return ActionResultInstance(result);
    }
}
