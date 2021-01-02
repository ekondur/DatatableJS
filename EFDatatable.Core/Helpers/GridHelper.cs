using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EFDatatable.Core
{
    [HtmlTargetElement("datatable")]
    public class GridHelper : TagHelper
    {
        public string Name { get; set; } = "DataGrid";
        public bool Ordering { get; set; } = true;
        public bool Searching { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("id", Name);
            output.Attributes.SetAttribute("style", "width:100%");
            output.Attributes.SetAttribute("class", "display nowrap dataTable dtr-inline collapsed");

            var grid = new GridModel
            {
                Name = Name,
                Ordering = Ordering,
                Searching = Searching,
            };
            
            context.Items.Add("DataGrid", grid);
        }
    }
}
