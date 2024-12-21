using Core.Persistence.EntityBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities;

public class Store : EntityBase<int>
{
    public int StoreNo { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public int PhoneNumber { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}
