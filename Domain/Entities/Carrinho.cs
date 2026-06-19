namespace MarketCheckoutApi.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        public string NomeComprador { get; set; }
        public string CpfComprador { get; set; }
        public string CriadoPor { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
