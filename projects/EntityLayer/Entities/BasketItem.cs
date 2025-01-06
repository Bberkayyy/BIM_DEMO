using Core.Persistence.EntityBaseModel;
using Microsoft.EntityFrameworkCore;
using System;

namespace EntityLayer.Entities;

public class BasketItem : EntityBase<int>
{
    public int? BasketId { get; set; }
    public Basket? Basket { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
