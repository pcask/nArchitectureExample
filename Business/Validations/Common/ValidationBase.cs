using Core.CrossCuttingConcerns.Validation;
using Core.Utilities;

namespace Business.Validations.Common;

public abstract class ValidationBase
{
    protected readonly ValidationReturn ValidationReturn;

    public ValidationBase()
    {
        ValidationReturn = ServicesTool.GetService<ValidationReturn>();
    }
}