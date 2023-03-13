using Marketplace.Application.Contracts;
using Marketplace.Application.Errors;

namespace Marketplace.Application.Services.Validation
{
    public class AdvertisementCreateValidator : FlowValidator<AdvertisementCreateRequest>
    {
        public AdvertisementCreateValidator(AdvertisementCreateRequest request)
            :base(request) 
        {
            IsNull()
                .AddError(AppError.NoDataHasBeenReported.Message)
            .IsNull(request.Title)
                .AddError(AppError.Advertisement.TitleNotInformed.Message)
            .IsNull(request.Description)
                .AddError(AppError.Advertisement.DescriptionNotInformed.Message)            
            .IsNegative((int)request.Discount)
                .AddError(AppError.Advertisement.DiscountCannotBeNegative.Message);
        }
    }
}
