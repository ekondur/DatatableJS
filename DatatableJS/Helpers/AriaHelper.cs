using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS.Helpers
{
    /// <summary>
    /// Language strings used for WAI-ARIA specific attributes.
    /// </summary>
    [HtmlTargetElement("aria", ParentTag = "language", TagStructure = TagStructure.WithoutEndTag)]
    public class AriaHelper : TagHelper
    {
        /// <summary>
        /// Default is ': activate to sort column ascending'.
        /// </summary>
        public string SortAscending { get; set; }

        /// <summary>
        /// Default is ': activate to sort column descending'.
        /// </summary>
        public string SortDescending { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var aria = CommonHelpers.GetGrid(context).Language.Aria;
            aria.SortAscending = SortAscending;
            aria.SortDescending = SortDescending;
        }
    }
}
