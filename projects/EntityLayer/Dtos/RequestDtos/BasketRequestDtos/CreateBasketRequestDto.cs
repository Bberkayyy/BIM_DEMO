using Core.Persistence.DtoBaseModel;
using EntityLayer.Dtos.RequestDtos.BasketPaymentRequestDtos;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.BasketRequestDtos;

public record CreateBasketRequestDto(int userCode, List<PaymentDetailDto> paymentDetails) : IRequestDto
{
    public static Basket ConvertToEntity(CreateBasketRequestDto createBasketRequestDto, List<BasketItem> basketItems, List<BasketPayment> basketPayments)
    {
        return new Basket()
        {
            UserCode = createBasketRequestDto.userCode,
            BasketItems = basketItems,
            BasketPayments = basketPayments,
            Created = DateTime.Now,
        };
    }
}
