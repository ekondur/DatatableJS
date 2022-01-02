using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    /// <summary>
    /// Define table top or bottom captions.
    /// </summary>
    [HtmlTargetElement("captions", ParentTag = "js-datatable", TagStructure = TagStructure.WithoutEndTag)]
    public class CaptionsHelper : TagHelper
    {
        /// <summary>
        /// Define table top caption.
        /// </summary>
        public string Top { get; set; }

        /// <summary>
        /// Define table bottom caption.
        /// </summary>
        public string Bottom { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var captions = CommonHelpers.GetGrid(context).Captions;
            captions.Top = Top;
            captions.Bottom = Bottom;
        }
    }
}
