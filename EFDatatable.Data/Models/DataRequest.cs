using System.Collections.Generic;

namespace EFDatatable.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DataRequest
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public List<Order> order { get; set; }
        public Search search { get; set; }
        public List<Filter> filters { get; set; }

        public DataRequest()
        {
            filters = new List<Filter>();
            order = new List<Order>();
            columns = new List<Column>();
        }
    }

    public class Filter
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public int Operand { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }
}
