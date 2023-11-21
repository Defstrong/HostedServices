namespace BusinessLogic;

public interface IClientService
{
    Task<bool> CreateAsync(ClientDto client, CancellationToken cancellationToken = default);
    Task<ClientDto?> GetAsync(string clientId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<ClientDto> GetAsync(CancellationToken cancellationToken = default);
    Task<ClientDto?> GetAsync(AuthClientDataDto clientData, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string clientId, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(ClientDto client, CancellationToken cancellationToken = default);
}