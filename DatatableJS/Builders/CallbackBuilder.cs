namespace DatatableJS
{
    /// <summary>
    /// Generic callback builder class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CallbackBuilder<T>
    {
        private readonly GridBuilder<T> _grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallbackBuilder{T}"/> class.
        /// </summary>
        /// <param name="grid"></param>
        public CallbackBuilder(GridBuilder<T> grid)
        {
            _grid = grid;
        }

        /// <summary>
        /// Set createdRow function <see href="https://datatables.net/reference/option/createdRow">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> CreatedRow(string functionName)
        {
            _grid._callBack.CreatedRow = functionName;
            return this;
        }

        /// <summary>
        /// Set drawCallback function <see href="https://datatables.net/reference/option/drawCallback">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> DrawCallback(string functionName)
        {
            _grid._callBack.DrawCallback = functionName;
            return this;
        }

        /// <summary>
        /// Set footerCallback function <see href="https://datatables.net/reference/option/footerCallback">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> FooterCallback(string functionName)
        {
            _grid._callBack.FooterCallback = functionName;
            return this;
        }

        /// <summary>
        /// Set formatNumber function <see href="https://datatables.net/reference/option/formatNumber">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> FormatNumber(string functionName)
        {
            _grid._callBack.FormatNumber = functionName;
            return this;
        }

        /// <summary>
        /// Set headerCallback function <see href="https://datatables.net/reference/option/headerCallback">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> HeaderCallback(string functionName)
        {
            _grid._callBack.HeaderCallback = functionName;
            return this;
        }

        /// <summary>
        /// Set infoCallback function <see href="https://datatables.net/reference/option/infoCallback">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> InfoCallback(string functionName)
        {
            _grid._callBack.InfoCallback = functionName;
            return this;
        }

        /// <summary>
        /// Set initComplete function <see href="https://datatables.net/reference/option/initComplete">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> InitComplete(string functionName)
        {
            _grid._callBack.InitComplete = functionName;
            return this;
        }

        /// <summary>
        /// Set preDrawCallback function <see href="https://datatables.net/reference/option/preDrawCallback">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> PreDrawCallback(string functionName)
        {
            _grid._callBack.PreDrawCallback = functionName;
            return this;
        }

        /// <summary>
        /// Set rowCallback function <see href="https://datatables.net/reference/option/rowCallback">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> RowCallback(string functionName)
        {
            _grid._callBack.RowCallback = functionName;
            return this;
        }

        /// <summary>
        /// Set stateLoadCallback function <see href="https://datatables.net/reference/option/stateLoadCallback">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> StateLoadCallback(string functionName)
        {
            _grid._callBack.StateLoadCallback = functionName;
            _grid._stateSave = true;
            return this;
        }

        /// <summary>
        /// Set stateLoadParams function <see href="https://datatables.net/reference/option/stateLoadParams">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> StateLoadParams(string functionName)
        {
            _grid._callBack.StateLoadParams = functionName;
            _grid._stateSave = true;
            return this;
        }

        /// <summary>
        /// Set stateLoaded function <see href="https://datatables.net/reference/option/stateLoaded">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> StateLoaded(string functionName)
        {
            _grid._callBack.StateLoaded = functionName;
            _grid._stateSave = true;
            return this;
        }

        /// <summary>
        /// Set stateSaveCallback function <see href="https://datatables.net/reference/option/stateSaveCallback">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> StateSaveCallback(string functionName)
        {
            _grid._callBack.StateSaveCallback = functionName;
            _grid._stateSave = true;
            return this;
        }

        /// <summary>
        /// Set stateSaveParams function <see href="https://datatables.net/reference/option/stateSaveParams">Reference:</see>
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public CallbackBuilder<T> StateSaveParams(string functionName)
        {
            _grid._callBack.StateSaveParams = functionName;
            _grid._stateSave = true;
            return this;
        }
    }
}
