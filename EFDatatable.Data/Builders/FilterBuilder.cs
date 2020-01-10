using System;
using System.Linq.Expressions;

namespace EFDatatable.Data
{
    public class FilterBuilder<T>
    {
        private readonly GridBuilder<T> _grid;
        private FilterDefinition _filter { get; set; }

        public FilterBuilder(GridBuilder<T> grid)
        {
            _grid = grid;
        }

        public FilterBuilder<T> Add<TProp>(Expression<Func<T, TProp>> property)
        {
            _filter = new FilterDefinition
            {
                Field = PropertyName(property),
            };
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

        public FilterBuilder<T> Equal(object value)
        {
            _filter.Operand = Operand.Equal;
            _filter.Value = value.ToString();
            _grid._filters.Add(_filter);
            return this;
        }

        public FilterBuilder<T> NotEqual(object value)
        {
            _filter.Operand = Operand.NotEqual;
            _filter.Value = value.ToString();
            _grid._filters.Add(_filter);
            return this;
        }

        public FilterBuilder<T> GreaterThan(object value)
        {
            _filter.Operand = Operand.GreaterThan;
            _filter.Value = value.ToString();
            _grid._filters.Add(_filter);
            return this;
        }

        public FilterBuilder<T> Contains(object value)
        {
            _filter.Operand = Operand.Contains;
            _filter.Value = value.ToString();
            _grid._filters.Add(_filter);
            return this;
        }

        public FilterBuilder<T> EndsWith(object value)
        {
            _filter.Operand = Operand.EndsWith;
            _filter.Value = value.ToString();
            _grid._filters.Add(_filter);
            return this;
        }

        public FilterBuilder<T> GreaterThanOrEqual(object value)
        {
            _filter.Operand = Operand.GreaterThanOrEqual;
            _filter.Value = value.ToString();
            _grid._filters.Add(_filter);
            return this;
        }

        public FilterBuilder<T> LessThan(object value)
        {
            _filter.Operand = Operand.LessThan;
            _filter.Value = value.ToString();
            _grid._filters.Add(_filter);
            return this;
        }

        public FilterBuilder<T> LessThanOrEqual(object value)
        {
            _filter.Operand = Operand.LessThanOrEqual;
            _filter.Value = value.ToString();
            _grid._filters.Add(_filter);
            return this;
        }

        public FilterBuilder<T> StartsWith(object value)
        {
            _filter.Operand = Operand.StartsWith;
            _filter.Value = value.ToString();
            _grid._filters.Add(_filter);
            return this;
        }
    }

    public class FilterDefinition
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public Operand Operand { get; set; }
    }

    public enum Operand
    {
        Equal,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }
}
