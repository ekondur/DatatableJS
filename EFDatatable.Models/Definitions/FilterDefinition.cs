namespace EFDatatable.Models.Definitions
{
    public class FilterDefinition
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public Operand Operand { get; set; }
    }

    public enum Operand
    {
        Equals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }
}
