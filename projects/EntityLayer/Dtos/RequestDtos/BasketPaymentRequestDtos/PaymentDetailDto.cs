using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.BasketPaymentRequestDtos;

public record PaymentDetailDto(PaymentType PaymentType, decimal Amount)
{
}
