namespace SMEAppHouse.Ladon.Application.Models;

public class ImageKitCredentials
{
    public required string Id { get; set; }
    public required string StandardPublicKey { get; set; }
    public required string StandardPrivateKey { get; set; }
    public required string RestrictedPublicKey { get; set; }
    public required string RestrictedPrivateKey { get; set; }
    public required string UrlEndpoint { get; set; }
}