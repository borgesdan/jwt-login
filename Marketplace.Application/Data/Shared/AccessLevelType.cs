namespace Marketplace.Application.Data.Shared
{
    public enum AccessLevelType
    {
        Anonymous,
        Buyer,
        Seller,                
        Admin,
        SuperAdmin
    }

    public struct AccessLevelReader
    {
        public static bool IsSeller(AccessLevelType accessLevelType)
            => accessLevelType == AccessLevelType.Seller
            || IsAdmin(accessLevelType);

        public static bool IsAdmin(AccessLevelType accessLevelType)
            => accessLevelType == AccessLevelType.Admin
            || accessLevelType == AccessLevelType.SuperAdmin;
    }
}