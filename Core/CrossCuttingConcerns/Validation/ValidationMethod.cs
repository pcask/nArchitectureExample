using Core.Entities.Security;

namespace Core.CrossCuttingConcerns.Validation;


[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ValidationMethod(byte Priority = 0) : Attribute
{
    public short Priority { get; } = Priority;
}

public record ValidationReturn
{
    public AppUser User { get; set; }
    public bool NoNeedToGoToDb { get; set; }
}