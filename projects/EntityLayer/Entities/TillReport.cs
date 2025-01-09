using Core.Persistence.EntityBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities;

public class TillReport : EntityBase<int>
{
    public int TillId { get; set; }
    public int TotalDocumentCount { get; set; }
    public int TotalCancelationCount { get; set; }
    public decimal DocumentTotalAmount { get; set; }
    public decimal CancelataionTotalAmount { get; set; }
}
