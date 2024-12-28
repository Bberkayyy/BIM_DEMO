using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Abstract;

public interface IProductRules
{
    void BarcodeNoMustBeUnique(long barcodeNo);
    void CategoryExists(int categoryId);
    void ShortCodeMustBeUnique(int shortCode);
    void NameCannotBeNullOrWhiteSpace(string name);
    void StockCannotBeNegative(int stock);
    void WeightCannotBeNegative(decimal weight);
    void ExpirationTimeCannotBeExpired(DateTime expiration);
    void ProductionTimeCannotBeFuture(DateTime expiration);
    void ProductExists(Product? product);
    void ProductDeleted(Product? product);
}
/*
    void CategoryNoMustBeUnique(short categoryNo, int id = -1);
    void CategoryNameMustBeUnique(string categoryName);
    void CategoryExists(Category? category);
    void CategoryNameCanNotBeNullOrWhiteSpace(string categoryName);
    void CategoryDeleted(int id);
    void CategoryDeleted(short categoryNo);
    void CategoryDeleted(string name);
 */
