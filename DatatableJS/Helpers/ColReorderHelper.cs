using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS.Helpers
{
    /// <summary>
    /// Define table col reorder.
    /// </summary>
    [HtmlTargetElement("col-reorder", ParentTag = "js-datatable", TagStructure = TagStructure.WithoutEndTag)]
    public class ColReorderHelper : TagHelper
    {
        /// <summary>
        /// Initial enablement state of ColReorder. Default value is "true". <see href="https://datatables.net/reference/option/colReorder.enable">Reference:</see> 
        /// </summary>
        public bool Enable { get; set; } = true;

        /// <summary>
        /// Disallow x columns from reordering (counting from the left). Default is "0". <see href="https://datatables.net/reference/option/colReorder.fixedColumnsLeft">Reference:</see>
        /// </summary>
        public int FixedColumnsLeft { get; set; }

        /// <summary>
        /// Disallow x columns from reordering (counting from the right). Default is "0". <see href="https://datatables.net/reference/option/colReorder.fixedColumnsLeft">Reference:</see>
        /// </summary>
        public int FixedColumnsRight { get; set; }

        /// <summary>
        /// Set a default order for the columns in the table. <see href="https://datatables.net/reference/option/colReorder.order">Reference:</see>
        /// </summary>
        public new string Order { get; set; } = "null";

        /// <summary>
        /// Enable / disable live reordering of columns during a drag. Default is "true". <see href="https://datatables.net/reference/option/colReorder.realtime">Reference:</see>
        /// </summary>
        public bool RealTime { get; set; } = true;

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var colReorder = CommonHelpers.GetGrid(context).ColReorder;
            colReorder.Settings = true;
            colReorder.Enable = Enable;
            colReorder.FixedColumnsLeft = FixedColumnsLeft;
            colReorder.FixedColumnsRight = FixedColumnsRight;
            colReorder.Order = Order;
            colReorder.RealTime = RealTime;
        }
    }
}
