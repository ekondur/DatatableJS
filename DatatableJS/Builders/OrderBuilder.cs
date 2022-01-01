using System;
using System.Linq.Expressions;

namespace DatatableJS
{
    /// <summary>
    /// Generic order builder class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OrderBuilder<T>
    {
        private readonly GridBuilder<T> _grid;
        private OrderModel _order { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderBuilder{T}"/> class.
        /// </summary>
        /// <param name="grid">The grid.</param>
        public OrderBuilder(GridBuilder<T> grid)
        {
            _grid = grid;
        }

        /// <summary>
        /// Adds the specified column for default ordering.
        /// </summary>
        /// <param name="column">The column number.</param>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        public OrderBuilder<T> Add(int column, Order order)
        {
            _order = new OrderModel
            {
                Column = column,
                Order = order
            };
            _grid._defaultOrders.Add(_order);

            return this;
        }
    }
}
