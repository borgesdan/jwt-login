using Marketplace.Application.Data.Shared;
using Marketplace.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Marketplace.Api.Controllers.v1
{
    public class DefaultController : ControllerBase
    {
        public Guid? CurrentUserUid
        {
            get 
            {
                var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                return claim != null ? Guid.Parse(claim.Value) : null;
            } 
        }

        public AccessLevelType? CurrentUserAccessLevel
        {
            get
            {
                var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                if (claim == null)
                    return null;

                var value = int.Parse(claim.Value);

                return (AccessLevelType)value;
            }
        }

        protected IActionResult ConvertData(IResultData resultData)
        {
            if(resultData == null)
                throw new ArgumentNullException(nameof(resultData));

            var result = new ObjectResult(resultData)
            {
                StatusCode = (int)resultData.StatusCode
            };

            return result;
        }
    }
}