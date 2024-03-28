using CommonCrm.Data.Entities;

namespace CommonCrm.Models;

public class LayoutViewModel
{
    public string UserName { get; set; }
    public List<ExchangeRate> ExchangeRates { get; set; }
}