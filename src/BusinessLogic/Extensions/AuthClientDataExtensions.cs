using BusinessLogic;
using DataAccess;

namespace Businesslogic;

public static class AuthClientDataExtensions
{
    public static DbAuthClientData DtoClientDataToDb(this AuthClientDataDto clientData)
        => new()
            {
                Name = clientData.Name,
                Password = clientData.Password
            };

    public static AuthClientDataDto DtoClientDataToDb(this DbAuthClientData clientData)
        => new()
            {
                Name = clientData.Name,
                Password = clientData.Password
            };
}