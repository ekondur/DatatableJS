namespace DatatableJS.Net
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OrderDefinition
    {
        public string Field { get; set; }
        public int Column { get; set; }
        public OrderBy OrderBy { get; set; }
    }

    public enum OrderBy
    {
        Ascending,
        Descending
    }
}
