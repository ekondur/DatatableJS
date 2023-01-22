using System.Collections.Generic;

namespace DatatableJS
{
    /// <summary>
    /// GridModel
    /// </summary>
    public class GridModel
    {
        internal string Name { get; set; }
        internal bool Ordering { get; set; }
        internal bool Searching { get; set; }
        internal bool Processing { get; set; }
        internal bool ScrollX { get; set; }
        internal bool StateSave { get; set; }

        internal List<ColumnModel> Columns { get; set; }
        internal List<FilterModel> Filters { get; set; }
        internal List<OrderModel> Orders { get; set; }

        internal DataSourceModel DataSource { get; set; }
        internal FixedColumnsModel FixedColumns { get; set; }
        internal LanguageModel Language { get; set; }
        internal CaptionsModel Captions { get; set; }
        internal LengthMenuModel LengthMenu { get; set; }
        internal CallbackModel Callback { get; set; }
        internal ColReorderModel ColReorder { get; set; }

        /// <summary>
        /// GridModel()
        /// </summary>
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
            Callback = new CallbackModel();
            ColReorder = new ColReorderModel();
        }
    }
}
