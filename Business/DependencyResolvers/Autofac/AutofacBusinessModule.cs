using Autofac;
using Module = Autofac.Module;
using Business.Abstracts;
using Business.Logging;
using Business.Validations;
using System.Reflection;
using Core.Security.JWT;
using Core.Abstracts;
using Infrastructure.Adapters;
using Autofac.Features.AttributeFilters;
using static Business.DependencyResolvers.Autofac.AutofacHelper;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Core
        builder.RegisterType<JWTTokenHelper>().As<ITokenHelper>().SingleInstance();

        // Infrastructure
        builder.RegisterType<CheckIdentityTRAdapter>().Keyed<ICheckIdentityService>("TR").InstancePerLifetimeScope();
        builder.RegisterType<CheckIdentityUSAAdapter>().Keyed<ICheckIdentityService>("USA").InstancePerLifetimeScope();

        // Business
        builder.RegisterType<ConsoleLoggerManager>().As<ILoggerService>().SingleInstance();
        builder.RegisterType<AuthValidations>().InstancePerLifetimeScope();

        // "keyed" olarak eklediğimiz ICheckIdentityService'in UserValidation içerisinde çözümlenebilmesi için; WithAttributeFiltering()
        builder.RegisterType<UserValidations>().WithAttributeFiltering().InstancePerLifetimeScope();


        //builder.RegisterType<ClaimValidations>().InstancePerLifetimeScope();
        //builder.RegisterType<UserClaimValidations>().InstancePerLifetimeScope();
        //builder.RegisterType<CategoryValidations>().InstancePerLifetimeScope();
        //builder.RegisterType<ProductValidations>().InstancePerLifetimeScope();
        //builder.RegisterType<ProductTransactionValidations>().InstancePerLifetimeScope();
        //builder.RegisterType<OrderValidations>().InstancePerLifetimeScope();
        //builder.RegisterType<OrderDetailValidations>().InstancePerLifetimeScope();
        //builder.RegisterType<CardTypeValidations>().InstancePerLifetimeScope();
        //builder.RegisterType<CardValidations>().InstancePerLifetimeScope();
        //builder.RegisterType<CardTransactionValidations>().InstancePerLifetimeScope();
        RegisterByEndName(builder, Assembly.GetExecutingAssembly(), "Validations", ServiceLifeTime.InstancePerLifetimeScope, false);


        //builder.RegisterType<UserManager>().As<IUserService>().InstancePerLifetimeScope();
        //builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerLifetimeScope();
        //builder.RegisterType<ClaimManager>().As<IClaimService>().InstancePerLifetimeScope();
        //builder.RegisterType<UserClaimManager>().As<IUserClaimService>().InstancePerLifetimeScope();
        //builder.RegisterType<CategoryManager>().As<ICategoryService>().InstancePerLifetimeScope();
        //builder.RegisterType<ProductManager>().As<IProductService>().InstancePerLifetimeScope();
        //builder.RegisterType<ProductTransactionManager>().As<IProductTransactionService>().InstancePerLifetimeScope();
        //builder.RegisterType<OrderManager>().As<IOrderService>().InstancePerLifetimeScope();
        //builder.RegisterType<OrderDetailManager>().As<IOrderDetailService>().InstancePerLifetimeScope();
        //builder.RegisterType<CardTypeManager>().As<ICardTypeService>().InstancePerLifetimeScope();
        //builder.RegisterType<CardManager>().As<ICardService>().InstancePerLifetimeScope();
        //builder.RegisterType<CardTransactionManager>().As<ICardTransactionService>().InstancePerLifetimeScope();
        RegisterByEndName(builder, Assembly.GetExecutingAssembly(), "Manager", ServiceLifeTime.InstancePerLifetimeScope);


        // Autofac IoC container'a eklenen tüm service'leri ele almak için; 
        // var registeredServices = builder.Build().ComponentRegistry.Registrations;
    }
}
