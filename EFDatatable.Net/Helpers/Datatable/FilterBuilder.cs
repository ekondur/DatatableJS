using EFDatatable.Models.Definitions;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace EFDatatable.Net.Helpers.Datatable
{
    public class FilterBuilder<T>
    {
        private readonly GridBuilder<T> _grid;
        private FilterDefinition _filter { get; set; }

        public FilterBuilder(GridBuilder<T> grid)
        {
            _grid = grid;
        }

        public FilterBuilder<T> Add<TProp>(Expression<Func<TProp>> property)
        {
            _filter = new FilterDefinition
            {
                Field = ExpressionHelper.GetExpressionText(property),
            };
            return this;
        }

        public FilterBuilder<T> IsEqual(string value)
        {
            _filter.Operand = "=";
            _filter.Value = value;
            _grid._filters.Add(_filter);
            return this;
        }

        public FilterBuilder<T> IsNotEqual(string value)
        {
            _filter.Operand = "<>";
            _filter.Value = value;
            _grid._filters.Add(_filter);
            return this;
        }
    }
}