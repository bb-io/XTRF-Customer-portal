namespace Apps.XtrfCustomerPortal.Models.Dtos;

public class FullOfficeDto
{
    public string Name { get; set; } = string.Empty;
    public long Id { get; set; }
    public List<PriceProfileDto> PriceProfiles { get; set; } = new();
    public List<PersonDto> Persons { get; set; } = new();
}

public class PriceProfileDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public object DefaultContactPerson { get; set; }
}

public class PersonDto
{
    public long Id { get; set; }
    public int Version { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool UsePartnerAddress { get; set; }
    public AddressDto Address { get; set; } = new();
    public ContactDto Contact { get; set; } = new();
}

public class AddressDto
{
    public CountryDto Country { get; set; } = new();
    public object Province { get; set; }
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Address2 { get; set; } = string.Empty;
} 

public class CountryDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LocalizedName { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
}

public class ContactDto
{
    public List<string> Phones { get; set; } = new();
    public string Mobile { get; set; } = string.Empty;
    public bool SmsEnabled { get; set; }
    public string Fax { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Www { get; set; } = string.Empty;
    public List<object> SocialMediaContacts { get; set; } = new();
}
