using Core.Persistence.EntityBaseModel;
using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities;

public class BasketPayment : EntityBase<int>
{
    public int BasketId { get; set; }
    public Basket Basket { get; set; } = null!;
    public PaymentType PaymentType { get; set; }
    public decimal Amount { get; set; }
}
