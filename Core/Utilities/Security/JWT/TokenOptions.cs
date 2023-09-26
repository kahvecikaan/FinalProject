namespace Core.Utilities.Security.JWT;

public class TokenOptions
{
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public int AccessTokenExpiration { get; set; }
    public string SecurityKey { get; set; } = null!;
}