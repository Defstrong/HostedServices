namespace BusinessLogic;

public sealed class JwtConfig
{
    private readonly string? _issuer;
    private readonly string? _key;
    private readonly string? _audience;

    public string Issuer
    {
        get => _issuer ?? string.Empty;
        init => _issuer = value is { Length: > 0 }
            ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }

    public string Key
    {
        get => _key ?? string.Empty;
        init => _key = value is { Length: > 0 }
            ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }

    public string Audience
    {
        get => _audience ?? string.Empty;
        init => _audience = value is { Length: > 0 }
            ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }
}