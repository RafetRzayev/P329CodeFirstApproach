namespace P329CodeFirstApproach.ViewModels
{
    public class BasketViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; } = 1;
    }
}
