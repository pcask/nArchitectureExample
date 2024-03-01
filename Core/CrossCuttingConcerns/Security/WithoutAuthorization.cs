namespace Core.CrossCuttingConcerns.Security;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class WithoutAuthorization : Attribute
{
}