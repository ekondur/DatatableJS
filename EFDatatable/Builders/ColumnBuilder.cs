using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace EFDatatable
{
    public class ColumnBuilder<T>
    {
        private readonly GridBuilder<T> _grid;
        private ColumnDefinition _column { get; set; }

        public ColumnBuilder(GridBuilder<T> grid)
        {
            _grid = grid;
        }

        public ColumnBuilder<T> Field<TProp>(Expression<Func<T, TProp>> property)
        {
            var member = property.Body as MemberExpression;
            _column = new ColumnDefinition
            {
                Data = PropertyName(property),
                Title = member.Member.GetCustomAttribute<DisplayAttribute>()?.Name ?? PropertyName(property),
                Render = member.Member.GetCustomAttribute<DisplayFormatAttribute>() != null ? $@"moment(data).format('{member.Member.GetCustomAttribute<DisplayFormatAttribute>().DataFormatString}')" : null
            };
            _grid._columns.Add(_column);
            return this;
        }

        /// <summary>
        /// Set column title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public ColumnBuilder<T> Title(string title)
        {
            _column.Title = title;
            return this;
        }

        /// <summary>
        /// Set column visible or hidden, default is true
        /// </summary>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        public ColumnBuilder<T> Visible(bool isVisible)
        {
            _column.Visible = isVisible;
            return this;
        }

        /// <summary>
        /// Set column orderable or not, default is true
        /// </summary>
        /// <param name="isOrderable"></param>
        /// <returns></returns>
        public ColumnBuilder<T> Orderable(bool isOrderable)
        {
            _column.Orderable = isOrderable;
            return this;
        }

        /// <summary>
        /// Set column searchable or not, default is true
        /// </summary>
        /// <param name="isSearchable"></param>
        /// <returns></returns>
        public ColumnBuilder<T> Searchable(bool isSearchable)
        {
            _column.Searchable = isSearchable;
            return this;
        }

        /// <summary>
        /// Set column width percentage
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public ColumnBuilder<T> Width(int width)
        {
            _column.Width = width;
            return this;
        }

        /// <summary>
        /// Set css class of column
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public ColumnBuilder<T> Class(string className)
        {
            _column.ClassName = className;
            return this;
        }

        /// <summary>
        /// Set a jquery datatable date format
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public ColumnBuilder<T> Format(string format)
        {
            _column.Render = $@"moment(data).format('{format}')";
            return this;
        }

        /// <summary>
        /// Customize column template using "data" for comparison
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public ColumnBuilder<T> Template(string template)
        {
            _column.Render = template;
            return this;
        }

        /// <summary>
        /// Define a link or button
        /// </summary>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="property"></param>
        /// <param name="onClick"></param>
        /// <param name="iconClass"></param>
        /// <param name="btnClass"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public ColumnBuilder<T> Command<TProp>(Expression<Func<T, TProp>> property, string onClick, string iconClass = "", string btnClass = "", string text = "")
        {
            _column = new ColumnDefinition
            {
                Width = _column.Width == 0 ? 1 : _column.Width,
                Data = PropertyName(property),
                Orderable = false,
                Searchable = false,
                Render = $@"'<a href=""#"" class=""{(!string.IsNullOrEmpty(iconClass) && string.IsNullOrEmpty(btnClass) ? "btn btn-xs btn-primary" : btnClass)}"" onClick=""{onClick}(\''+data+'\')"">'+{(string.IsNullOrEmpty(text) ? (string.IsNullOrEmpty(iconClass) ? "data" : "''") : string.Format("'{0}'", text))}+'<i class=""{iconClass}""></i></a>'"
            };
            _grid._columns.Add(_column);
            return this;
        }

        private static string PropertyName<TProp>(Expression<Func<T, TProp>> expression)
        {
            var body = expression.Body as MemberExpression;

            if (body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            return body.Member.Name;
        }
    }
}
