namespace Core.CrossCuttingConcerns.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class MustBeAuthorized : Attribute
{
}
