using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    [HtmlTargetElement("filters", ParentTag = "datatable")]
    public class FiltersHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
        }
    }

    [HtmlTargetElement("add", ParentTag = "filters")]
    public class FilterHelper : TagHelper
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public Operand Operand { get; set; }

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
