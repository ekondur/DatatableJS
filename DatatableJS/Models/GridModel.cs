using System.Collections.Generic;

namespace DatatableJS
{
    public class GridModel
    {
        public string Name { get; set; }
        public bool Ordering { get; set; }
        public bool Searching { get; set; }
        public bool Processing { get; set; }

        public List<ColumnModel> Columns { get; set; }
        public List<FilterModel> Filters { get; set; }
        public List<OrderModel> Orders { get; set; }

        public DataSourceModel DataSource { get; set; }
        public FixedColumnsModel FixedColumns { get; set; }
        public LanguageModel Language { get; set; }
        public CaptionsModel Captions { get; set; }
        public LengthMenuModel LengthMenu { get; set; }

        public GridModel()
        {
            Columns = new List<ColumnModel>();
            Filters = new List<FilterModel>();
            DataSource = new DataSourceModel();
            FixedColumns = new FixedColumnsModel();
            Language = new LanguageModel();
            Captions = new CaptionsModel();
            Orders = new List<OrderModel>();
            LengthMenu = new LengthMenuModel();
        }
    }
}
