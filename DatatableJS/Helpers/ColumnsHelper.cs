using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    /// <summary>
    /// Columns builder.
    /// </summary>
    [HtmlTargetElement("columns", ParentTag = "js-datatable")]
    public class ColumnsHelper : TagHelper
    {
        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "thead";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.PreContent.SetHtmlContent("<tr>");
            output.PostContent.SetHtmlContent("</tr>");
        }
    }

    /// <summary>
    /// Column builder.
    /// </summary>
    [HtmlTargetElement("add", ParentTag = "columns", TagStructure = TagStructure.WithoutEndTag)]
    public class ColumnHelper : TagHelper
    {
        /// <summary>
        /// Make a column with defined type properties.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Set column title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Set column visible or hidden, default is true.
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Set column orderable or not, default is true.
        /// </summary>
        public bool Orderable { get; set; } = true;

        /// <summary>
        /// Set column searchable or not, default is true.
        /// </summary>
        public bool Searchable { get; set; } = true;

        /// <summary>
        /// Set column width percentage.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Set css class of column.
        /// </summary>
        public string ClassName { get; set; } = "";

        /// <summary>
        /// Set default value for null data
        /// </summary>
        public string DefaultContent { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
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
