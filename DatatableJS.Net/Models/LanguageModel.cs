namespace DatatableJS.Net
{
    internal class LanguageModel
    {
        internal string URL { get; set; }
        internal string Decimal { get; set; }
        internal string EmptyTable { get; set; }
        internal string Info { get; set; }
        internal string InfoEmpty { get; set; }
        internal string InfoFiltered { get; set; }
        internal string InfoPostFix { get; set; }
        internal string Thousands { get; set; }
        internal string LengthMenu { get; set; }
        internal string LoadingRecords { get; set; }
        internal string Processing { get; set; }
        internal string Search { get; set; }
        internal string ZeroRecords { get; set; }
        internal PaginateModel Paginate { get; set; } = new PaginateModel();
        internal AriaModel Aria { get; set; } = new AriaModel();
    }

    internal class PaginateModel
    {
        public string First { get; set; }
        public string Last { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
    }

    internal class AriaModel
    {
        public string SortAscending { get; set; }
        public string SortDescending { get; set; }
    }
}
