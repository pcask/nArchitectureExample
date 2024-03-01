using Autofac;
using Module = Autofac.Module;
using System.Reflection;
using static Core.DependencyResolvers.Autofac.AutofacHelper;
using Core.Abstracts;
using System.ComponentModel.DataAnnotations;
using Core.CrossCuttingConcerns.Validation;

namespace Core.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Infrastructure
        // builder.RegisterType<CheckIdentityTRAdapter>().Keyed<ICheckIdentityService>("TR").InstancePerLifetimeScope();
        // Öncesinde Keyed Service olarak eklemiştim fakat Autofac ile service'i manual olarak resolve etmek zahmetli geldi.
        // UserValidation, ICheckIdentityService tipine bağlımlı ve manual resolve işlemi için dıdısının dısınından icazet almak gerekiyor.
        // Örnek bir resolve işlemini ServiceTool.cs içerisinde yazdım, yorum satırı halinde, beyaz atlı prensini bekliyor.


        // "keyed" olarak eklediğimiz ICheckIdentityService'in UserValidation içerisinde çözümlenebilmesi için;
        // aşağıdaki gibi kullanılmalı fakat ICheckIdentityService'i keyed olarak eklemekten vazgeçtim.
        // builder.RegisterType<UserValidations>().WithAttributeFiltering().InstancePerLifetimeScope();

        // Validation'ların sahip olduğu method'ları intercept etmiyeceğimiz için buradan kayıt olmalarına gerek yok. 
        // RegisterByEndName(builder, Assembly.GetExecutingAssembly(), "Validations", ServiceLifeTime.InstancePerLifetimeScope, false);


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

        // Manager'ları ayrıca interface'siz kaydediyorum, runtime'da direkt olarak concerete tip ile servis çözümlemesi yapabilmek için.
        //RegisterByEndName(builder, Assembly.GetExecutingAssembly(), "Manager", ServiceLifeTime.InstancePerLifetimeScope, false);

        // Autofac IoC container'a eklenen tüm service'leri ele almak için; 
        //var registeredServices = builder.Build().ComponentRegistry.Registrations;
    }
}
