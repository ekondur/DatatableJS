using System.Collections.Generic;

namespace EFDatatable.Models.Data
{
    public class DataResult<T>
    {
        public int draw { get; set; }
        public long recordsTotal { get; set; }
        public long recordsFiltered { get; set; }
        public List<T> data { get; set; }
    }
}
