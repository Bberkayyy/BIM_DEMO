using EntityLayer.Dtos.RequestDtos.CustomerRequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Extensions.Dtos.PosDtos;

public record GiveBackDto(int userCode, int tillId, long productBarcodeNo, decimal productQuantity, decimal giveBackAmount, string ReasonForReturn, bool isReturnCash, CreateCustomerRequestDto createCustomerRequestDto)
{
}
