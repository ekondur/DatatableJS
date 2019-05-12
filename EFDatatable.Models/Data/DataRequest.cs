using System.Collections.Generic;

namespace EFDatatable.Models.Data
{
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
}
