using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    [HtmlTargetElement("fixed-columns", ParentTag = "datatable", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class FixedColumnsHelper : TagHelper
    {
        public int LeftColumns { get; set; }
        public int RightColumns { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var fixedColumns = CommonHelpers.GetGrid(context).FixedColumns;
            fixedColumns.LeftColumns = LeftColumns;
            fixedColumns.RightColumns = RightColumns;
        }
    }
}
