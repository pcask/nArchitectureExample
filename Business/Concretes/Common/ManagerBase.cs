using Core.CrossCuttingConcerns.Validation;
using Core.Utilities;

namespace Business.Concretes.Common;

public abstract class ManagerBase
{
    protected readonly ValidationReturn ValidationReturn;

    protected ManagerBase()
    {
        ValidationReturn = ServicesTool.GetService<ValidationReturn>();
    }
}
