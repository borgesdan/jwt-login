using Marketplace.Application.Data.Shared;

namespace Marketplace.Application.Contracts
{
    public class UserPostRequest
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public AccessLevelType AccessLevel { get; set; }
    }
}
