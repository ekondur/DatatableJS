using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

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
        internal bool _serverSide { get; private set; }
        internal string _method { get; private set; }
        internal bool _camelCase { get; private set; }
        internal string _data { get; private set; }
        internal string _cssClass { get; private set; } = "display nowrap dataTable dtr-inline collapsed";
        internal string _captionTop { get; private set; }
        internal string _captionBottom { get; private set; }
        internal bool _paging { get; private set; } = true;
        internal bool _columnSearching { get; private set; }
        internal string _columnSearchingCss { get; private set; }
        internal SearchPosition _columnSearchPosition { get; set; } = SearchPosition.Footer;
        internal List<int> _lengthMenuValues { get; private set; } = new List<int>();
        internal List<string> _lengthMenuDisplayedTexts { get; private set; } = new List<string>();
        internal int? _pageLength { get; private set; }
        internal bool _processing { get; private set; } = true;
        internal bool _scrollX { get; private set; }
        internal bool _stateSave { get; set; }
        internal string _dom { get; private set; }

        internal bool _selectEnable { get; private set; }
        internal SelectStyle _selectStyle { get; private set; }
        internal SelectItems _selectItems { get; private set; }
        internal bool _selectInfo { get; private set; }
        internal bool _selectToggleable { get; private set; }
        public IHtmlHelper HtmlHelper { get; }

        internal List<ColumnDefinition> _columns = new List<ColumnDefinition>();
        internal List<FilterModel> _filters = new List<FilterModel>();
        internal List<OrderModel> _orders = new List<OrderModel>();

        internal CallbackModel _callBack = new CallbackModel();
        internal LanguageModel _language = new LanguageModel();
        internal ColReorderModel _colReorder = new ColReorderModel();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        public GridBuilder(IHtmlHelper htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }

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
        /// Enable ordering and set default orders.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public GridBuilder<T> Orders(Action<OrderBuilder<T>> config)
        {
            _ordering = true;
            var builder = new OrderBuilder<T>(this);
            config(builder);
            return this;
        }

        /// <summary>
        /// Set callback functions. <see href="https://datatables.net/reference/option/">Reference:</see>
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public GridBuilder<T> Callbacks(Action<CallbackBuilder<T>> config)
        {
            var builder = new CallbackBuilder<T>(this);
            config(builder);
            return this;
        }

        /// <summary>
        /// Allowing you to modified all strings individually or completely replace them all as required.  <see href="https://datatables.net/reference/option/language">Reference:</see>
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public GridBuilder<T> Language(Action<LanguageBuilder<T>> config)
        {
            var builder = new LanguageBuilder<T>(this);
            config(builder);
            return this;
        }

        /// <summary>
        /// Specify language json url from cdn or local.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public GridBuilder<T> Language(string url)
        {
            _language.URL = url;
            return this;
        }

        /// <summary>
        /// Set the action url, type and json naming policy is camelcase, default is false.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="camelCase"></param>
        /// <returns></returns>
        public GridBuilder<T> URL(string url, string method, bool camelCase)
        {
            _url = url;
            _method = method;
            _camelCase = camelCase;
            return this;
        }

        /// <summary>
        /// Set the action url and type.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public GridBuilder<T> URL(string url, string method)
        {
            return URL(url, method, false);
        }

        /// <summary>
        /// Set the action url and type, default type is GET.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public GridBuilder<T> URL(string url)
        {
            return URL(url, "GET", false);
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
        /// Enable server-side processing mode.
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
        /// Dom allow to control position of datatable elements
        /// </summary>
        /// <see cref="https://datatables.net/reference/option/dom"/>
        /// <param name="dom"></param>
        /// <returns></returns>
        public GridBuilder<T> Dom(string dom)
        {
            _dom = dom; 
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
        /// Enable columns search feature.
        /// </summary>
        /// <param name="searching">if set to <c>true</c> [searching].</param>
        /// <param name="css"></param>       
        /// <param name="position">default is footer</param>
        /// <returns></returns>
        public GridBuilder<T> ColumnSearching(bool searching, string css, SearchPosition position)
        {
            _columnSearching = searching;
            _columnSearchingCss = css;
            _columnSearchPosition = position;
            return this;
        }

        /// <summary>
        /// Enable columns search feature.
        /// </summary>
        /// <param name="searching">if set to <c>true</c> [searching].</param>
        /// <param name="position">default is footer</param>
        /// <returns></returns>
        public GridBuilder<T> ColumnSearching(bool searching, SearchPosition position)
        {
            return ColumnSearching(searching, "", position);
        }

        /// <summary>
        /// Enable columns search feature.
        /// </summary>
        /// <param name="searching">if set to <c>true</c> [searching].</param>
        /// <param name="css"></param>
        /// <returns></returns>
        public GridBuilder<T> ColumnSearching(bool searching, string css)
        {
            return ColumnSearching(searching, css, SearchPosition.Footer);
        }

        /// <summary>
        /// Enable columns search feature.
        /// </summary>
        /// <param name="searching"></param>
        /// <returns></returns>
        public GridBuilder<T> ColumnSearching(bool searching)
        {
            return ColumnSearching(searching, "", SearchPosition.Footer);
        }

        /// <summary>
        /// Define table length menu.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="hasAll"></param>
        /// <param name="allText"></param>
        /// <returns></returns>
        public GridBuilder<T> LengthMenu(int[] values, bool hasAll, string allText)
        {
            _lengthMenuValues = values.ToList();
            _lengthMenuDisplayedTexts = values.Select(x => x.ToString()).ToList();

            if (!_pageLength.HasValue)
            {
                _pageLength = _lengthMenuValues.FirstOrDefault();
            }

            if (hasAll)
            {
                _lengthMenuValues.Add(-1);
                _lengthMenuDisplayedTexts.Add(allText);
            }

            return this;
        }

        /// <summary>
        /// Define table length menu.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="hasAll"></param>
        /// <returns></returns>
        public GridBuilder<T> LengthMenu(int[] values, bool hasAll)
        {
            return LengthMenu(values, hasAll, "All");
        }

        /// <summary>
        /// Define table length menu.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public GridBuilder<T> LengthMenu(int[] values)
        {
            return LengthMenu(values, false, "All");
        }

        /// <summary>
        /// Define table page length.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GridBuilder<T> PageLength(int value)
        {
            _pageLength = value;

            if (!_lengthMenuValues.Any())
            {
                _lengthMenuValues = new List<int> { value, value * 2, value * 3, value * 4, value * 5 };
                _lengthMenuDisplayedTexts = _lengthMenuValues.Select(x => x.ToString()).ToList();
            }
            return this;
        }

        /// <summary>
        /// Enable or disable the display of a 'processing' indicator when the table is being processed, Default is true.
        /// </summary>
        /// <param name="processing"></param>
        /// <returns></returns>
        public GridBuilder<T> Processing(bool processing)
        {
            _processing = processing;
            return this;
        }

        /// <summary>
        /// Enable horizontal scrolling. When a table is too wide to fit into a certain layout, or you have a large number of columns in the table, you can enable horizontal (x) scrolling to show the table in a viewport, which can be scrolled.
        /// </summary>
        /// <param name="scrollX"></param>
        /// <returns></returns>
        public GridBuilder<T> ScrollX(bool scrollX)
        {
            _scrollX = scrollX;
            return this;
        }

        /// <summary>
        /// Enable or disable state saving such as pagination position, display length, filtering and sorting information.
        /// </summary>
        /// <param name="stateSave"></param>
        /// <returns></returns>
        public GridBuilder<T> StateSave(bool stateSave)
        {
            _stateSave = stateSave;
            return this;
        }

        /// <summary>
        /// Enable selection and set properties
        /// </summary>
        /// <param name="enable">Enable the selectable grid, default is false.</param>
        /// <param name="items">Set the selection behaviour.</param>
        /// <param name="style">Set the selection style.</param>
        /// <param name="info">Disable the visibility of selected row info on grid, default is true.</param>
        /// <param name="toggleable">Disable the toggleable selection, default is true.</param>
        /// <returns></returns>
        public GridBuilder<T> Selecting(bool enable, SelectItems items, SelectStyle style, bool info, bool toggleable)
        {
            _selectEnable = enable;
            _selectItems = items;
            _selectStyle = style;
            _selectInfo = info;
            _selectToggleable = toggleable;

            if (items == SelectItems.Checkbox)
            {
                var column = new ColumnDefinition
                {
                    ClassName = "select-checkbox",
                    Orderable = false,
                    Searchable = false,
                    Width = 5,
                    Render = null
                };
                _columns.Insert(0, column);
            }

            return this;
        }

        /// <summary>
        /// Enable selection and set properties
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="items"></param>
        /// <param name="style"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public GridBuilder<T> Selecting(bool enable, SelectItems items, SelectStyle style, bool info)
        {
            return Selecting(enable, items, style, info, true);
        }

        /// <summary>
        /// Enable selection and set properties
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="items"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public GridBuilder<T> Selecting(bool enable, SelectItems items, SelectStyle style)
        {
            return Selecting(enable, items, style, true, true);
        }

        /// <summary>
        /// Enable selection and set properties
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public GridBuilder<T> Selecting(bool enable, SelectItems items)
        {
            return Selecting(enable, items, SelectStyle.Default, true, true);
        }

        /// <summary>
        /// Enable selection and set properties
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        public GridBuilder<T> Selecting(bool enable)
        {
            return Selecting(enable, SelectItems.Row, SelectStyle.Default, true, true);
        }

        /// <summary>
        /// Enable ColReorder for a table. <see href="https://datatables.net/reference/option/colReorder">Reference:</see>
        /// </summary>
        /// <returns></returns>
        public GridBuilder<T> ColReorder(bool value)
        {
            _colReorder.ColReorder = value;
            return this;
        }

        /// <summary>
        /// Enable and configure the ColReorder extension for DataTables. <see href="https://datatables.net/reference/option/#colreorder">Reference:</see>
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public GridBuilder<T> ColReorder(Action<ColReorderBuilder<T>> config)
        {
            var builder = new ColReorderBuilder<T>(this);
            config(builder);
            return this;
        }
    }
}
