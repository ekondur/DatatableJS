namespace DatatableJS
{
    public class OrderModel
    {
        public int Column { get; set; }
        public Order Order { get; set; }
    }

    public enum Order
    {
        Ascending,
        Descending
    }
}