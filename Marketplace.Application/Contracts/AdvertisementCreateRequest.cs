namespace Marketplace.Application.Contracts
{
    public class AdvertisementCreateRequest
    {
        public int SellerId { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public double Discount { get; set; }
        public string? Description { get; set; }
    }
}
