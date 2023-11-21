using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public sealed class ClientRepository : IClientRepository
{
    private readonly ApiDbContext _context;
    public ClientRepository(ApiDbContext context)
        => _context = context;
    
    public async Task<bool> CreateAsync(DbClient client, CancellationToken cancellationToken)
    {
        await _context.Clients.AddAsync(client, cancellationToken);
        int createResult = await _context.SaveChangesAsync();
        return createResult > 0;
    }

    public async Task<bool> DeleteAsync(string clientId, CancellationToken cancellationToken = default)
    {
        DbClient? client = await _context.Clients
            .FirstOrDefaultAsync(client => client.Id.Equals(clientId), cancellationToken);
        
        if(client is null)
            return false;
            
        _context.Clients.Remove(client);

        int deleteResult = await _context.SaveChangesAsync();
        return deleteResult > 0;
    }

    public async Task<DbClient?> GetAsync(string clientId, CancellationToken cancellationToken = default)
    {
        DbClient? client = await _context.Clients
            .SingleOrDefaultAsync(client => client.Id.Equals(clientId), cancellationToken);
        return client;       
    }

    public async Task<DbClient?> GetAsync(DbAuthClientData clientData, CancellationToken cancellationToken = default)
    {
        DbClient? definitionClient = await _context.Clients
            .FirstOrDefaultAsync(client => client.Password == clientData.Password && client.Name == clientData.Name);
        return definitionClient;
    }

    public async IAsyncEnumerable<DbClient> GetAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        IEnumerable<DbClient> clients = await _context.Clients.ToListAsync(cancellationToken);
        foreach(DbClient client in clients)
            yield return client;
    }

    public async Task<bool> UpdateAsync(DbClient client, CancellationToken cancellationToken = default)
    {
        _context.Entry(client).State = EntityState.Modified;
        int updateResult = await _context.SaveChangesAsync();
        return updateResult > 0;
    }
}