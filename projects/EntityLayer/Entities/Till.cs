using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities;

public class Till
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<PointOfSale> PointOfSales { get; set; } = new List<PointOfSale>();
}
