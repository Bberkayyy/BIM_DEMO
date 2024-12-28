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

    public void BarcodeNoMustBeUnique(long barcodeNo)
    {
        Product? product = _productRepository.GetByFilter(x => x.BarcodeNo == barcodeNo);
        if (product != null)
            throw new BusinessException($"Barcode no is already exists! ({barcodeNo}). Enter a diffrerent");
    }

    public void CategoryExists(int categoryId)
    {
        throw new NotImplementedException();
    }

    public void ExpirationTimeCannotBeExpired(DateTime expiration)
    {
        throw new NotImplementedException();
    }

    public void NameCannotBeNullOrWhiteSpace(string name)
    {
        throw new NotImplementedException();
    }

    public void ProductDeleted(Product? product)
    {
        throw new NotImplementedException();
    }

    public void ProductExists(Product? product)
    {
        throw new NotImplementedException();
    }

    public void ProductionTimeCannotBeFuture(DateTime expiration)
    {
        throw new NotImplementedException();
    }

    public void ShortCodeMustBeUnique(int shortCode)
    {
        throw new NotImplementedException();
    }

    public void StockCannotBeNegative(int stock)
    {
        throw new NotImplementedException();
    }

    public void WeightCannotBeNegative(decimal weight)
    {
        throw new NotImplementedException();
    }
}
