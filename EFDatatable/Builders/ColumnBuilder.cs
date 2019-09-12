using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace EFDatatable.Builders
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
                Data = ExpressionHelper.GetExpressionText(property),
                Title = member.Member.GetCustomAttribute<DisplayAttribute>()?.Name ?? ExpressionHelper.GetExpressionText(property),
                Render = member.Member.GetCustomAttribute<DisplayFormatAttribute>() != null ? $@"moment(data).format('{member.Member.GetCustomAttribute<DisplayFormatAttribute>().DataFormatString}')" : null
            };
            _grid._columns.Add(_column);
            return this;
        }

        public ColumnBuilder<T> Title(string title)
        {
            _column.Title = title;
            return this;
        }

        public ColumnBuilder<T> Visible(bool isVisible)
        {
            _column.Visible = isVisible;
            return this;
        }

        public ColumnBuilder<T> Orderable(bool isOrderable)
        {
            _column.Orderable = isOrderable;
            return this;
        }

        public ColumnBuilder<T> Searchable(bool isSearchable)
        {
            _column.Searchable = isSearchable;
            return this;
        }

        public ColumnBuilder<T> Width(int width)
        {
            _column.Width = width;
            return this;
        }

        public ColumnBuilder<T> Class(string className)
        {
            _column.ClassName = className;
            return this;
        }

        public ColumnBuilder<T> Format(string format)
        {
            _column.Render = $@"moment(data).format('{format}')";
            return this;
        }

        public ColumnBuilder<T> Command<TProp>(Expression<Func<T, TProp>> property, string onClick, string iconClass = "", string btnClass = "", string text = "")
        {
            _column = new ColumnDefinition
            {
                Width = _column.Width == 0 ? 1 : _column.Width,
                Data = ExpressionHelper.GetExpressionText(property),
                Orderable = false,
                Searchable = false,
                Render = $@"'<a href=""#"" class=""{(!string.IsNullOrEmpty(iconClass) && string.IsNullOrEmpty(btnClass) ? "btn btn-xs btn-primary" : btnClass)}"" onClick=""{onClick}(\''+data+'\')"">'+{(string.IsNullOrEmpty(text) ? (string.IsNullOrEmpty(iconClass) ? "data" : "''") : string.Format("'{0}'", text))}+'<i class=""{iconClass}""></i></a>'"
            };
            _grid._columns.Add(_column);
            return this;
        }
    }

    public class ColumnDefinition
    {
        public string Data { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; } = true;
        public bool Searchable { get; set; } = true;
        public bool Orderable { get; set; } = true;
        public int Width { get; set; }
        public string ClassName { get; set; } = "";
        public string Render { get; set; } = "data";
    }
}
