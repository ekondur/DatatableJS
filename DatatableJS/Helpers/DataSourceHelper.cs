using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;

namespace DatatableJS
{
    /// <summary>
    /// Set datasource properties
    /// </summary>
    [HtmlTargetElement("data-source", ParentTag = "js-datatable", TagStructure = TagStructure.WithoutEndTag)]
    public class DataSourceHelper : TagHelper
    {
        /// <summary>
        /// URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Set the action type, default type is GET.
        /// </summary>
        public HttpMethodType Method { get; set; } = HttpMethodType.GET;

        /// <summary>
        /// Disable or enable paging, default is true.
        /// </summary>
        public bool Paging { get; set; } = true;

        /// <summary>
        /// Enable server-side processing mode.
        /// </summary>
        public bool ServerSide { get; set; }

        /// <summary>
        /// Passing additional data to action set name of javascript function.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Define table page length.
        /// </summary>
        public int PageLength { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var grid = CommonHelpers.GetGrid(context);
            grid.DataSource.URL = URL;
            grid.DataSource.Method = Method;
            grid.DataSource.Paging = Paging;
            grid.DataSource.Data = Data;
            grid.DataSource.ServerSide = ServerSide;
            grid.DataSource.PageLength = PageLength;

            if (!grid.LengthMenu._lengthMenuValues.Any())
            {
                grid.LengthMenu._lengthMenuValues = new List<int> { PageLength, PageLength * 2, PageLength * 3, PageLength * 4, PageLength * 5 };
                grid.LengthMenu._lengthMenuDisplayedTexts = grid.LengthMenu._lengthMenuValues.Select(x => x.ToString()).ToList();
            }
        }
    }
}
