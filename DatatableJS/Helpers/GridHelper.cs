using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    /// <summary>
    /// Generic grid builder class.
    /// </summary>
    [HtmlTargetElement("js-datatable")]
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
        /// Enable or disable the display of a 'processing' indicator when the table is being processed, Default is true.
        /// </summary>
        public bool Processing { get; set; } = true;

        /// <summary>
        /// Enable horizontal scrolling. When a table is too wide to fit into a certain layout, or you have a large number of columns in the table, you can enable horizontal (x) scrolling to show the table in a viewport, which can be scrolled.
        /// </summary>
        public bool ScrollX { get; set; }

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
                Processing = Processing,
                ScrollX = ScrollX
            };
            
            context.Items.Add("DataGrid", grid);
        }
    }
}
