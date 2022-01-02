namespace DatatableJS.Net
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OrderDefinition
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
