using System.Runtime.CompilerServices;
using Businesslogic;
using DataAccess;

namespace BusinessLogic;

public sealed class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        ArgumentNullException.ThrowIfNull(clientRepository);
        _clientRepository = clientRepository;
    }

    public Task<bool> CreateAsync(ClientDto client, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);
        return _clientRepository.CreateAsync(client.DtoClientToDb(), cancellationToken);
    }

    public async Task<ClientDto?> GetAsync(AuthClientDataDto clientData, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(clientData);
        DbClient? client = await _clientRepository.GetAsync(clientData.DtoClientDataToDb(), cancellationToken);
        return client?.DbClientToDto();
    }

    public Task<bool> DeleteAsync(string clientId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientId);
        return _clientRepository.DeleteAsync(clientId, cancellationToken);
    }

    public async Task<ClientDto?> GetAsync(string clientId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientId);
        DbClient? client = await _clientRepository.GetAsync(clientId, cancellationToken);
        return client?.DbClientToDto();
    }

    public async IAsyncEnumerable<ClientDto> GetAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach(DbClient client in _clientRepository.GetAsync(cancellationToken))
            yield return client.DbClientToDto();
    }

    public Task<bool> UpdateAsync(ClientDto client, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);
        return _clientRepository.UpdateAsync(client.DtoClientToDb(), cancellationToken);
    }
}