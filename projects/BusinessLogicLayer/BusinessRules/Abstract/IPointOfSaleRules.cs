using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Abstract;

public interface IPointOfSaleRules
{
    void TillExists(int tillId);
    void UserExists(int userCode);
    void PointOfSaleExists(PointOfSale pointOfSale);
}
