using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System.Linq;

namespace EFDatatable.Core
{
    [HtmlTargetElement("render", ParentTag = "datatable", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class RenderHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "script";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.Clear();
            var grid = CommonHelpers.GetGrid(context);

            var script = $@"
            $(document).ready(function () {{
                $('#{grid.Name}').DataTable( {{
                    processing:true,
                    serverSide:{grid.DataSource.ServerSide.ToLowString()},
                    fixedColumns: {{ 
                        leftColumns: {grid.FixedColumns?.LeftColumns},
                        rightColumns: {grid.FixedColumns?.RightColumns}
                    }},
                    order:[],
                    ordering: {grid.Ordering.ToLowString()},
                    searching: {grid.Searching.ToLowString()},
                    paging: {grid.DataSource.Paging.ToLowString()},
                    language: {{
                        'url': '{grid.Language.URL}'
                    }},
                    ajax: {{
                            url: ""{grid.DataSource?.URL}"",
                            type: ""{grid.DataSource?.Method}"",
                            data: {GetDataStr(grid)}
                          }},
                    columns: [{string.Join(", ", grid.Columns.Select(a => $@"{{ 
                                'data': '{a.Data}',
                                'orderable': {a.Orderable.ToLowString()},
                                'searchable': {a.Searchable.ToLowString()},
                                'className': '{a.ClassName}',
                                'visible': {a.Visible.ToLowString()},
                                'width': '{a.Width}%',
                                    {(string.IsNullOrEmpty(a.Render) ? string.Empty : $"'render': function(data, type, row, meta) {{ if (data == null) {{ return ''; }} else {{ return {a.Render}; }} }}")}
                            }}"))}]
                }});
            }});
                {(string.IsNullOrEmpty(grid.Captions.Top) ? string.Empty : string.Format("$('#{0}').append('<caption style=\"caption-side:top\">{1}</caption>');", grid.Name, grid.Captions.Top))}
                {(string.IsNullOrEmpty(grid.Captions.Bottom) ? string.Empty : string.Format("$('#{0}').append('<caption style=\"caption-side:bottom\">{1}</caption>');", grid.Name, grid.Captions.Bottom))}";

            output.Content.AppendHtml(script);
        }

        private string GetDataStr(GridModel grid)
        {
            var filters = string.Format("d.filters = {0}{1}", JsonConvert.SerializeObject(grid.Filters), string.IsNullOrEmpty(grid.DataSource.Data) ? string.Empty : ",");

            return $@"function (d) {{
                    {(grid.Filters.Count > 0 ? filters : string.Empty)}
                    {(string.IsNullOrEmpty(grid.DataSource.Data) ? string.Empty : string.Format("d.data = {0}()", grid.DataSource.Data))}
                    }}";
        }
    }
}
