using System.ComponentModel;

namespace DatatableJS.Net
{
    /// <summary>
    /// Set which items are selectable (row, cell, checkbox). Default is row.
    /// </summary>
    public enum SelectItems
    {
        /// <summary>
        /// Enable row selection.
        /// </summary>
        [Description("row")]
        Row,

        /// <summary>
        /// Enable cell selection.
        /// </summary>
        [Description("cell")]
        Cell,

        /// <summary>
        /// Enable checkbox selection.
        /// </summary>
        [Description("row")]
        Checkbox
    }

    /// <summary>
    /// Set select style functionalities.
    /// </summary>
    public enum SelectStyle
    {
        /// <summary>
        /// Select just for single item, to select multiple just click with CTRL button.
        /// </summary>
        [Description("os")]
        Default,

        /// <summary>
        /// Only single item can be selected.
        /// </summary>
        [Description("single")]
        Single,

        /// <summary>
        /// Multiple selectable with one click 
        /// </summary>
        [Description("multi")]
        Multi
    }
}
