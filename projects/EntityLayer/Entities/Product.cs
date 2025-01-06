using Core.Persistence.EntityBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities;

public class Product : EntityBase<int>
{
    public long BarcodeNo { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public int ShortCode { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public decimal? Weight { get; set; }
    public DateTime? Expiration { get; set; }
    public DateTime? Production { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
}
