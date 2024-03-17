namespace CommonCrm.Data.Entities.CompanyRelations;

public class BankAccounts: BaseEntity
{
    public string? BankName { get; set; }
    public string? Branch { get; set; }
    public string? SwiftCode { get; set; }
    public string? AccountOwner { get; set; }
    public string? Doviz { get; set; }
    public string? HesapNo { get; set; }
    public string? IbanNo { get; set; }
    public string? Description { get; set; }

}