namespace DataAccess;

public interface IClientRepository
{
    Task<bool> CreateAsync(DbClient client, CancellationToken cancellationToken);
    Task<DbClient?> GetAsync(string clientId, CancellationToken cancellationToken);
    IAsyncEnumerable<DbClient> GetAsync(CancellationToken cancellationToken);
    Task<DbClient?> GetAsync(DbAuthClientData clientData, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(string clientId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(DbClient client, CancellationToken cancellationToken);
}