using BusinessLogicLayer.BusinessRules.Abstract;
using Core.CrossCuttingConcerns;
using DataAccessLayer.Repositories.BasketItemRespositories;
using DataAccessLayer.Repositories.ProductRepositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Concrete;

public class BasketItemRules : IBasketItemRules
{
    public void BarcodeNoMustBeDigitAndFourteenCharacter(long? barcodeNo)
    {
        if (barcodeNo.ToString()!.Length != 14 && !barcodeNo!.ToString()!.All(char.IsDigit))
            throw new BusinessException("Barcode no must be fourteen digit character");
    }

    public void BasketItemExists(BasketItem? item)
    {
        if (item == null)
            throw new BusinessException("Item not found!");
    }

    public void ProductExists(Product? product)
    {
        if (product == null)
            throw new BusinessException("Product not found!");
    }

    public void QuantityMustBeExists(BasketItem basketItem, decimal quantity)
    {
        if (basketItem.Quantity != quantity)
            throw new BusinessException("Item not found!");
    }

    public void ShortCodeMustBeDigitAndSevenCharacter(int? shortCode)
    {
        if (shortCode.ToString()!.Length != 7 && !shortCode.ToString()!.All(char.IsDigit))
            throw new BusinessException("Short code must be seven digit character!");
    }
}
