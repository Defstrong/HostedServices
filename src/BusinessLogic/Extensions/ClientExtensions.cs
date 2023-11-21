using BusinessLogic;
using DataAccess;

namespace Businesslogic;

public static class ClientExtensions
{
    public static DbClient DtoClientToDb(this ClientDto client)
        => new()
            {
                Id = client.Id,
                Name = client.Name,
                Password = client.Password
            };

    public static ClientDto DbClientToDto(this DbClient client)
        => new()
            {
                Id = client.Id,
                Name = client.Name,
                Password = client.Password
            };
}