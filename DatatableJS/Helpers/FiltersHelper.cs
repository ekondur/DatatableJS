using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    /// <summary>
    /// Filter data with request.
    /// </summary>
    [HtmlTargetElement("filters", ParentTag = "datatable")]
    public class FiltersHelper : TagHelper
    {
        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
        }
    }

    /// <summary>
    /// Add a filter to request.
    /// </summary>
    [HtmlTargetElement("add", ParentTag = "filters")]
    public class FilterHelper : TagHelper
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public Operand Operand { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var grid = CommonHelpers.GetGrid(context);
            grid.Filters.Add(new FilterModel
            {
                Field = Field,
                Value = Value,
                Operand = Operand
            });
        }
    }
}
