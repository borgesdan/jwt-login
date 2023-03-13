using Marketplace.Application.Data.Shared;

namespace Marketplace.Application.Contracts
{
    public class UserTokenRequest
    {
        public Guid UserUid { get; set; }
        public string? Email { get; set; }
        public AccessLevelType AccessLevel { get; set; }
    }
}