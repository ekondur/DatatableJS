using System.ComponentModel;

namespace DatatableJS.Net
{
    /// <summary>
    /// Set which items are selectable (row, column, cell, row with checkbox). Default is row.
    /// </summary>
    public enum SelectItems
    {
        /// <summary>
        /// Enable row selection.
        /// </summary>
        [Description("row")]
        Row,

        /// <summary>
        /// Enable column selection.
        /// </summary>
        [Description("column")]
        Column,

        /// <summary>
        /// Enable cell selection.
        /// </summary>
        [Description("cell")]
        Cell,

        /// <summary>
        /// Enable row selection with checkbox.
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
        Multi,

        /// <summary>
        /// Multi-item with rangle selection 
        /// </summary>
        [Description("multi+shift")]
        MultiShift
    }

    /// <summary>
    /// Select position of the column search box
    /// </summary>
    public enum SearchPosition
    {
        /// <summary>
        /// Footer
        /// </summary>
        Footer,

        /// <summary>
        /// Header
        /// </summary>
        Header
    }
}
