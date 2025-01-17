﻿using DataAccessLayer.Context;
using DataAccessLayer.Repositories.BasketItemRespositories;
using DataAccessLayer.Repositories.BasketPaymentRepositories;
using DataAccessLayer.Repositories.BasketRepositories;
using DataAccessLayer.Repositories.CategoryRepositories;
using DataAccessLayer.Repositories.CustomerRepositories;
using DataAccessLayer.Repositories.GiveBackListRepositories;
using DataAccessLayer.Repositories.PointOfSaleRepositories;
using DataAccessLayer.Repositories.ProductRepositories;
using DataAccessLayer.Repositories.RoleRepositories;
using DataAccessLayer.Repositories.StoreRepositories;
using DataAccessLayer.Repositories.TillRepositories;
using DataAccessLayer.Repositories.UserRepositories;
using DataAccessLayer.Repositories.UserRoleRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Extensions;

public static class DataAccessExtensions
{
    public static IServiceCollection AddDataAccessIoC(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DbConnection")));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IBasketItemRepository, BasketItemRepository>();
        services.AddScoped<ITillRepository, TillRepository>();
        services.AddScoped<IPointOfSaleRepository, PointOfSaleRepository>();
        services.AddScoped<IBasketPaymentRepository, BasketPaymentRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IGiveBackListRepository, GiveBackListRepository>();
        return services;
    }
}
