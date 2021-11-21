using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    /// <summary>
    /// Fix the table columns from left or right.
    /// </summary>
    [HtmlTargetElement("fixed-columns", ParentTag = "js-datatable", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class FixedColumnsHelper : TagHelper
    {
        public int LeftColumns { get; set; }
        public int RightColumns { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var fixedColumns = CommonHelpers.GetGrid(context).FixedColumns;
            fixedColumns.LeftColumns = LeftColumns;
            fixedColumns.RightColumns = RightColumns;
        }
    }
}
