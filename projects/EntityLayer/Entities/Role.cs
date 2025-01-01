using Core.Persistence.EntityBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities;

public class Role : EntityBase<int>
{
    public string Name { get; set; } = null!;

    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
}
