using Newtonsoft.Json;

namespace SMEAppHouse.Ladon.Application.Models;

public class ServiceItem
{
    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("description")]
    public required string Description { get; set; }

    [JsonProperty("imageCDNUrl")]
    public required string ImageCDNUrl { get; set; }

    [JsonProperty("class")]
    public PresenterClassesEnum? PresenterClass { get; set; }

    [JsonProperty("show")]
    public bool Show { get; set; }

    public enum PresenterClassesEnum
    {
        Tall,
        Big,
        Wide
    }
}
