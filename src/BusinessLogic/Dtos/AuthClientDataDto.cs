namespace BusinessLogic;

public sealed record AuthClientDataDto
{
    private readonly string? _name;
    private readonly string? _password;

    public string Name
    {
        get => _name ?? string.Empty;
        init => _name = value is { Length: > 0 }
            ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }

    public string Password
    {
        get => _password ?? string.Empty;
        init => _password = value is { Length: > 0 }
            ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }
}