namespace CommonCrm.Data.Entities;

public class CountryStates: BaseEntity
{
    public string CountryName { get; set; }
    public string ISO3 { get; set; }
    public string StateName { get; set; }
    public string StateCode { get; set; }

}