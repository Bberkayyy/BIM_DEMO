using Core.Persistence.EntityBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities;

public class Customer : EntityBase<int>
{
    public string IdentityNumber { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string TaxOffice { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }

    public ICollection<GiveBackList> GiveBackLists { get; set; } = new HashSet<GiveBackList>();
}
