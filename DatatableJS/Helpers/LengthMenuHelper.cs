using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace DatatableJS.Helpers
{
    /// <summary>
    /// Define table length menu.
    /// </summary>
    [HtmlTargetElement("length-menu", ParentTag = "js-datatable", TagStructure = TagStructure.WithoutEndTag)]
    public class LengthMenuHelper : TagHelper
    {
        /// <summary>
        /// Set the values of LengthMenu
        /// </summary>
        public int[] Values { get; set; }

        /// <summary>
        /// Add All option on LengthMenu
        /// </summary>
        public bool HasAll { get; set; } = false;

        /// <summary>
        /// Set a text for LengtMenu All option, default is "All"
        /// </summary>
        public string AllText { get; set; } = "All";

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var lengthMenu = CommonHelpers.GetGrid(context).LengthMenu;

            lengthMenu._lengthMenuValues = Values.ToList();
            lengthMenu._lengthMenuDisplayedTexts = Values.Select(x => x.ToString()).ToList();

            if (!lengthMenu._pageLength.HasValue)
                lengthMenu._pageLength = lengthMenu._lengthMenuValues.FirstOrDefault();

            if (HasAll)
            {
                lengthMenu._lengthMenuValues.Add(-1);
                lengthMenu._lengthMenuDisplayedTexts.Add(AllText);
            }
        }
    }
}
