using Microsoft.AspNetCore.Authorization;

namespace Marketplace.Api.Attributes
{
    public class BearerAuthorizeAttribute : AuthorizeAttribute
    {
        public BearerAuthorizeAttribute() 
        {
            base.AuthenticationSchemes = "Bearer";
        }
    }
}