using Core.Persistence.EntityBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities;

public class PointOfSale : EntityBase<int>
{
    public int UserCode { get; set; }
    public User User { get; set; } = null!;
    public int TillId { get; set; }
    public Till Till { get; set; } = null!;
    public decimal AdvanceWithdrawalTotal { get; set; }
    public decimal CashWithdrawalTotal { get; set; }
    public decimal SaleTotal { get; set; }
    public decimal GiveBackTotal { get; set; }
    public decimal DrawerTotal { get; set; }
    public decimal CashPaymentTotal { get; set; }
    public decimal CreditCardPaymentTotal { get; set; }
    public decimal OtherPaymentTotal { get; set; }
    public decimal GiftCardPaymentTotal { get; set; }
}
