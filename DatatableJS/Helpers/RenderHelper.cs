using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System.Linq;

namespace DatatableJS
{
    /// <summary>
    /// Render datatable to create
    /// </summary>
    [HtmlTargetElement("render", ParentTag = "js-datatable", TagStructure = TagStructure.WithoutEndTag)]
    public class RenderHelper : TagHelper
    {
        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "script";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.Clear();
            var grid = CommonHelpers.GetGrid(context);

            var lengthMenu = (grid.LengthMenu._lengthMenuValues.Count == 0) ? string.Empty :
                        $"lengthMenu: {string.Format("[[{0}], [{1}]]", string.Join(", ", grid.LengthMenu._lengthMenuValues), string.Join(", ", grid.LengthMenu._lengthMenuDisplayedTexts.Select(a => string.Concat(@"""", a, @""""))))},"
                        ;

            var script = $@"
            $(document).ready(function () {{
                $('#{grid.Name}').DataTable( {{
                    processing: {grid.Processing.ToLowString()},
                    scrollX: {grid.ScrollX.ToLowString()},
                    stateSave: {grid.StateSave.ToLowString()},
                    serverSide: {grid.DataSource.ServerSide.ToLowString()},
                    fixedColumns: {{ 
                        leftColumns: {grid.FixedColumns?.LeftColumns},
                        rightColumns: {grid.FixedColumns?.RightColumns}
                    }},
                    order: [{(!grid.Ordering ? string.Empty : string.Join(", ", grid.Orders.Select(a => $@"[{ a.Column}, '{(a.OrderBy == OrderBy.Ascending ? "asc" : "desc")}']")))}],
                    ordering: {grid.Ordering.ToLowString()},
                    searching: {grid.Searching.ToLowString()},
                    paging: {grid.DataSource.Paging.ToLowString()},
                    {lengthMenu}
                    {(!string.IsNullOrEmpty(grid.Callback.CreatedRow) ? $"createdRow: function (row, data, dataIndex, cells) {{ {grid.Callback.CreatedRow}(row, data, dataIndex, cells); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.DrawCallback) ? $"drawCallback: function (settings) {{ {grid.Callback.DrawCallback}(settings); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.FooterCallback) ? $"footerCallback: function (tfoot, data, start, end, display) {{ {grid.Callback.FooterCallback}(tfoot, data, start, end, display); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.FormatNumber) ? $"formatNumber: function (toFormat) {{ {grid.Callback.FormatNumber}(toFormat); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.HeaderCallback) ? $"headerCallback: function (thead, data, start, end, display) {{ {grid.Callback.HeaderCallback}(thead, data, start, end, display); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.InfoCallback) ? $"infoCallback: function (settings, start, end, max, total, pre) {{ {grid.Callback.InfoCallback}(settings, start, end, max, total, pre); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.InitComplete) ? $"initComplete: function (settings, json) {{ {grid.Callback.InitComplete}(settings, json); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.PreDrawCallback) ? $"preDrawCallback: function (settings) {{ {grid.Callback.PreDrawCallback}(settings); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.RowCallback) ? $"rowCallback: function (row, data, displayNum, displayIndex, dataIndex) {{ {grid.Callback.RowCallback}(row, data, displayNum, displayIndex, dataIndex); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.StateLoadCallback) ? $"stateLoadCallback: function (settings, callback) {{ {grid.Callback.StateLoadCallback}(settings, callback); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.StateLoadParams) ? $"stateLoadParams: function (settings, data) {{ {grid.Callback.StateLoadParams}(settings, data); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.StateLoaded) ? $"stateLoaded: function (settings, data) {{ {grid.Callback.StateLoaded}(settings, data); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.StateSaveCallback) ? $"stateSaveCallback: function (settings, data) {{ {grid.Callback.StateSaveCallback}(settings, data); }}," : string.Empty) }
                    {(!string.IsNullOrEmpty(grid.Callback.StateSaveParams) ? $"stateSaveParams: function (settings, data) {{ {grid.Callback.StateSaveParams}(settings, data); }}," : string.Empty) }
                    {(!grid.DataSource.PageLength.HasValue ? string.Empty : $"pageLength: {grid.DataSource.PageLength.Value},")}
                    language: {{
                        url: '{grid.Language.URL}',
                        {(!string.IsNullOrEmpty(grid.Language.Decimal) ? $"decimal: '{grid.Language.Decimal}'," : string.Empty)}
                        {(!string.IsNullOrEmpty(grid.Language.EmptyTable) ? $"emptyTable: '{grid.Language.EmptyTable}'," : string.Empty)}
                        {(!string.IsNullOrEmpty(grid.Language.Info) ? $"info: '{grid.Language.Info}'," : string.Empty)}
                        {(!string.IsNullOrEmpty(grid.Language.InfoEmpty) ? $"infoEmpty: '{grid.Language.InfoEmpty}'," : string.Empty)}
                        {(!string.IsNullOrEmpty(grid.Language.InfoFiltered) ? $"infoFiltered: '{grid.Language.InfoFiltered}'," : string.Empty)}
                        {(!string.IsNullOrEmpty(grid.Language.InfoPostFix) ? $"infoPostFix: '{grid.Language.InfoPostFix}'," : string.Empty)}
                        {(!string.IsNullOrEmpty(grid.Language.Thousands) ? $"thousands: '{grid.Language.Thousands}'," : string.Empty)}
                        {(!string.IsNullOrEmpty(grid.Language.LengthMenu) ? $"lengthMenu: '{grid.Language.LengthMenu}'," : string.Empty)}
                        {(!string.IsNullOrEmpty(grid.Language.LoadingRecords) ? $"loadingRecords: '{grid.Language.LoadingRecords}'," : string.Empty)}
                        {(!string.IsNullOrEmpty(grid.Language.Processing) ? $"processing: '{grid.Language.Processing}'," : string.Empty)}
                        {(!string.IsNullOrEmpty(grid.Language.Search) ? $"search: '{grid.Language.Search}'," : string.Empty)}
                        {(!string.IsNullOrEmpty(grid.Language.ZeroRecords) ? $"zeroRecords: '{grid.Language.ZeroRecords}'," : string.Empty)}
                        paginate: {{
                                {(!string.IsNullOrEmpty(grid.Language.Paginate.First) ? $"first: '{grid.Language.Paginate.First}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid.Language.Paginate.Last) ? $"last: '{grid.Language.Paginate.Last}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid.Language.Paginate.Next) ? $"next: '{grid.Language.Paginate.Next}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid.Language.Paginate.Previous) ? $"previous: '{grid.Language.Paginate.Previous}'," : string.Empty)}
                            }},
                        aria: {{
                                {(!string.IsNullOrEmpty(grid.Language.Aria.SortAscending) ? $"sortAscending: '{grid.Language.Aria.SortAscending}'," : string.Empty)}
                                {(!string.IsNullOrEmpty(grid.Language.Aria.SortDescending) ? $"sortDescending: '{grid.Language.Aria.SortDescending}'," : string.Empty)}
                            }}
                    }},
                    ajax: {{
                            url: ""{grid.DataSource.URL}"",
                            type: ""{grid.DataSource.Method}"",
                            data: {GetDataStr(grid)}
                          }},
                    columns: [{string.Join(", ", grid.Columns.Select(a => $@"{{ 
                                data: '{(grid.DataSource.CamelCase ? char.ToLowerInvariant(a.Data[0]) + a.Data.Substring(1) : a.Data)}',
                                name: '{a.Data}',
                                defaultContent: '{a.DefaultContent}',
                                orderable: {a.Orderable.ToLowString()},
                                searchable: {a.Searchable.ToLowString()},
                                className: '{a.ClassName}',
                                visible: {a.Visible.ToLowString()},
                                width: '{a.Width}%',
                                    {(string.IsNullOrEmpty(a.Render) ? string.Empty : $"'render': function(data, type, row, meta) {{ return {a.Render}; }}")}
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
