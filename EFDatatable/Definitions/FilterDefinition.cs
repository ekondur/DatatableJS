namespace EFDatatable
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class FilterDefinition
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
