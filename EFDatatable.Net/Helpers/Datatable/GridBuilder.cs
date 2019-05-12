using EFDatatable.Models.Definitions;
using System;
using System.Collections.Generic;
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

        public GridBuilder<T> Query(string query)
        {
            _query = query;
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

        public GridBuilder<T> URL(string url)
        {
            _url = url;
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
    }
}