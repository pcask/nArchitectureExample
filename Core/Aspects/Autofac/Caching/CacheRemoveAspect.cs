using Autofac;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities;
using Core.Utilities.Interceptors;
using System.Reflection;

namespace Core.Aspects.Autofac.Caching;

// Update, delete işlemlerinde Cache'deki data eski kalacağı için, ilgili Manager sınıfı içerisindeki Cache'lenen dataların silinmesi sağlanacak
// ki bu sayede yeniden Cache'lenebilsinler.
public class CacheRemoveAspect : MethodInterception
{
    private readonly ICacheService _cacheService;

    public CacheRemoveAspect()
    {
        _cacheService = ServicesTool.GetService<ICacheService>();
    }

    // Update veya Delete işlemleri başarılı olursa ilgili Cache'ler silinecek aksi takdirde silinmelerine gerek yok.
    public override void OnSuccess(IInvocation invocation)
    {
        string baseName = invocation.Method.DeclaringType.FullName;

        var managerActivator = ServicesTool.GetAutofacActivator(invocation.TargetType);

        // İlgili sınıf içerisindeki cache'lenebilecek olan tüm method'ları ele alalım;
        var methods = managerActivator.LimitType.GetMethods().Where(m => m.GetCustomAttributes<CacheAspect>().Any()).ToList();

        if (methods.Count > 0)
        {
            var keys = _cacheService.GetKeys(); // IMemoryCache'den direk olarak key'leri alamadım bende IMemoryCache ile senkron olacak şekilde sadece key'leri store ettim.

            List<string> foundedKeys = [];
            methods.ForEach(m =>
             {
                 var parameters = m.GetParameters().ToList();
                 // Key'ler içerisinden sadece method isimleri ve varsa parametre isimleri ile eşleşen key'leri Cache'den sildim.
                 var query = keys.Where(k => k.StartsWith(baseName + "." + m.Name)).AsQueryable();

                 if (parameters.Count > 0)
                 {
                     parameters.ForEach(p =>
                     {
                         query = query.Where(k => k.Contains(p.Name));
                     });
                 }

                 foundedKeys.AddRange(query);
             });

            foundedKeys.ForEach(_cacheService.Remove);
        }
    }
}