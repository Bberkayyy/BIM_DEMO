using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.PointOfSaleResponseDtos;

public record ResultPointOfSaleResponseDto(int Id, int UserCode, string UserFullName, string TillName, decimal CreditCardPaymentTotal, decimal CashPaymentTotal, decimal OtherPaymentTotal, decimal GiftCardPaymentTotal, DateTime CheckinDate, int CheckinTime, DateTime? CheckoutDate, int? CheckoutTime)
{
    public static ResultPointOfSaleResponseDto ConvertToResponse(PointOfSale entity)
    {
        return new ResultPointOfSaleResponseDto(Id: entity.Id,
                                                UserCode: entity.UserCode,
                                                UserFullName: $"{entity.User.Name} {entity.User.Surname}",
                                                TillName: entity.Till.Name,
                                                CreditCardPaymentTotal: entity.CreditCardPaymentTotal,
                                                CashPaymentTotal: entity.CashPaymentTotal,
                                                OtherPaymentTotal: entity.OtherPaymentTotal,
                                                GiftCardPaymentTotal: entity.GiftCardPaymentTotal,
                                                CheckinDate: entity.Created.Date,
                                                CheckinTime: entity.Created.Hour,
                                                CheckoutDate: entity.Deleted?.Date,
                                                CheckoutTime: entity.Deleted?.Hour);
    }
}
