using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Abstract;

public interface IGiveBackListRules
{
    void ProductExists(long barcodeNo);
    void CustomerExists(string identityNumber);
    void GiveBackExists(GiveBackList? giveBack);
}
