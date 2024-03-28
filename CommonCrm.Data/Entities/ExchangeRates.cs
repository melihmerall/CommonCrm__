namespace CommonCrm.Data.Entities;

public class ExchangeRate: BaseEntity
{
    public string CurrencyCode { get; set; }
    public decimal Rate { get; set; }


}