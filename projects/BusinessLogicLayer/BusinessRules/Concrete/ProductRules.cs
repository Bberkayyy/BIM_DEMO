using BusinessLogicLayer.BusinessRules.Abstract;
using Core.CrossCuttingConcerns;
using DataAccessLayer.Repositories.CategoryRepositories;
using DataAccessLayer.Repositories.ProductRepositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Concrete;

public class ProductRules : IProductRules
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductRules(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public void BarcodeNoMustBeFourteenCharacter(long barcodeNo)
    {
        if (barcodeNo < 10000000000000 || barcodeNo > 99999999999999)
            throw new BusinessException($"Barcode no must be between 10000000000000 and 99999999999999. Please enter a number in this range. ({barcodeNo})");
    }

    public bool BarcodeNoMustBeUnique(long barcodeNo)
    {
        return _productRepository.EntityExists(x => x.BarcodeNo == barcodeNo);
    }

    public void BarcodeNoMustBeUnique(long barcodeNo, int id = -1)
    {
        Product? product = _productRepository.GetByFilter(x => x.BarcodeNo == barcodeNo);
        if (product != null && product.Id != id)
        {
            if (product.Id != id)
                throw new BusinessException($"Product id cannot be changed!");
            if (product.BarcodeNo == barcodeNo)
                throw new BusinessException($"Barcode no is already exists! ({barcodeNo}). Enter a different barcode no.");
        }
    }

    public void CategoryExists(int categoryId)
    {
        Category? category = _categoryRepository.GetByFilter(x => x.Id == categoryId);
        if (category == null)
            throw new BusinessException($"Category not found!");
        if (category != null && category.Deleted != null)
            throw new BusinessException($"Category not found! Category deleted at {category.Deleted?.ToString("yyyy-MM-dd HH:mm")}.");
    }

    public void ExpirationTimeCannotBeExpired(DateTime? expiration)
    {
        if (expiration != null)
        {
            if (expiration?.Day < DateTime.Now.Day)
                if (expiration?.Month <= DateTime.Now.Month)
                    if (expiration?.Year <= DateTime.Now.Year)
                        throw new BusinessException($"The product has expired!");
        }
    }

    public short GetSelectedCategoryNo(int categoryId)
    {
        return _categoryRepository.GetByFilter(x => x.Id == categoryId)!.CategoryNo;
    }

    public void NameCannotBeNullOrWhiteSpace(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessException("Please enter a product name!");
    }

    public void PriceCannotBeNegative(decimal price)
    {
        if (price < 0)
            throw new BusinessException($"Product price cannot be negative! ({price})");
    }

    public void ProductExists(Product? product, bool isDeleteFromDatabase = false)
    {
        if (product == null)
            throw new BusinessException("Product not found!");
        if (!isDeleteFromDatabase && product.Deleted != null)
            throw new BusinessException($"Product not found! Product deleted at {product.Deleted?.ToString("yyyy-MM-dd HH:mm")}.");
    }

    public void ProductionTimeCannotBeFuture(DateTime? production)
    {
        if (production != null)
        {
            if (production?.Day > DateTime.Now.Day)
                if (production?.Month >= DateTime.Now.Month)
                    if (production?.Year >= DateTime.Now.Year)
                        throw new BusinessException($"The product has not been produced yet.");
        }
    }

    public void ShortCodeMustBeSevenCharacter(int shortCode)
    {
        if (shortCode < 1000000 || shortCode > 9999999)
            throw new BusinessException($"Short code must be between 1000000 and 9999999. Please enter a number in this range. ({shortCode})");
    }

    public void ShortCodeShouldStartWithCategoryNo(int shortCode, int categoryId)
    {
        string categoryNoInShortCode = shortCode.ToString()[..3];
        short categoryNo = GetSelectedCategoryNo(categoryId);

        if (categoryNo.ToString() != categoryNoInShortCode)
            throw new BusinessException($"Short code should start with category no! ({categoryNo}-{categoryNoInShortCode})");
    }

    public bool ShortCodeMustBeUnique(int shortCode)
    {
        return _productRepository.EntityExists(x => x.ShortCode == shortCode);
    }

    public void ShortCodeMustBeUnique(int shortCode, int id = -1)
    {
        Product? product = _productRepository.GetByFilter(x => x.ShortCode == shortCode);
        if (product != null && product.Id != id)
        {
            if (product.Id != id)
                throw new BusinessException($"Product id cannot be changed!");
            if (product.ShortCode == shortCode)
                throw new BusinessException($"Short code is already exists! ({shortCode}). Enter a different short code.");
        }
    }

    public void StockCannotBeNegative(int stock)
    {
        if (stock < 0)
            throw new BusinessException($"Product stock cannot be negative! ({stock})");
    }

    public void WeightCannotBeNegative(decimal? weight)
    {
        if (weight < 0)
            throw new BusinessException($"Product weight cannot be negative! ({weight})");
    }

    public void BarcodeNoShouldStartWithEightAndLastSevenCharacterShouldBeShortCode(long barcodeNo, int shortCode)
    {
        string barcodeFirstCharacter = barcodeNo.ToString()[..1];
        string barcodeLastSevenCharacter = barcodeNo.ToString().Substring(7, 7);

        if (barcodeFirstCharacter != "8")
            throw new BusinessException($"Barcode no should start with eight! ({barcodeFirstCharacter})");
        if (barcodeLastSevenCharacter != shortCode.ToString())
            throw new BusinessException($"Barcode no last seven character should be short code!");
    }
}