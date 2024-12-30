using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Abstract;

public interface IProductRules
{
    bool BarcodeNoMustBeUnique(long barcodeNo);
    void BarcodeNoMustBeUnique(long barcodeNo, int id = -1);
    void BarcodeNoMustBeFourteenCharacter(long barcodeNo);
    void BarcodeNoShouldStartWithEightAndLastSevenCharacterShouldBeShortCode(long barcodeNo, int shortCode);
    void CategoryExists(int categoryId);
    bool ShortCodeMustBeUnique(int shortCode);
    void ShortCodeShouldStartWithCategoryNo(int shortCode, int categoryId);
    void ShortCodeMustBeUnique(int shortCode, int id = -1);
    void ShortCodeMustBeSevenCharacter(int shortCode);
    void NameCannotBeNullOrWhiteSpace(string name);
    void PriceCannotBeNegative(decimal price);
    void StockCannotBeNegative(int stock);
    void WeightCannotBeNegative(decimal? weight);
    void ExpirationTimeCannotBeExpired(DateTime? expiration);
    void ProductionTimeCannotBeFuture(DateTime? production);
    void ProductExists(Product? product, bool isDeleteFromDatabase = false);
    short GetSelectedCategoryNo(int categoryId);
}