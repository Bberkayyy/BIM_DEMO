using BusinessLogicLayer.BusinessRules.Abstract;
using Core.CrossCuttingConcerns;
using DataAccessLayer.Repositories.CustomerRepositories;
using DataAccessLayer.Repositories.ProductRepositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Concrete;

public class GiveBackListRules : IGiveBackListRules
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;

    public GiveBackListRules(ICustomerRepository customerRepository, IProductRepository productRepository)
    {
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }

    public void CustomerExists(string identityNumber)
    {
        Customer? customer = _customerRepository.GetByFilter(x => x.IdentityNumber == identityNumber);
        if (customer == null)
            throw new BusinessException("Customer not found!");
    }

    public void GiveBackExists(GiveBackList? giveBack)
    {
        if (giveBack is null)
            throw new BusinessException("Give back not found!");
    }

    public void ProductExists(long barcodeNo)
    {
        Product? product = _productRepository.GetByFilter(x => x.BarcodeNo == barcodeNo);
        if (product == null)
            throw new BusinessException("Product not found!");
    }
}
