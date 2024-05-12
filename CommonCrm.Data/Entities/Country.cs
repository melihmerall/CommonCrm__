namespace CommonCrm.Data.Entities;

public class Country: BaseEntity
{
    public string Name { get; set; }
    public string Iso2 { get; set; }
    public string Iso3 { get; set; }
}