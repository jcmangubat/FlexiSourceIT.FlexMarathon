using static SMEAppHouse.Ladon.Domain.Constants.Rules;

namespace SMEAppHouse.Ladon.Application.Models;

public class EmailContact
{
    public required string EmailAddress { get; set; }
    public string? Name { get; set; }

    public RecipientTypesEnum? RecipientType { get; set; }

    public override string ToString() => $"{Name} <{EmailAddress}>".Trim();
}
