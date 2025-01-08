using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.PointOfSaleRequestDtos;

public record CreatePointOfSaleRequestDto(int UserCode, int TillId, decimal AdvanceWithdrawalTotal, decimal CashWithdrawalTotal, decimal SaleTotal, decimal GiveBackTotal, decimal DrawerTotal, decimal CashPaymentTotal, decimal CreditCardPaymentTotal, decimal OtherPaymentTotal, decimal GiftCardPaymentTotal) : IRequestDto
{
    public static PointOfSale ConvertToEntity(CreatePointOfSaleRequestDto createPointOfSaleRequestDto)
    {
        return new PointOfSale()
        {
            UserCode = createPointOfSaleRequestDto.UserCode,
            TillId = createPointOfSaleRequestDto.TillId,
            CreditCardPaymentTotal = createPointOfSaleRequestDto.CreditCardPaymentTotal,
            CashPaymentTotal = createPointOfSaleRequestDto.CashPaymentTotal,
            GiveBackTotal = createPointOfSaleRequestDto.GiveBackTotal,
            OtherPaymentTotal = createPointOfSaleRequestDto.OtherPaymentTotal,
            AdvanceWithdrawalTotal = createPointOfSaleRequestDto.AdvanceWithdrawalTotal,
            CashWithdrawalTotal = createPointOfSaleRequestDto.CashWithdrawalTotal,
            SaleTotal = createPointOfSaleRequestDto.SaleTotal,
            DrawerTotal = createPointOfSaleRequestDto.DrawerTotal,
            GiftCardPaymentTotal = createPointOfSaleRequestDto.GiftCardPaymentTotal,
            Created = DateTime.Now,
        };
    }
}
