using EntityLayer.Entities;
using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.ResponseDtos.BasketPaymentResponseDtos;

public record ResultBasketPaymentResponseDto(int BasketPaymentId, int BasketId, PaymentType PaymentType, decimal Amount)
{
    public static List<ResultBasketPaymentResponseDto> ConvertToResponse(List<BasketPayment> entities)
    {
        return entities.Select(x => new ResultBasketPaymentResponseDto(
            BasketPaymentId: x.Id,
            BasketId: x.BasketId,
            PaymentType: x.PaymentType,
            Amount: x.Amount)).ToList();
    }
}
