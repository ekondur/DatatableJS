using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EFDatatable.Core
{
    [HtmlTargetElement("language", ParentTag = "datatable", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class LanguageHelper : TagHelper
    {
        public string URL { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var language = CommonHelpers.GetGrid(context).Language;
            language.URL = URL;
        }
    }
}
