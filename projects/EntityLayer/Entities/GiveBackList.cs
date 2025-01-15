using Core.Persistence.EntityBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities;

public class GiveBackList : EntityBase<int>
{
    public string CustomerIdentityNumber { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public long ProductBarcodeNo { get; set; }
    public Product Product { get; set; } = null!;
    public string ReasonForReturn { get; set; } = null!;
    public bool IsReturnCash { get; set; }
    public decimal ProductQuantity { get; set; }
    public decimal ProductPrice { get; set; }
}
