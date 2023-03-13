using Marketplace.Application.Contracts;
using Marketplace.Application.Errors;

namespace Marketplace.Application.Services.Validation
{
    public class UserLoginValidator : FlowValidator<UserLoginRequest>
    {
        public UserLoginValidator(UserLoginRequest request) : base(request) 
        {
            IsNull()
                .AddError(AppError.NoDataHasBeenReported.Message)
            .IsNull(request.Email)
                .AddError(AppError.User.EmailNotInformed.Message)
            .IsNull(request.Password)
                .AddError(AppError.User.PasswordNotInformed.Message);
        }
    }
}