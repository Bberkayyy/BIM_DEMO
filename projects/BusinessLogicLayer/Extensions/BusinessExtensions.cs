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
        services.AddScoped<IRoleService, RoleManager>();
        services.AddScoped<IUserRoleService, UserRoleManager>();
        services.AddScoped<IBasketService, BasketManager>();
        services.AddScoped<IBasketItemService, BasketItemManager>();
        services.AddScoped<IPointOfSaleService, PointOfSaleManager>();
        services.AddScoped<ICustomerService, CustomerManager>();
        services.AddScoped<IGiveBackListService, GiveBackListManager>();

        services.AddScoped<ICategoryRules, CategoryRules>();
        services.AddScoped<IProductRules, ProductRules>();
        services.AddScoped<IStoreRules, StoreRules>();
        services.AddScoped<IRoleRules, RoleRules>();
        services.AddScoped<IUserRoleRules, UserRoleRules>();
        services.AddScoped<IUserRules, UserRules>();
        services.AddScoped<IBasketItemRules, BasketItemRules>();
        services.AddScoped<IBasketRules, BasketRules>();
        services.AddScoped<IPointOfSaleRules, PointOfSaleRules>();
        services.AddScoped<ICustomerRules, CustomerRules>();
        services.AddScoped<IGiveBackListRules, GiveBackListRules>();
        return services;
    }
}
