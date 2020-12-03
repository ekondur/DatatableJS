namespace EFDatatable.Data
{
    public class FilterDef
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public Operand Operand { get; set; }
        public Operator Operator { get; set; }
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

    public enum Operator
    {
        And,
        Or
    }
}
