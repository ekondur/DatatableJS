using System;
using System.Linq;
using System.Linq.Expressions;
using DatatableJS.Net.Exceptions;

namespace DatatableJS.Net
{
    /// <summary>
    /// Generic order builder class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OrderBuilder<T>
    {
        private readonly GridBuilder<T> _grid;
        private OrderDefinition _order { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderBuilder{T}"/> class.
        /// </summary>
        /// <param name="grid">The grid.</param>
        public OrderBuilder(GridBuilder<T> grid)
        {
            _grid = grid;
        }

        /// <summary>
        /// Adds the specified property for default ordering.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="orderBy">The order.</param>
        /// <returns></returns>
        public OrderBuilder<T> Add<TProp>(Expression<Func<T, TProp>> property, OrderBy orderBy)
        {
            var propertyName = ExpressionHelpers<T>.PropertyName(property);

            var column = _grid._columns
                .Where(c => c.Data.Equals(propertyName))
                .FirstOrDefault();

            if (column == null)
            {
                throw new ColumnNotFoundException(string.Format("Column not found for property {0}", propertyName));
            }

            var columnIndex = _grid._columns.IndexOf(column);

            _order = new OrderDefinition
            {
                Field = propertyName,
                Column = columnIndex,
                OrderBy = orderBy
            };
            _grid._orders.Add(_order);

            return this;
        }
    }
}
