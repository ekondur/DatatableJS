using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS.Helpers
{
    /// <summary>
    /// Pagination specific language strings.
    /// </summary>
    [HtmlTargetElement("paginate", ParentTag = "language", TagStructure = TagStructure.WithoutEndTag)]
    public class PaginateHelper : TagHelper
    {
        /// <summary>
        /// Pagination 'First' button string.
        /// </summary>
        public string First { get; set; }

        /// <summary>
        /// Pagination 'Last' button string.
        /// </summary>
        public string Last { get; set; }

        /// <summary>
        /// Pagination 'Next' button string.
        /// </summary>
        public string Next { get; set; }

        /// <summary>
        /// Pagination 'Previous' button string.
        /// </summary>
        public string Previous { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var paginate = CommonHelpers.GetGrid(context).Language.Paginate;
            paginate.First = First;
            paginate.Last = Last;
            paginate.Next = Next;
            paginate.Previous = Previous;
        }
    }
}
