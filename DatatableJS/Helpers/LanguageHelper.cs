using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    /// <summary>
    /// Specify language json url from cdn or local.
    /// </summary>
    [HtmlTargetElement("language", ParentTag = "js-datatable", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class LanguageHelper : TagHelper
    {
        /// <summary>
        /// Set language json source URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var language = CommonHelpers.GetGrid(context).Language;
            language.URL = URL;
        }
    }
}
