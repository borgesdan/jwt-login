using Marketplace.Application.Contracts;
using Marketplace.Application.Data.Shared;
using Marketplace.Application.Errors;

namespace Marketplace.Application.Services.Validation
{
    public class UserCreateValidator : FlowValidator<UserPostRequest>
    {
        public UserCreateValidator(UserPostRequest request, AccessLevelType accessLevel) : base(request)
        {
            IsNull()
                .AddError(AppError.NoDataHasBeenReported.Message)
            .IsNull()
                .AddError(AppError.User.NameNotInformed.Message)
            .IsNull(request.Email)
                .AddError(AppError.User.EmailNotInformed.Message)
            .IsNull(request.Password)
                .AddError(AppError.User.PasswordNotInformed.Message)
            .Condition(request.AccessLevel, (a) => 
                (AccessLevelReader.IsAdmin(a)) && accessLevel != AccessLevelType.SuperAdmin)
                .AddError(AppError.User.InvalidPermissionToCreateUser.Message);
        }
    }
}