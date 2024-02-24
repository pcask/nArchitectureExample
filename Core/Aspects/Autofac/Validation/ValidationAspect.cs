using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities;
using Core.Utilities.Interceptors;
using System.Reflection;

namespace Core.Aspects.Autofac.Validation;

// Parametre geçilen validation tipinin barındırdığı tüm validation method'ları validate edilecek ilgili method'un öncesinde çalıştırıyoruz.
public class ValidationAspect(Type validationType) : MethodInterception
{
    public override void OnBefore(IInvocation invocation)
    {
        // Activator.CreateInstance(validationType); // kullanamıyorum çünkü validationType'in bağımlılıkları olabilir.
        object obj = ServicesTool.GetAutofacService(validationType);

        obj.GetType().GetMethods()                                         // Burada sadece async method'ları ele alalım.
            .Where(m => m.GetCustomAttributes<ValidationMethod>(true).Any() && m.ReturnType.IsAssignableTo(typeof(Task)))
            .OrderBy(m => m.GetCustomAttribute<ValidationMethod>(true).Priority)
            .ToList().ForEach(m => // foreach başlangıcında unuttuğum "async" ifade yüzünde fırlatılan exceptionlar CustomExceptionMiddleware tarafından yakalanmıyordu!!!
            {
                var matchedArgs = m.GetParameters()
                .Select(p => invocation.Arguments.FirstOrDefault(a => a.GetType() == p.ParameterType)).ToArray()
                ?? throw new Exception("There are no argument matching the validation parameter type!");

                var asyncResult = m.Invoke(obj, matchedArgs) as Task;

                asyncResult.GetAwaiter().GetResult();

                var pi = asyncResult.GetType().GetProperty("Result");
                if (pi.PropertyType == Type.GetType("System.Threading.Tasks.VoidTaskResult"))
                    return;

                // Yukarıda Task<object> tipine cast edemediğim için Task'e çevirip ardından Task void dönüşü ifade ettiği için dolaylı olarak Result değerini elde ettim.
                dynamic result = pi.GetValue(asyncResult);

                var index = invocation.Method.GetParameters().ToList().FindIndex(p => p.ParameterType == typeof(ValidationReturn));

                if (index != -1)
                    invocation.Arguments[index] = result as ValidationReturn;

            });
    }
}