using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.BasketPaymentRequestDtos;

public record CreateBasketPaymentRequestDto(int BasketId, List<PaymentDetailDto> PaymentDetails)
{
    public static List<BasketPayment> ConvertToEntity(CreateBasketPaymentRequestDto createBasketPaymentRequestDto)
    {
        return createBasketPaymentRequestDto.PaymentDetails.Select(x => new BasketPayment
        {
            BasketId = createBasketPaymentRequestDto.BasketId,
            PaymentType = x.PaymentType,
            Amount = x.Amount,
            Created = DateTime.Now,
        }).ToList();
    }
}
