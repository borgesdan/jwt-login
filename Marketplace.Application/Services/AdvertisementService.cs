using Marketplace.Application.Contracts;
using Marketplace.Application.Data.Context;
using Marketplace.Application.Data.Entities;
using Marketplace.Application.Data.Shared;
using Marketplace.Application.Errors;
using Marketplace.Application.Services.Validation;

namespace Marketplace.Application.Services
{
    public class AdvertisementService
    {
        private readonly AppDbContext _context;

        public AdvertisementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IResultData> Create(AdvertisementCreateRequest request, AccessLevelType accessLevel)
        {
            if (AccessLevelReader.IsSeller(accessLevel))
                return ResultData.Error(AppError.InvalidAccessLevel.Message);

            var validator = new AdvertisementCreateValidator(request);

            if (!validator.Validate())
                return ResultData.Error(validator.Errors.First());

            var advertisement = new AdvertisementEntity
            {
                Description = request.Description,
                Discount = request.Discount,
                SellerId = request.SellerId,
                Price = request.Price,
                Title = request.Title,
            };

            _context.Advertisements.Add(advertisement);
            await _context.SaveChangesAsync();

            return ResultData.Ok();
        }
    }
}