using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using BusinessLogicLayer.BusinessRules.Concrete;
using BusinessLogicLayer.Concrete;
using DataAccessLayer.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Extensions;

public static class BusinessExtensions
{
    public static IServiceCollection AddBusinessIoC(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<IStoreService, StoreManager>();
        services.AddScoped<IUserService, UserManager>();

        services.AddScoped<ICategoryRules, CategoryRules>();
        services.AddScoped<IProductRules, ProductRules>();
        return services;
    }
}
