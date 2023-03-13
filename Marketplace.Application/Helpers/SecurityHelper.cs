using System.Security.Cryptography;
using System.Text;

namespace Marketplace.Application.Helpers
{
    public class SecurityHelper
    {
        public static string CreateSHA1Hash(string key)
        {
            var sourceBytes = Encoding.UTF8.GetBytes(key);
            var hashBytes = SHA1.HashData(sourceBytes);
            var hash = BitConverter
                .ToString(hashBytes)
                .ToLower()
                .Replace("-", string.Empty);

            var guid1 = "94360715-8A4C-490E-B1E2-B0463E79CE88".ToLower().Replace("-", string.Empty);
            var guid2 = "061CEC05-0FD9-4D71-B736-9349FE14EE03".ToLower().Replace("-", string.Empty);

            var fullHash = guid1 + hash + guid2;

            return fullHash;
        }
    }
}