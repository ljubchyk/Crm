﻿namespace Crm.Domain;

public record Address : ValueObject
{
    private readonly string country = string.Empty;
    private readonly string city = string.Empty;
    private readonly string postalCode = string.Empty;
    private readonly string line1 = string.Empty;
    private readonly string line2;

    private Address()
    {

    }

    public Address(string country, string city, string postalCode, string line1, string line2 = null)
    {
        Assert.NotEmpty(country, nameof(country));
        Assert.NotEmpty(city, nameof(city));
        Assert.NotEmpty(postalCode, nameof(postalCode));
        Assert.NotEmpty(line1, nameof(line1));

        if (line2 is not null)
        {
            Assert.NotEmpty(line2, nameof(line2));
        }

        this.country = country;
        this.city = city;
        this.postalCode = postalCode;
        this.line1 = line1;
        this.line2 = line2;
    }

    public string Country => country;
    public string City => city; 
    public string PostalCode => postalCode;
    public string Line1 => line1;
    public string Line2 => line2;
}
