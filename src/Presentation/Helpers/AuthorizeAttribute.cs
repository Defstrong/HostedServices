using Microsoft.AspNetCore.Mvc.Filters;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Presentation;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        ClientDto? client = (ClientDto?)context.HttpContext.Items["Client"];

        if(client == null)
            context.Result = new JsonResult(new { message = "Unauthorize" }) { StatusCode = StatusCodes.Status401Unauthorized };
    }
}