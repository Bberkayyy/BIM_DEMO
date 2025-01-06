using Core.Persistence.EntityBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities;

public class Basket : EntityBase<int>
{
    public int UserCode { get; set; }
    public User User { get; set; } = null!;
    public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
}
