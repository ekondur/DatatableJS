using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace EFDatatable
{
    public class EFHelper
    {
        private readonly HtmlHelper _htmlHelper;
        public EFHelper(HtmlHelper helper)
        {
            _htmlHelper = helper;
        }

        public GridBuilder<T> GridFor<T>() where T : class
        {
            return new GridBuilder<T>();
        }
    }

    public static class EFHelperExtension
    {
        public static EFHelper EF(this HtmlHelper helper)
        {
            return new EFHelper(helper);
        }

        public static string ToLowString(this bool b)
        {
            return b.ToString().ToLower();
        }

        public static T GetAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute
        {
            var attributes = provider.GetCustomAttributes(typeof(T), true);
            return attributes.Length > 0 ? attributes[0] as T : null;
        }

        public static MvcHtmlString Render<T>(this GridBuilder<T> gridBuilder)
        {
            var html = $@"
                    <table id=""{gridBuilder._name}"" class=""{gridBuilder._cssClass}"" style=""width:100%"">
                        <thead>
                            <tr>
                                {string.Join(Environment.NewLine, gridBuilder._columns.Select(a => string.Format("<th>{0}</th>", a.Title)))}
                            </tr>
                        </thead>
                    </table>
                    <script>
                    $(document).ready(function () {{
                        $('#{gridBuilder._name}').DataTable( {{
                            processing:true,
                            serverSide:{gridBuilder._serverSide.ToLowString()},
                            fixedColumns: {{ 
                                leftColumns: {gridBuilder._leftColumns},
                                rightColumns: {gridBuilder._rightColumns}
                            }},
                            order:[],
                            orderdering: {gridBuilder._ordering.ToLowString()},
                            searching: {gridBuilder._searching.ToLowString()},
                            paging: {gridBuilder._paging.ToLowString()},
                            language: {{
                                'url': '{gridBuilder._langUrl}'
                            }},
                            ajax: {{
                                url: ""{gridBuilder._url}"",
                                type: ""{gridBuilder._method}"",
                                data: {gridBuilder.GetDataStr()}
                            }},
                            columns: [{string.Join(", ", gridBuilder._columns.Select(a => $@"{{ 
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
                    {(string.IsNullOrEmpty(gridBuilder._captionTop) ? string.Empty : string.Format("$('#{0}').append('<caption style=\"caption-side:top\">{1}</caption>');", gridBuilder._name, gridBuilder._captionTop))}
                    {(string.IsNullOrEmpty(gridBuilder._captionBottom) ? string.Empty : string.Format("$('#{0}').append('<caption style=\"caption-side:bottom\">{1}</caption>');", gridBuilder._name, gridBuilder._captionBottom))}
                    </script>";

            return new MvcHtmlString(html);
        }

        public static string GetDataStr<T>(this GridBuilder<T> gridBuilder)
        {
            var filters = string.Format("d.filters = {0}{1}", JsonConvert.SerializeObject(gridBuilder._filters), string.IsNullOrEmpty(gridBuilder._data) ? string.Empty : ",");

            return $@"function (d) {{
                    {(gridBuilder._filters.Count > 0 ? filters : string.Empty)}
                    {(string.IsNullOrEmpty(gridBuilder._data) ? string.Empty : string.Format("d.data = {0}()", gridBuilder._data))}
                    }}";
        }
    }
}
