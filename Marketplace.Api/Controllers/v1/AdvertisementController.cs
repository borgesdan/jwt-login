using Marketplace.Api.Attributes;
using Marketplace.Application.Contracts;
using Marketplace.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Application.Data.Shared;

namespace Marketplace.Api.Controllers.v1
{
    [BearerAuthorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdvertisementController : DefaultController
    {
        private readonly AdvertisementService _advertisementService;

        public AdvertisementController(AdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdvertisementCreateRequest request)
            => ConvertData(await _advertisementService.Create(request, CurrentUserAccessLevel ?? AccessLevelType.Anonymous));
    }
}
