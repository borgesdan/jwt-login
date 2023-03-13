namespace Marketplace.Application.Contracts
{
    public class UserTokenResponse
    {
        public DateTime Expiration { get; set; }
        public string? Token { get; set; }
    }
}
