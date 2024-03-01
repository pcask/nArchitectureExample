using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities;
using Core.Utilities.Interceptors;
using System.Reflection;

namespace Core.Aspects.Autofac.Validation;

// Parametre geçilen validation tipinin barındırdığı tüm validation method'ları validate edilecek ilgili method'un öncesinde çalıştırıyoruz.
public class ValidationAspect(Type validationType) : MethodInterception
{
    private ValidationReturn validationReturn = ServicesTool.GetService<ValidationReturn>();

    public override void OnBefore(IInvocation invocation)
    {
        // Activator.CreateInstance(validationType); // kullanamıyorum çünkü validationType'in bağımlılıkları olabilir.
        object validationObject = ServicesTool.GetService(validationType);

        validationObject.GetType().GetMethods()                                // Burada sadece async method'ları ele alalım.
            .Where(m => m.GetCustomAttributes<ValidationMethod>(true).Any() && m.ReturnType.IsAssignableTo(typeof(Task)))
            .OrderBy(m => m.GetCustomAttribute<ValidationMethod>(true).Priority)
            .ToList().ForEach(m =>
            {
                var matchedArgs = m.GetParameters()
                .Select(p => invocation.Arguments.FirstOrDefault(a => a.GetType() == p.ParameterType)).ToArray()
                ?? throw new Exception("There are no arguments matching the validation parameter type!");

                var asyncResult = m.Invoke(validationObject, matchedArgs) as Task;

                asyncResult.GetAwaiter().GetResult();

                // Validation Method'ların geri dönüş değerleri ile işlem yapılmak istenirse;
                //var pi = asyncResult.GetType().GetProperty("Result");
                //if (pi.PropertyType == Type.GetType("System.Threading.Tasks.VoidTaskResult"))
                //return;

                // Yukarıda Task<object> tipine cast edemediğim için Task'e çevirip ardından Task void dönüşü ifade ettiği için dolaylı olarak Result değerini elde ettim.
                // dynamic result = pi.GetValue(asyncResult);

            });
    }

    public override void Intercept(IInvocation invocation)
    {
        base.Intercept(invocation);
        //validationReturn.Reset();
    }
}