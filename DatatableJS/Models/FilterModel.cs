namespace DatatableJS
{
    public class FilterModel
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public Operand Operand { get; set; }
    }

    public enum Operand
    {
        Equal,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }
}
