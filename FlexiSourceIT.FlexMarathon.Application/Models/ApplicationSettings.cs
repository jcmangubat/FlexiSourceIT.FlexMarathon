using System.Text.Json.Serialization;
using static SMEAppHouse.Ladon.Domain.Constants.Rules;

namespace SMEAppHouse.Ladon.Application.Models;

public class ApplicationSettings
{
    /// <summary>
    /// The path into which the attachment files .e.g images will be 
    /// copied into in the ImageKit.Io DAM site.
    /// </summary>
    public string? QRFPath { get; set; }
    public required IEnumerable<EmailContact> ContactUsRecipients { get; set; } = [];

    [JsonPropertyName("ContactUsMessageForwardingInterval")]
    public TimeSpan? MessageForwardingInterval { get; set; }

    [JsonPropertyName("SiteSectionOptions")]
    public required Dictionary<SiteSectionsEnum, bool> SiteSectionOptions { get; set; }

    public required FactPoints FactPoints { get; set; }
}
