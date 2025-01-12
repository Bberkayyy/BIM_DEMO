using Core.Shared;
using EntityLayer.Dtos.RequestDtos.PointOfSaleRequestDtos;
using EntityLayer.Dtos.ResponseDtos.PointOfSaleResponseDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstract;

public interface IPointOfSaleService
{
    Response<ResultPointOfSaleResponseDto> TCashierCheckIn(CreatePointOfSaleRequestDto createPointOfSaleRequestDto);
    Response<ResultPointOfSaleResponseDto> TCashierCheckOutByUserCode(int userCode);
    Response<List<ResultPointOfSaleResponseDto>> TGetAll(Expression<Func<PointOfSale, bool>>? predicate = null, Func<IQueryable<PointOfSale>, IIncludableQueryable<PointOfSale, object>>? include = null);
    Response<ResultPointOfSaleResponseDto> TGetByFilter(Expression<Func<PointOfSale, bool>> predicate, Func<IQueryable<PointOfSale>, IIncludableQueryable<PointOfSale, object>>? include = null);

    Task<Response<ResultPointOfSaleResponseDto>> TCashierCheckInAsync(CreatePointOfSaleRequestDto createPointOfSaleRequestDto);
    Task<Response<ResultPointOfSaleResponseDto>> TCashierCheckOutByUserCodeAsync(int userCode);
    Task<Response<List<ResultPointOfSaleResponseDto>>> TGetAllAsync(Expression<Func<PointOfSale, bool>>? predicate = null, Func<IQueryable<PointOfSale>, IIncludableQueryable<PointOfSale, object>>? include = null);
    Task<Response<ResultPointOfSaleResponseDto>> TGetByFilterAsync(Expression<Func<PointOfSale, bool>> predicate, Func<IQueryable<PointOfSale>, IIncludableQueryable<PointOfSale, object>>? include = null);

    Response<ResultPointOfSaleResponseDto> TAdvanceWithdrawal(int userCode, int tillId, decimal advanceAmount);
    Response<ResultPointOfSaleResponseDto> TCashWithdrawal(int userCode, int tillId, decimal cashAmount);
    Response<ResultPointOfSaleResponseDto> TGiveBack(int userCode, int tillId, decimal giveBackAmount, bool isReturnInCash);

    Task<Response<ResultPointOfSaleResponseDto>> TAdvanceWithdrawalAsync(int userCode, int tillId, decimal advanceAmount);
    Task<Response<ResultPointOfSaleResponseDto>> TCashWithdrawalAsync(int userCode, int tillId, decimal cashAmount);
    Task<Response<ResultPointOfSaleResponseDto>> TGiveBackAsync(int userCode, int tillId, decimal giveBackAmount, bool isReturnInCash);

    Response<ResultPointOfSaleResponseDto> TCashierExitReport(int userCode, int tillId);
    Task<Response<ResultPointOfSaleResponseDto>> TCashierExitReportAsync(int userCode, int tillId);
}
