using Core.Persistence.DtoBaseModel;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos.RequestDtos.BasketRequestDtos;

public record CreateBasketRequestDto(int userCode) : IRequestDto
{
    public static Basket ConvertToEntity(CreateBasketRequestDto createBasketRequestDto, List<BasketItem> basketItems)
    {
        return new Basket()
        {
            UserCode = createBasketRequestDto.userCode,
            BasketItems = basketItems,
            Created = DateTime.Now
        };
    }
}
