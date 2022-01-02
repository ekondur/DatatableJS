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
        /// Adds the specified column for default ordering.
        /// </summary>
        /// <param name="column">The column number.</param>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        public OrderBuilder<T> Add(int column, Order order)
        {
            _order = new OrderDefinition
            {
                Column = column,
                Order = order
            };
            _grid._orders.Add(_order);

            return this;
        }
    }

}
