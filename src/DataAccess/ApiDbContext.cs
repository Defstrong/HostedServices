using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public sealed class ApiDbContext : DbContext
{
    public DbSet<DbClient> Clients => Set<DbClient>();

    public ApiDbContext() {}
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) {}
}