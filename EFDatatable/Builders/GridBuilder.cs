using System;
using System.Collections.Generic;

namespace EFDatatable
{
    public class GridBuilder<T>
    {
        internal string _name { get; private set; } = "DataGrid";
        internal string _url { get; private set; }
        internal bool _ordering { get; private set; } = true;
        internal bool _searching { get; private set; } = true;
        internal int _leftColumns { get; private set; }
        internal int _rightColumns { get; private set; }
        internal bool _serverSide { get; private set; }
        internal string _method { get; private set; }
        internal string _data { get; private set; }
        internal string _cssClass { get; private set; } = "display nowrap dataTable dtr-inline collapsed";
        internal string _captionTop { get; private set; }
        internal  string _captionBottom { get; set; }

        internal List<ColumnDefinition> _columns = new List<ColumnDefinition>();
        internal List<FilterDefinition> _filters = new List<FilterDefinition>();

        /// <summary>
        /// Default name is "DataGrid"
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Define table top or bottom captions
        /// </summary>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public GridBuilder<T> Captions(string top = null, string bottom = null)
        {
            _captionTop = top;
            _captionBottom = bottom;
            return this;
        }
    }
}
