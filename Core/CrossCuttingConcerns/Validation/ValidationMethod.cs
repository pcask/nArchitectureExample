namespace Core.CrossCuttingConcerns.Validation;


[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class ValidationMethod(byte Priority = 0) : Attribute
{
    public short Priority { get; } = Priority;
}