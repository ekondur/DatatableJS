using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    /// <summary>
    /// Generic grid builder class.
    /// </summary>
    [HtmlTargetElement("datatable")]
    public class GridHelper : TagHelper
    {
        /// <summary>
        /// Default name is "DataGrid".
        /// </summary>
        public string Name { get; set; } = "DataGrid";

        /// <summary>
        /// Disable or enable ordering, default is true.
        /// </summary>
        public bool Ordering { get; set; } = true;

        /// <summary>
        /// Disable or enable searching, default is true.
        /// </summary>
        public bool Searching { get; set; } = true;

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
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
