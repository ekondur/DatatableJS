using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EFDatatable.Core
{
    [HtmlTargetElement("captions", ParentTag = "datatable", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CaptionsHelper : TagHelper
    {
        public string Top { get; set; }
        public string Bottom { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var captions = CommonHelpers.GetGrid(context).Captions;
            captions.Top = Top;
            captions.Bottom = Bottom;
        }
    }
}
