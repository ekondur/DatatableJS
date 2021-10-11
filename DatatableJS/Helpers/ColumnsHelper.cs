using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    [HtmlTargetElement("columns", ParentTag = "datatable")]
    public class ColumnsHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "thead";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.PreContent.SetHtmlContent("<tr>");
            output.PostContent.SetHtmlContent("</tr>");
        }
    }

    [HtmlTargetElement("column", ParentTag = "columns")]
    public class ColumnHelper : TagHelper
    {
        public string Field { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; } = true;
        public bool Searchable { get; set; } = true;
        public bool Orderable { get; set; } = true;
        public int Width { get; set; }
        public string ClassName { get; set; } = "";
        public string DefaultContent { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "th";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.Append(Title ?? Field);

            var grid = CommonHelpers.GetGrid(context);
            grid.Columns.Add(new ColumnModel
            {
                Data = Field,
                Visible = Visible,
                Searchable = Searchable,
                Orderable = Orderable,
                Width = Width,
                ClassName = ClassName,
                DefaultContent = DefaultContent
            });
        }
    }
}
