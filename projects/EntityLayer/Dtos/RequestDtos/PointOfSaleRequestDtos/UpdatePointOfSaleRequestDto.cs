using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.PointOfSaleRequestDtos;

public record UpdatePointOfSaleRequestDto(int Id, int UserCode, int TillId, decimal AdvanceWithdrawalTotal, decimal CashWithdrawalTotal, decimal SaleTotal, decimal GiveBackTotal, decimal DrawerTotal, decimal CashPaymentTotal, decimal CreditCardPaymentTotal, decimal OtherPaymentTotal, decimal GiftCardPaymentTotal, DateTime Created, DateTime? Updated, DateTime? Deleted) : IRequestDto
{
    public static PointOfSale ConvertToEntity(UpdatePointOfSaleRequestDto updatePointOfSaleRequestDto)
    {
        return new PointOfSale
        {
            Id = updatePointOfSaleRequestDto.Id,
            UserCode = updatePointOfSaleRequestDto.UserCode,
            TillId = updatePointOfSaleRequestDto.TillId,
            CreditCardPaymentTotal = updatePointOfSaleRequestDto.CreditCardPaymentTotal,
            CashPaymentTotal = updatePointOfSaleRequestDto.CashPaymentTotal,
            GiveBackTotal = updatePointOfSaleRequestDto.GiveBackTotal,
            OtherPaymentTotal = updatePointOfSaleRequestDto.OtherPaymentTotal,
            AdvanceWithdrawalTotal = updatePointOfSaleRequestDto.AdvanceWithdrawalTotal,
            CashWithdrawalTotal = updatePointOfSaleRequestDto.CashWithdrawalTotal,
            SaleTotal = updatePointOfSaleRequestDto.SaleTotal,
            DrawerTotal = updatePointOfSaleRequestDto.DrawerTotal,
            GiftCardPaymentTotal = updatePointOfSaleRequestDto.GiftCardPaymentTotal,
            Created = updatePointOfSaleRequestDto.Created,
            Updated = DateTime.Now
        };
    }
}
