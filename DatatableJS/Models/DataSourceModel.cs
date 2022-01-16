namespace DatatableJS
{
    /// <summary>
    /// DataSourceModel
    /// </summary>
    public class DataSourceModel
    {
        internal string URL { get; set; }
        internal HttpMethodType Method { get; set; }
        internal bool Paging { get; set; }
        internal string Data { get; set; }
        internal bool ServerSide { get; set; }
        internal int? PageLength { get; set; }
    }

    /// <summary>
    /// HttpMethodType
    /// </summary>
    public enum HttpMethodType
    {
        /// <summary>
        /// GET
        /// </summary>
        GET,
        /// <summary>
        /// POST
        /// </summary>
        POST
    }
}
