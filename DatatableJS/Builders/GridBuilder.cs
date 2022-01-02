using System;
using System.Collections.Generic;

namespace DatatableJS
{
    /// <summary>
    /// Generic grid builder class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GridBuilder<T>
    {
        internal string _name { get; private set; } = "DataGrid";
        internal string _url { get; private set; }
        internal bool _ordering { get; private set; } = true;
        internal bool _searching { get; private set; } = true;
        internal int _leftColumns { get; private set; }
        internal int _rightColumns { get; private set; }
        internal bool _serverSide { get; private set; } = true;
        internal string _method { get; private set; }
        internal string _data { get; private set; }
        internal string _cssClass { get; private set; } = "display nowrap dataTable dtr-inline collapsed";
        internal string _captionTop { get; private set; }
        internal string _captionBottom { get; private set; }
        internal bool _paging { get; private set; } = true;
        internal string _langUrl { get; private set; }
        internal bool _columnSearching { get; private set; }
        internal string _columnSearchingCss { get; private set; }

        internal List<ColumnDefinition> _columns = new List<ColumnDefinition>();
        internal List<FilterModel> _filters = new List<FilterModel>();
        internal List<OrderModel> _orders = new List<OrderModel>();

        /// <summary>
        /// Default name is "DataGrid".
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GridBuilder<T> Name(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Define table columns.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public GridBuilder<T> Columns(Action<ColumnBuilder<T>> config)
        {
            var builder = new ColumnBuilder<T>(this);
            config(builder);
            return this;
        }

        /// <summary>
        /// Filter data with request.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public GridBuilder<T> Filters(Action<FilterBuilder<T>> config)
        {
            var builder = new FilterBuilder<T>(this);
            config(builder);
            return this;
        }

        /// <summary>
        /// Set the action url and type, default type is GET.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public GridBuilder<T> URL(string url, string method)
        {
            _url = url;
            _method = method;
            return this;
        }

        /// <summary>
        /// Set the action url and type, default type is GET.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public GridBuilder<T> URL(string url)
        {
            return URL(url, "GET");
        }

        /// <summary>
        /// Disable or enable ordering, default is true.
        /// </summary>
        /// <param name="ordering"></param>
        /// <returns></returns>
        public GridBuilder<T> Ordering(bool ordering)
        {
            _ordering = ordering;
            return this;
        }

        /// <summary>
        /// Enable ordering and set default order.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public GridBuilder<T> Order(Action<OrderBuilder<T>> config)
        {
            if (!_ordering)
            {
                _ordering = true;
            }
            var builder = new OrderBuilder<T>(this);
            config(builder);
            return this;
        }

        /// <summary>
        /// Disable or enable searching, default is true.
        /// </summary>
        /// <param name="searching"></param>
        /// <returns></returns>
        public GridBuilder<T> Searching(bool searching)
        {
            _searching = searching;
            return this;
        }

        /// <summary>
        /// Fix the table columns from left or right.
        /// </summary>
        /// <param name="leftColumns"></param>
        /// <param name="rightColumns"></param>
        /// <returns></returns>
        public GridBuilder<T> FixedColumns(int leftColumns, int rightColumns)
        {
            _leftColumns = leftColumns;
            _rightColumns = rightColumns;
            return this;
        }

        /// <summary>
        /// Fix the table columns from left.
        /// </summary>
        /// <param name="leftColumns"></param>
        /// <returns></returns>
        public GridBuilder<T> FixedColumns(int leftColumns)
        {
            return FixedColumns(leftColumns, 0);
        }

        /// <summary>
        /// Proccessing data server side or client side, default is false.
        /// </summary>
        /// <param name="serverSide"></param>
        /// <returns></returns>
        public GridBuilder<T> ServerSide(bool serverSide)
        {
            _serverSide = _paging ? serverSide : _paging;
            return this;
        }

        /// <summary>
        /// Passing additional data to action set name of javascript function.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public GridBuilder<T> Data(string data)
        {
            _data = data;
            return this;
        }

        /// <summary>
        /// Set css class of table.
        /// </summary>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        public GridBuilder<T> Class(string cssClass)
        {
            _cssClass = cssClass;
            return this;
        }

        /// <summary>
        /// Define table top or bottom captions.
        /// </summary>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public GridBuilder<T> Captions(string top, string bottom)
        {
            _captionTop = top;
            _captionBottom = bottom;
            return this;
        }

        /// <summary>
        /// Define table top caption.
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public GridBuilder<T> Captions(string top)
        {
            return Captions(top, null);
        }

        /// <summary>
        /// Disable or enable paging, default is true.
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        public GridBuilder<T> Paging(bool paging)
        {
            if (!paging)
            {
                _serverSide = false;
            }
            _paging = paging;
            return this;
        }

        /// <summary>
        /// Specify language json url from cdn or local.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public GridBuilder<T> Language(string url)
        {
            _langUrl = url;
            return this;
        }

        /// <summary>
        /// Enable columns search feature on footer.
        /// </summary>
        /// <param name="searching">if set to <c>true</c> [searching].</param>
        /// <param name="css"></param>
        /// <returns></returns>
        public GridBuilder<T> ColumnSearching(bool searching, string css)
        {
            _columnSearching = searching;
            _columnSearchingCss = css;
            return this;
        }

        /// <summary>
        /// Enable columns search feature on footer.
        /// </summary>
        /// <param name="searching"></param>
        /// <returns></returns>
        public GridBuilder<T> ColumnSearching(bool searching)
        {
            return ColumnSearching(searching, "");
        }
    }
}
