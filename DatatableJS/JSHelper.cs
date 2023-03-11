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
        /// <param name="grid"></param>
        /// <returns></returns>
        public static IHtmlContent Render<T>(this GridBuilder<T> grid)
        {
            var tfoot = grid._columnSearching ?
                        $@"<tfoot>
                            <tr class=""filters"">
                                {string.Join(Environment.NewLine, grid._columns.Select(a => string.Format("<th>{0}</th>", a.Searchable ? $"<input type=\"{((a.Type == typeof(DateTime?) || a.Type == typeof(DateTime)) && grid._serverSide ? "date" : "text")}\" style=\"width:100%\" placeholder=\"{a.Title}\" class=\"{grid._columnSearchingCss}\" />" : "<input style=\"display:none\" />")))}
                            </tr>
                        </tfoot>"
                        : string.Empty;

            var tfootInit = string.Empty;

            var initFootSearch = grid._stateSave ?
                        $@"var dtable = $('#{grid._name}').DataTable();
                        var dState = dtable.state.loaded();
                        var dCounter = -1;
                        if (dState) {{
                            dtable.columns().eq(0).each(function(colIdx) {{
                            var colSearch = dState.columns[colIdx].search;                       
                            if (dState.columns[colIdx].visible){{
                                dCounter++;
                            }}
                            if (colSearch.search) {{
                                $('input', $('.filters th')[dCounter]).val(colSearch.search);
                               }}
                            }});
                            dtable.draw();
                        }};"
                        : string.Empty;

            if (!String.IsNullOrEmpty(grid._callBack.InitComplete) && grid._columnSearching == false)
            {
                tfootInit = "initComplete: function (settings, json) {";
                tfootInit += grid._callBack.InitComplete + "(settings, json);";
                tfootInit += "},";
            }
            else if (!String.IsNullOrEmpty(grid._callBack.InitComplete) && grid._columnSearching == true)
            {
                tfootInit = grid._columnSearching ?
                                $@"initComplete: function (settings, json) {{
                                        {grid._callBack.InitComplete}(settings, json);
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
                                        {initFootSearch}
                                    }},"
                                    : string.Empty;
            }
            else if (String.IsNullOrEmpty(grid._callBack.InitComplete) && grid._columnSearching == true)
            {
                tfootInit = grid._columnSearching ?
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
                                        {initFootSearch}
                                    }},"
                                    : string.Empty;
            }

            var selectInit = grid._selectEnable ?
                                $@"select: {{
                                        info: {grid._selectInfo.ToLowString()},
                                        style: '{grid._selectStyle.GetEnumDescription()}',
                                        items: '{grid._selectItems.GetEnumDescription()}',
                                        toggleable: {grid._selectToggleable.ToLowString()},
                                        selector: '{(grid._selectItems == SelectItems.Checkbox ? "td:first-child" : string.Empty)}',
                                        blurable: true
                                    }},"
                                    : string.Empty;

            var colReorderInit = grid._colReorder.Settings ?
                                 $@"colReorder: {{
                                        enable: {grid._colReorder.Enable.ToLowString()},
                                        fixedColumnsLeft: {grid._colReorder.FixedColumnsLeft},
                                        fixedColumnsRight: {grid._colReorder.FixedColumnsRight},
                                        order: {grid._colReorder.Order},
                                        realtime: {grid._colReorder.RealTime.ToLowString()}
                                    }}," 
                                    : $"colReorder: {grid._colReorder.ColReorder.ToLowString()},";

            var lengthMenu = (
                    grid._lengthMenuValues.Count == 0
                ) ? string.Empty :
                    $"lengthMenu: {string.Format("[[{0}], [{1}]]", string.Join(", ", grid._lengthMenuValues), string.Join(", ", grid._lengthMenuDisplayedTexts.Select(a => string.Concat(@"""", a, @""""))))},"
                ;

            var html = $@"
                    <table id=""{grid._name}"" class=""{grid._cssClass}"" style=""width:100%"">
                        <thead>
                            <tr>
                                {string.Join(Environment.NewLine, grid._columns.Select(a => string.Format("<th>{0}</th>", a.Title)))}
                            </tr>
                        </thead>
                        {tfoot}
                    </table>
                    <script>
                    $(document).ready(function () {{
                        $('#{grid._name}').DataTable( {{
                            {tfootInit}
                            processing: {grid._processing.ToLowString()},
                            scrollX: {grid._scrollX.ToLowString()},
                            stateSave: {grid._stateSave.ToLowString()},
                            serverSide: {grid._serverSide.ToLowString()},
                            {selectInit}
                            {colReorderInit}
                            fixedColumns: {{ 
                                leftColumns: {grid._leftColumns},
                                rightColumns: {grid._rightColumns}
                            }},
                            order: [{(!grid._ordering ? string.Empty : string.Join(", ", grid._orders.Select(a => $@"[{ a.Column}, '{(a.OrderBy == OrderBy.Ascending ? "asc" : "desc")}']")))}],
                            ordering: {grid._ordering.ToLowString()},
                            searching: {grid._searching.ToLowString()},
                            paging: {grid._paging.ToLowString()},
                            {lengthMenu}
                            {(!string.IsNullOrEmpty(grid._callBack.CreatedRow) ? $"createdRow: function (row, data, dataIndex, cells) {{ {grid._callBack.CreatedRow}(row, data, dataIndex, cells); }}," : string.Empty) }
                            {(!string.IsNullOrEmpty(grid._callBack.DrawCallback) ? $"drawCallback: function (settings) {{ {grid._callBack.DrawCallback}(settings); }}," : string.Empty) }
                            {(!string.IsNullOrEmpty(grid._callBack.FooterCallback) ? $"footerCallback: function (tfoot, data, start, end, display) {{ {grid._callBack.FooterCallback}(tfoot, data, start, end, display); }}," : string.Empty) }
                            {(!string.IsNullOrEmpty(grid._callBack.FormatNumber) ? $"formatNumber: function (toFormat) {{ {grid._callBack.FormatNumber}(toFormat); }}," : string.Empty) }
                            {(!string.IsNullOrEmpty(grid._callBack.HeaderCallback) ? $"headerCallback: function (thead, data, start, end, display) {{ {grid._callBack.HeaderCallback}(thead, data, start, end, display); }}," : string.Empty) }
                            {(!string.IsNullOrEmpty(grid._callBack.InfoCallback) ? $"infoCallback: function (settings, start, end, max, total, pre) {{ {grid._callBack.InfoCallback}(settings, start, end, max, total, pre); }}," : string.Empty) }
                            {(!string.IsNullOrEmpty(grid._callBack.PreDrawCallback) ? $"preDrawCallback: function (settings) {{ {grid._callBack.PreDrawCallback}(settings); }}," : string.Empty) }
                            {(!string.IsNullOrEmpty(grid._callBack.RowCallback) ? $"rowCallback: function (row, data, displayNum, displayIndex, dataIndex) {{ {grid._callBack.RowCallback}(row, data, displayNum, displayIndex, dataIndex); }}," : string.Empty) }
                            {(!string.IsNullOrEmpty(grid._callBack.StateLoadCallback) ? $"stateLoadCallback: function (settings, callback) {{ {grid._callBack.StateLoadCallback}(settings, callback); }}," : string.Empty) }
                            {(!string.IsNullOrEmpty(grid._callBack.StateLoadParams) ? $"stateLoadParams: function (settings, data) {{ {grid._callBack.StateLoadParams}(settings, data); }}," : string.Empty) }
                            {(!string.IsNullOrEmpty(grid._callBack.StateLoaded) ? $"stateLoaded: function (settings, data) {{ {grid._callBack.StateLoaded}(settings, data); }}," : string.Empty) }
                            {(!string.IsNullOrEmpty(grid._callBack.StateSaveCallback) ? $"stateSaveCallback: function (settings, data) {{ {grid._callBack.StateSaveCallback}(settings, data); }}," : string.Empty) }
                            {(!string.IsNullOrEmpty(grid._callBack.StateSaveParams) ? $"stateSaveParams: function (settings, data) {{ {grid._callBack.StateSaveParams}(settings, data); }}," : string.Empty) }
                            {(!grid._pageLength.HasValue ? string.Empty : $"pageLength: {grid._pageLength.Value},")}
                            language: {{
                                url: '{grid._language.URL}',
                                {(!string.IsNullOrEmpty(grid._language.Decimal) ? $"decimal: '{grid._language.Decimal}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid._language.EmptyTable) ? $"emptyTable: '{grid._language.EmptyTable}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid._language.Info) ? $"info: '{grid._language.Info}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid._language.InfoEmpty) ? $"infoEmpty: '{grid._language.InfoEmpty}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid._language.InfoFiltered) ? $"infoFiltered: '{grid._language.InfoFiltered}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid._language.InfoPostFix) ? $"infoPostFix: '{grid._language.InfoPostFix}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid._language.Thousands) ? $"thousands: '{grid._language.Thousands}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid._language.LengthMenu) ? $"lengthMenu: '{grid._language.LengthMenu}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid._language.LoadingRecords) ? $"loadingRecords: '{grid._language.LoadingRecords}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid._language.Processing) ? $"processing: '{grid._language.Processing}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid._language.Search) ? $"search: '{grid._language.Search}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid._language.ZeroRecords) ? $"zeroRecords: '{grid._language.ZeroRecords}'," : string.Empty)}
                                paginate: {{
                                        {(!string.IsNullOrEmpty(grid._language.Paginate.First) ? $"first: '{grid._language.Paginate.First}'," : string.Empty)}
                                        {(!string.IsNullOrEmpty(grid._language.Paginate.Last) ? $"last: '{grid._language.Paginate.Last}'," : string.Empty)}
                                        {(!string.IsNullOrEmpty(grid._language.Paginate.Next) ? $"next: '{grid._language.Paginate.Next}'," : string.Empty)}
                                        {(!string.IsNullOrEmpty(grid._language.Paginate.Previous) ? $"previous: '{grid._language.Paginate.Previous}'," : string.Empty)}
                                    }},
                                aria: {{
                                        {(!string.IsNullOrEmpty(grid._language.Aria.SortAscending) ? $"sortAscending: '{grid._language.Aria.SortAscending}'," : string.Empty)}
                                        {(!string.IsNullOrEmpty(grid._language.Aria.SortDescending) ? $"sortDescending: '{grid._language.Aria.SortDescending}'," : string.Empty)}
                                    }}
                            }},
                            ajax: {{
                                url: '{grid._url}',
                                type: '{grid._method}',
                                data: {grid.GetDataStr()}
                            }},
                            columns: [{string.Join(", ", grid._columns.Select(a => $@"{{ 
                                data: '{(grid._camelCase ? char.ToLowerInvariant(a.Data[0]) + a.Data.Substring(1) : a.Data)}',
                                name: '{a.Data}',
                                defaultContent: '{a.DefaultContent}',
                                orderable: {a.Orderable.ToLowString()},
                                searchable: {a.Searchable.ToLowString()},
                                className: '{a.ClassName}',
                                visible: {a.Visible.ToLowString()},
                                width: '{(a.Width > 0 ? $"{a.Width}%" : string.Empty)}',
                                {(string.IsNullOrEmpty(a.Render) ? string.Empty : $"'render': function(data, type, row, meta) {{ return {a.Render}; }}")}
                            }}"))}]
                        }});
                    }});
                    {(string.IsNullOrEmpty(grid._captionTop) ? string.Empty : string.Format("$('#{0}').append('<caption style=\"caption-side:top\">{1}</caption>');", grid._name, grid._captionTop))}
                    {(string.IsNullOrEmpty(grid._captionBottom) ? string.Empty : string.Format("$('#{0}').append('<caption style=\"caption-side:bottom\">{1}</caption>');", grid._name, grid._captionBottom))}
                    </script>";

            return new HtmlString(html);
        }

        private static string GetDataStr<T>(this GridBuilder<T> grid)
        {
            var filters = string.Format("d.filters = {0}{1}", JsonConvert.SerializeObject(grid._filters), string.IsNullOrEmpty(grid._data) ? string.Empty : ",");

            return $@"function (d) {{
                    {(grid._filters.Count > 0 ? filters : string.Empty)}
                    {(string.IsNullOrEmpty(grid._data) ? string.Empty : string.Format("d.data = {0}()", grid._data))}
                    }}";
        }
    }
}
