using Core.Persistence.EntityBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities;

public class User : EntityBase<int>
{
    public int StoreId { get; set; }
    public Store Store { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string IdentityNumber { get; set; } = null!;
    public int UserCode { get; set; }
    public int Password { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    public ICollection<Basket> Baskets { get; set; } = new List<Basket>();
    public ICollection<PointOfSale> PointOfSales { get; set; } = new List<PointOfSale>();
}
