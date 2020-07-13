using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace EFDatatable
{
    /// <summary>
    /// EF() helper extenstions
    /// </summary>
    public class EFHelper
    {
        private readonly HtmlHelper _htmlHelper;

        /// <summary>
        /// Call EF() helper extensions after
        /// </summary>
        /// <param name="helper"></param>
        public EFHelper(HtmlHelper helper)
        {
            _htmlHelper = helper;
        }

        /// <summary>
        /// Make a datatable with expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("This method is deprecated, please use Datatable() instead.")]
        public GridBuilder<T> GridFor<T>() where T : class
        {
            return new GridBuilder<T>();
        }

        /// <summary>
        /// Make a datatable with expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public GridBuilder<T> Datatable<T>() where T : class
        {
            return new GridBuilder<T>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class EFHelperExtension
    {
        /// <summary>
        /// Splitted html helpers extensions
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static EFHelper EF(this HtmlHelper helper)
        {
            return new EFHelper(helper);
        }

        private static string ToLowString(this bool b)
        {
            return b.ToString().ToLower();
        }

        /// <summary>
        /// Render datatable script for prepared grid builder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridBuilder"></param>
        /// <returns></returns>
        public static MvcHtmlString Render<T>(this GridBuilder<T> gridBuilder)
        {
            var tfoot = gridBuilder._columnSearching ? 
                        $@"<tfoot>
                            <tr>
                                {string.Join(Environment.NewLine, gridBuilder._columns.Select(a => string.Format("<th>{0}</th>", a.Searchable ? $"<input type=\"text\" style=\"width:100%\" placeholder=\"{a.Title}\" />" : "")))}
                            </tr>
                        </tfoot>" 
                        : string.Empty;

            var tfootInit = gridBuilder._columnSearching ? 
                                $@"initComplete: function () {{
                                        this.api().columns().every(function() {{
                                            var that = this;
                                            $('input', this.footer()).on('keyup change clear', function () {{
                                                if (that.search() !== this.value) {{
                                                    that
                                                        .search(this.value)
                                                        .draw();
                                                }}
                                            }});
                                        }});
                                    }},"
                                    : string.Empty;
            var html = $@"
                    <table id=""{gridBuilder._name}"" class=""{gridBuilder._cssClass}"" style=""width:100%"">
                        <thead>
                            <tr>
                                {string.Join(Environment.NewLine, gridBuilder._columns.Select(a => string.Format("<th>{0}</th>", a.Title)))}
                            </tr>
                        </thead>
                        {tfoot}
                    </table>
                    <script>
                    $(document).ready(function () {{
                        $('#{gridBuilder._name}').DataTable( {{
                            {tfootInit}
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

        private static string GetDataStr<T>(this GridBuilder<T> gridBuilder)
        {
            var filters = string.Format("d.filters = {0}{1}", JsonConvert.SerializeObject(gridBuilder._filters), string.IsNullOrEmpty(gridBuilder._data) ? string.Empty : ",");

            return $@"function (d) {{
                    {(gridBuilder._filters.Count > 0 ? filters : string.Empty)}
                    {(string.IsNullOrEmpty(gridBuilder._data) ? string.Empty : string.Format("d.data = {0}()", gridBuilder._data))}
                    }}";
        }
    }
}
