namespace Core.CrossCuttingConcerns.Validation;

public class ValidationReturn
{
    public dynamic Entity { get; set; }
    public bool NoNeedToGoToDb { get; set; }


    public void Reset()
    {
        Entity = null;
        NoNeedToGoToDb = false;
    }
}