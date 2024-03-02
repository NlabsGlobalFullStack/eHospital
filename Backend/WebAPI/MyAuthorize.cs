using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI;

public class MyAuthorize : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        throw new NotImplementedException();
    }
}
