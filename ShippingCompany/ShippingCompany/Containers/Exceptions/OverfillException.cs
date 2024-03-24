namespace ShippingCompany;

public class OverfillException : Exception
{
    public OverfillException(string massage) : base(massage)
    {
    }
}