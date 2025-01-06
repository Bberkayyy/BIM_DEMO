using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Abstract;

public interface IBasketItemRules
{
    void BarcodeNoMustBeDigitAndFourteenCharacter(long? barcodeNo);
    void ShortCodeMustBeDigitAndSevenCharacter(int? shortCode);
    void ProductExists(Product? product);
    void BasketItemExists(BasketItem? item);
    void QuantityMustBeExists(BasketItem basketItem, decimal quantity);
}
