using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace DatatableJS
{
    /// <summary>
    /// JS() helper extenstions
    /// </summary>
    public class JSHelper
    {
        private readonly IHtmlHelper _htmlHelper;

        /// <summary>
        /// Call JS() helper extensions after
        /// </summary>
        /// <param name="helper"></param>
        public JSHelper(IHtmlHelper helper)
        {
            _htmlHelper = helper;
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
    public static class JSHelperExtension
    {
        /// <summary>
        /// Splitted html helpers extensions
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static JSHelper JS(this IHtmlHelper helper)
        {
            return new JSHelper(helper);
        }

        /// <summary>
        /// Render datatable script for prepared grid builder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridBuilder"></param>
        /// <returns></returns>
        public static IHtmlContent Render<T>(this GridBuilder<T> gridBuilder)
        {
            var tfoot = gridBuilder._columnSearching ?
                        $@"<tfoot>
                            <tr>
                                {string.Join(Environment.NewLine, gridBuilder._columns.Select(a => string.Format("<th>{0}</th>", a.Searchable ? $"<input type=\"{((a.Type == typeof(DateTime?) || a.Type == typeof(DateTime)) && gridBuilder._serverSide ? "date" : "text")}\" style=\"width:100%\" placeholder=\"{a.Title}\" class=\"{gridBuilder._columnSearchingCss}\" />" : "")))}
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

            var selectInit = gridBuilder._selectEnable ?
                                $@"select: {{
                                        info: {gridBuilder._selectInfo.ToLowString()},
                                        style: '{gridBuilder._selectStyle.GetEnumDescription()}',
                                        items: '{gridBuilder._selectItems.GetEnumDescription()}',
                                        toggleable: {gridBuilder._selectToggleable.ToLowString()},
                                        selector: '{(gridBuilder._selectItems == SelectItems.Checkbox ? "td:first-child" : string.Empty)}',
                                        blurable: true
                                    }},"
                                    : string.Empty;

            var lengthMenu = (
                    gridBuilder._lengthMenuValues.Count == 0
                ) ? string.Empty : 
                    $"lengthMenu: {string.Format("[[{0}], [{1}]]", string.Join(", ", gridBuilder._lengthMenuValues), string.Join(", ", gridBuilder._lengthMenuDisplayedTexts.Select(a => string.Concat(@"""", a, @""""))))},"
                ;

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
                            processing:{gridBuilder._processing.ToLowString()},
                            scrollX:{gridBuilder._scrollX.ToLowString()},
                            serverSide:{gridBuilder._serverSide.ToLowString()},
                            {selectInit}
                            fixedColumns: {{ 
                                leftColumns: {gridBuilder._leftColumns},
                                rightColumns: {gridBuilder._rightColumns}
                            }},
                            order: [{(!gridBuilder._ordering ? string.Empty : string.Join(", ", gridBuilder._orders.Select(a => $@"[{ a.Column}, '{(a.OrderBy == OrderBy.Ascending ? "asc" : "desc")}']")))}],
                            ordering: {gridBuilder._ordering.ToLowString()},
                            searching: {gridBuilder._searching.ToLowString()},
                            paging: {gridBuilder._paging.ToLowString()},
                            {lengthMenu}
                            {(!gridBuilder._pageLength.HasValue ? string.Empty : $"pageLength: {gridBuilder._pageLength.Value},")}
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
                                'name': '{a.Data}',
                                'defaultContent': '{a.DefaultContent}',
                                'orderable': {a.Orderable.ToLowString()},
                                'searchable': {a.Searchable.ToLowString()},
                                'className': '{a.ClassName}',
                                'visible': {a.Visible.ToLowString()},
                                'width': '{(a.Width > 0 ? $"{a.Width}%" : string.Empty)}',
                                    {(string.IsNullOrEmpty(a.Render) ? string.Empty : $"'render': function(data, type, row, meta) {{ if (data == null) {{ return ''; }} else {{ return {a.Render}; }} }}")}
                            }}"))}]
                        }});
                    }});
                    {(string.IsNullOrEmpty(gridBuilder._captionTop) ? string.Empty : string.Format("$('#{0}').append('<caption style=\"caption-side:top\">{1}</caption>');", gridBuilder._name, gridBuilder._captionTop))}
                    {(string.IsNullOrEmpty(gridBuilder._captionBottom) ? string.Empty : string.Format("$('#{0}').append('<caption style=\"caption-side:bottom\">{1}</caption>');", gridBuilder._name, gridBuilder._captionBottom))}
                    </script>";

            return new HtmlString(html);
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
