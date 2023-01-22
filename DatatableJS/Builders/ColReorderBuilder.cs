namespace DatatableJS
{
    /// <summary>
    /// Generic col reorder builder class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ColReorderBuilder<T>
    {
        private readonly GridBuilder<T> _grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColReorderBuilder{T}"/> class.
        /// </summary>
        /// <param name="grid"></param>
        public ColReorderBuilder(GridBuilder<T> grid)
        {
            _grid = grid;
        }

        /// <summary>
        /// Initial enablement state of ColReorder. Default value is "true". <see href="https://datatables.net/reference/option/colReorder.enable">Reference:</see> 
        /// </summary>
        /// <param name="value">true</param>
        /// <returns></returns>
        public ColReorderBuilder<T> Enable(bool value)
        {
            _grid._colReorder.Settings = true;
            _grid._colReorder.Enable = value;
            return this;
        }

        /// <summary>
        /// Disallow x columns from reordering (counting from the left). Default is "0". <see href="https://datatables.net/reference/option/colReorder.fixedColumnsLeft">Reference:</see>
        /// </summary>
        /// <param name="value">0</param>
        /// <returns></returns>
        public ColReorderBuilder<T> FixedColumnsLeft(int value)
        {
            _grid._colReorder.Settings = true;
            _grid._colReorder.FixedColumnsLeft = value;
            return this;
        }

        /// <summary>
        /// Disallow x columns from reordering (counting from the right). Default is "0". <see href="https://datatables.net/reference/option/colReorder.fixedColumnsLeft">Reference:</see>
        /// </summary>
        /// <param name="value">0</param>
        /// <returns></returns>
        public ColReorderBuilder<T> FixedColumnsRight(int value)
        {
            _grid._colReorder.Settings = true;
            _grid._colReorder.FixedColumnsRight = value;
            return this;
        }

        /// <summary>
        /// Set a default order for the columns in the table. <see href="https://datatables.net/reference/option/colReorder.order">Reference:</see>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ColReorderBuilder<T> Order(params int[] value)
        {
            _grid._colReorder.Settings = true;
            _grid._colReorder.Order = $"[{string.Join(",", value)}]";
            return this;
        }

        /// <summary>
        /// Enable / disable live reordering of columns during a drag. Default is "true". <see href="https://datatables.net/reference/option/colReorder.realtime">Reference:</see>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ColReorderBuilder<T> RealTime(bool value)
        {
            _grid._colReorder.Settings = true;
            _grid._colReorder.RealTime = value;
            return this;
        }
    }
}
