using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EFDatatable.Core
{
    [HtmlTargetElement("data-source", ParentTag = "datatable", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class DataSourceHelper : TagHelper
    {
        public string URL { get; set; }
        public HttpMethodType Method { get; set; }
        public bool Paging { get; set; } = true;
        public bool ServerSide { get; set; } = true;
        public string Data { get; set; }

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
