using SMEAppHouse.Core.CodeKits.Extensions;

namespace SMEAppHouse.Ladon.Application.Models;

public class DropdownItem
{
    public required string Value { get; set; }
    public required string Description { get; set; }

    public static IEnumerable<DropdownItem> Get<T>() where T : struct, Enum
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentException("T must be an enum type.");

        var dropdownItems = new List<DropdownItem>();
        var enumValues = Enum.GetValues(typeof(T)).Cast<T>()
                                .Select(e => new DropdownItem
                                {
                                    Value = e.ToString(),
                                    Description = e.GetDescription() // Or use the Description attribute if available
                                })
                                .ToList();

        dropdownItems.AddRange(enumValues
                                    .Where(p => p.Description.NotEqualsCaseInsensitive("other") &&
                                                p.Description.NotEqualsCaseInsensitive("others"))
                                    .OrderBy(p => p.Description));
        var otherDropdownItem = enumValues
                                    .FirstOrDefault(p => p.Description.EqualsCaseInsensitive("other") ||
                                                            p.Description.EqualsCaseInsensitive("others"));
        if (otherDropdownItem != null)
            dropdownItems.Add(otherDropdownItem);

        return dropdownItems;
    }
}