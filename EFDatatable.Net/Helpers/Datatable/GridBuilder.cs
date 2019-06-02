using EFDatatable.Models.Definitions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EFDatatable.Net.Helpers.Datatable
{
    public class GridBuilder<T>
    {
        private readonly HtmlHelper _htmlHelper;
        private string _name { get; set; } = "DataGrid";
        private string _url { get; set; }
        private string _query { get; set; }
        private bool _ordering { get; set; } = true;
        private bool _searching { get; set; } = true;
        private int _leftColumns { get; set; }
        private int _rightColumns { get; set; }
        private bool _serverSide { get; set; }
        private string _method { get; set; }
        private string _data { get; set; }

        internal List<ColumnDefinition> _columns = new List<ColumnDefinition>();
        internal List<FilterDefinition> _filters = new List<FilterDefinition>();

        public GridBuilder(HtmlHelper helper)
        {
            _htmlHelper = helper;
        }

        public GridBuilder<T> Name(string name)
        {
            _name = name;
            return this;
        }

        public GridBuilder<T> Columns(Action<ColumnBuilder<T>> config)
        {
            var builder = new ColumnBuilder<T>(this);
            config(builder);
            return this;
        }

        public GridBuilder<T> Filters(Action<FilterBuilder<T>> config)
        {
            var builder = new FilterBuilder<T>(this);
            config(builder);
            return this;
        }

        public GridBuilder<T> URL(string url, string method = "GET")
        {
            _url = url;
            _method = method;
            return this;
        }

        public GridBuilder<T> Ordering(bool ordering)
        {
            _ordering = ordering;
            return this;
        }

        public GridBuilder<T> Searching(bool searching)
        {
            _searching = searching;
            return this;
        }

        public GridBuilder<T> FixedColumns(int leftColumns = 0, int rightColums = 0)
        {
            _leftColumns = leftColumns;
            _rightColumns = rightColums;
            return this;
        }

        public GridBuilder<T> ServerSide(bool serverSide)
        {
            _serverSide = serverSide;
            return this;
        }

        public GridBuilder<T> Data(string data)
        {
            _data = data;
            return this;
        }

        public MvcHtmlString Render()
        {
            var html = $@"
                    <table id=""{_name}"" class=""table table-bordered table-striped"">
                        <thead>
                            <tr>
                                {string.Join(Environment.NewLine, _columns.Select(a => string.Format("<th>{0}</th>", a.Title)))}
                            </tr>
                        </thead>
                    </table>
                    <script>
                    $(document).ready(function () {{
                        $('#{_name}').DataTable( {{
                            processing:true,
                            serverSide:{_serverSide.ToLowString()},
                            fixedColumns: {{ 
                                leftColumns: {_leftColumns},
                                rightColumns: {_rightColumns}
                            }},
                            order:[],
                            orderdering: {_ordering.ToLowString()},
                            searching: {_searching.ToLowString()},
                            ajax: {{
                                url: ""{_url}"",
                                type: ""{_method}"",
                                data: {GetDataStr()}
                            }},
                            columns: [{string.Join(", ", _columns.Select(a => $@"{{ 
                                'data': '{a.Data}',
                                'orderable': {a.Orderable.ToLowString()},
                                'searchable': {a.Searchable.ToLowString()},
                                'className': '{a.ClassName}',
                                'visible': {a.Visible.ToLowString()},
                                'width': '{a.Width}%',
                                    {(string.IsNullOrEmpty(a.Render) ? string.Empty : $"'render': function(data, type, row, meta) {{ return {a.Render}; }}")}
                            }}"))}]
                        }});
                    }});
                    </script>";

            return new MvcHtmlString(html);
        }

        private string GetDataStr()
        {
            var filters = string.Format("d.filters = {0}", JsonConvert.SerializeObject(_filters));

            return $@"function (d) {{
                    {(_filters.Count > 0 ? filters : string.Empty)}
                    {(string.IsNullOrEmpty(_data) ? string.Empty : string.Format(", d.data = {0}()", _data))}
                    }}";
        }
    }
}