namespace MarketCheckout.Application.Response
{
    public class ItemCartResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductResponse? Product { get; set; }
    }
}
