using System;
using System.Collections.Generic;
using System.Text;

namespace EFDatatable.Data
{
    public class GridBuilder<T>
    {
        public string _name { get; set; } = "DataGrid";
        public string _url { get; set; }
        public bool _ordering { get; set; } = true;
        public bool _searching { get; set; } = true;
        public int _leftColumns { get; set; }
        public int _rightColumns { get; set; }
        public bool _serverSide { get; set; }
        public string _method { get; set; }
        public string _data { get; set; }
        public string _cssClass { get; set; } = "display nowrap dataTable dtr-inline collapsed";

        public List<ColumnDefinition> _columns = new List<ColumnDefinition>();
        public List<FilterDefinition> _filters = new List<FilterDefinition>();

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

        public GridBuilder<T> Class(string cssClass)
        {
            _cssClass = cssClass;
            return this;
        }
    }
}
