namespace DatatableJS
{
    public class OrderModel
    {
        public string Field { get; set; }
        public int Column { get; set; }
        public Order Order { get; set; }
    }

    public enum Order
    {
        Ascending,
        Descending
    }
}