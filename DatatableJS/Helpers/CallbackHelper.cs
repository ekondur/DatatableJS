using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS.Helpers
{
    /// <summary>
    /// Set callback functions. <see href="https://datatables.net/reference/option/">Reference:</see>
    /// </summary>
    [HtmlTargetElement("callbacks", ParentTag = "js-datatable", TagStructure = TagStructure.WithoutEndTag)]
    public class CallbackHelper : TagHelper
    {
        /// <summary>
        /// Set createdRow function <see href="https://datatables.net/reference/option/createdRow">Reference:</see>
        /// </summary>
        public string CreatedRow { get; set; }

        /// <summary>
        /// Set drawCallback function <see href="https://datatables.net/reference/option/drawCallback">Reference:</see>
        /// </summary>
        public string DrawCallback { get; set; }

        /// <summary>
        /// Set footerCallback function <see href="https://datatables.net/reference/option/footerCallback">Reference:</see>
        /// </summary>
        public string FooterCallback { get; set; }

        /// <summary>
        /// Set formatNumber function <see href="https://datatables.net/reference/option/formatNumber">Reference:</see>
        /// </summary>
        public string FormatNumber { get; set; }

        /// <summary>
        /// Set headerCallback function <see href="https://datatables.net/reference/option/headerCallback">Reference:</see>
        /// </summary>
        public string HeaderCallback { get; set; }

        /// <summary>
        /// Set infoCallback function <see href="https://datatables.net/reference/option/infoCallback">Reference:</see>
        /// </summary>
        public string InfoCallback { get; set; }

        /// <summary>
        /// Set initComplete function <see href="https://datatables.net/reference/option/initComplete">Reference:</see>
        /// </summary>
        public string InitComplete { get; set; }

        /// <summary>
        /// Set preDrawCallback function <see href="https://datatables.net/reference/option/preDrawCallback">Reference:</see>
        /// </summary>
        public string PreDrawCallback { get; set; }

        /// <summary>
        /// Set rowCallback function <see href="https://datatables.net/reference/option/rowCallback">Reference:</see>
        /// </summary>
        public string RowCallback { get; set; }

        /// <summary>
        /// Set stateLoadCallback function <see href="https://datatables.net/reference/option/stateLoadCallback">Reference:</see>
        /// </summary>
        public string StateLoadCallback { get; set; }

        /// <summary>
        /// Set stateLoadParams function <see href="https://datatables.net/reference/option/stateLoadParams">Reference:</see>
        /// </summary>
        public string StateLoadParams { get; set; }

        /// <summary>
        /// Set stateLoaded function <see href="https://datatables.net/reference/option/stateLoaded">Reference:</see>
        /// </summary>
        public string StateLoaded { get; set; }

        /// <summary>
        /// Set stateSaveCallback function <see href="https://datatables.net/reference/option/stateSaveCallback">Reference:</see>
        /// </summary>
        public string StateSaveCallback { get; set; }

        /// <summary>
        /// Set stateSaveParams function <see href="https://datatables.net/reference/option/stateSaveParams">Reference:</see>
        /// </summary>
        public string StateSaveParams { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            var grid = CommonHelpers.GetGrid(context);
            grid.Callback.CreatedRow = CreatedRow;
            grid.Callback.InitComplete = InitComplete;
            grid.Callback.DrawCallback = DrawCallback;
            grid.Callback.FooterCallback = FooterCallback;
            grid.Callback.FormatNumber = FormatNumber;
            grid.Callback.HeaderCallback = HeaderCallback;
            grid.Callback.InfoCallback = InfoCallback;
            grid.Callback.PreDrawCallback = PreDrawCallback;
            grid.Callback.RowCallback = RowCallback;
            grid.Callback.StateLoadCallback = StateLoadCallback;
            grid.Callback.StateLoadParams = StateLoadParams;
            grid.Callback.StateLoaded = StateLoaded;
            grid.Callback.StateSaveCallback = StateSaveCallback;
            grid.Callback.StateSaveParams = StateSaveParams;
            grid.StateSave = !string.IsNullOrEmpty(StateLoadCallback) || !string.IsNullOrEmpty(StateLoadParams) 
                || !string.IsNullOrEmpty(StateLoaded) || !string.IsNullOrEmpty(StateSaveCallback) || !string.IsNullOrEmpty(StateSaveParams);
        }
    }
}
