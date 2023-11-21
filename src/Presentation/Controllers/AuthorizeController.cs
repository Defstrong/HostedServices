using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using BusinessLogic;
using System.Runtime.CompilerServices;
namespace Presentation;


[Route("Login")]
public sealed class AuthorizationController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly JwtConfig _jwtConfig;
    private readonly IConfiguration _configuration;

    public AuthorizationController(IClientService clientService, 
        IOptionsMonitor<JwtConfig> jwtConfig, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(clientService);
        _clientService = clientService;
        _jwtConfig = jwtConfig.CurrentValue;
        _configuration = configuration;
    }

    [HttpPost("reg")]
    public async Task<IActionResult> Register([FromBody] ClientDto client, CancellationToken cancellationToken = default)
    {
        bool createResult = await _clientService.CreateAsync(client, cancellationToken);

        if(!createResult)
            return BadRequest( new { message = "Didn't register! "});
        
        return Ok(client);
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] AuthClientDataDto authClientDataDto, CancellationToken cancellationToken = default)
    {
        ClientDto? client = await _clientService.GetAsync(authClientDataDto, cancellationToken);
        if(client is null)
            return NotFound("Client name or password is incorrect");
        
        string token = _configuration.GenerateJwtToken(client);

        return Ok(new { client, token });
    }
    
    [Authorize]
    [HttpGet("hello")]
    public async IAsyncEnumerable<ClientDto> GetAll([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach(ClientDto client in _clientService.GetAsync(cancellationToken))
            yield return client;
    }
}