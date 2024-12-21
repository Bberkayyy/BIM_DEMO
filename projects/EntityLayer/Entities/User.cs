using Core.Persistence.EntityBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities;

public class User : EntityBase<int>
{
    public int StoreNo { get; set; }
    public Store Store { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public int UserCode { get; set; }
    public int Password { get; set; }
}
