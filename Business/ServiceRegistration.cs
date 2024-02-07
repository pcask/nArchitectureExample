using Business.Abstracts;
using Business.Concretes;
using Business.Validations;
using Core.Abstracts;
using Core.Adapters;
using Core.Security.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class ServiceRegistration
{
    public static void RegisterBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<AuthValidations>();
        services.AddScoped<UserValidations>();
        services.AddScoped<ClaimValidations>();
        services.AddScoped<UserClaimValidations>();
        services.AddScoped<CategoryValidations>();
        services.AddScoped<ProductValidations>();
        services.AddScoped<ProductTransactionValidations>();
        services.AddScoped<OrderValidations>();
        services.AddScoped<OrderDetailValidations>();
        services.AddScoped<CardTypeValidations>();
        services.AddScoped<CardValidations>();
        services.AddScoped<CardTransactionValidations>();

        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IClaimService, ClaimManager>();
        services.AddScoped<IUserClaimService, UserClaimManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<IProductTransactionService, ProductTransactionManager>();
        services.AddScoped<IOrderService, OrderManager>();
        services.AddScoped<IOrderDetailService, OrderDetailManager>();
        services.AddScoped<ICardTypeService, CardTypeManager>();
        services.AddScoped<ICardService, CardManager>();
        services.AddScoped<ICardTransactionService, CardTransactionManager>();
    }
}
