using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.Extensions.Dtos.PosDtos;
using Core.Shared;
using EntityLayer.Dtos.RequestDtos.PointOfSaleRequestDtos;
using EntityLayer.Dtos.ResponseDtos.PointOfSaleResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
    public IActionResult AdvanceWithdrawal(AdvanceWithdrawalDto advanceWithdrawalDto)
    {
        Response<ResultPointOfSaleResponseDto> result = _pointOfSaleService.TAdvanceWithdrawal(advanceWithdrawalDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncAdvanceWithdrawal(AdvanceWithdrawalDto advanceWithdrawalDto)
    {
        Response<ResultPointOfSaleResponseDto> result = await _pointOfSaleService.TAdvanceWithdrawalAsync(advanceWithdrawalDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public IActionResult CashWithdrawal(CashWithdrawalDto cashWithdrawalDto)
    {
        Response<ResultPointOfSaleResponseDto> result = _pointOfSaleService.TCashWithdrawal(cashWithdrawalDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncCashWithdrawal(CashWithdrawalDto cashWithdrawalDto)
    {
        Response<ResultPointOfSaleResponseDto> result = await _pointOfSaleService.TCashWithdrawalAsync(cashWithdrawalDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public IActionResult GiveBack(GiveBackDto giveBackDto)
    {
        Response<ResultPointOfSaleResponseDto> result = _pointOfSaleService.TGiveBack(giveBackDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncGiveBack(GiveBackDto giveBackDto)
    {
        Response<ResultPointOfSaleResponseDto> result = await _pointOfSaleService.TGiveBackAsync(giveBackDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public IActionResult CashierExitReport(int userCode, int tillId)
    {
        Response<ResultPointOfSaleResponseDto> result = _pointOfSaleService.TCashierExitReport(userCode, tillId);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncCashierExitReport(int userCode, int tillId)
    {
        Response<ResultPointOfSaleResponseDto> result =await _pointOfSaleService.TCashierExitReportAsync(userCode, tillId);
        return ActionResultInstance(result);
    }
}
