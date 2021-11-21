using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    /// <summary>
    /// Set datasource properties
    /// </summary>
    [HtmlTargetElement("data-source", ParentTag = "js-datatable", TagStructure = TagStructure.NormalOrSelfClosing)]
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
        /// Proccessing data server side or client side, default is false.
        /// </summary>
        public bool ServerSide { get; set; } = true;

        /// <summary>
        /// Passing additional data to action set name of javascript function.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var dataSource = CommonHelpers.GetGrid(context).DataSource;
            dataSource.URL = URL;
            dataSource.Method = Method;
            dataSource.Paging = Paging;
            dataSource.Data = Data;
            dataSource.ServerSide = ServerSide;
        }
    }
}
