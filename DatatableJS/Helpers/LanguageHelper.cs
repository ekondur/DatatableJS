using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatatableJS
{
    /// <summary>
    /// Language configuration options for DataTables.
    /// </summary>
    [HtmlTargetElement("language", ParentTag = "js-datatable")]
    public class LanguageHelper : TagHelper
    {
        /// <summary>
        /// Set language json source URL.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Set the decimal place character. <see href="https://datatables.net/reference/option/language.decimal">Reference:</see>
        /// </summary>
        public string Decimal { get; set; }

        /// <summary>
        /// This string is shown when the table is empty of data (regardless of filtering). <see href="https://datatables.net/reference/option/language.emptyTable">Reference:</see>
        /// </summary>
        public string EmptyTable { get; set; }

        /// <summary>
        /// This string gives information to the end user about the information that is current on display on the page. <see href="https://datatables.net/reference/option/language.info">Reference:</see>
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Display information string for when the table is empty. <see href="https://datatables.net/reference/option/language.infoEmpty">Reference:</see>
        /// </summary>
        public string InfoEmpty { get; set; }

        /// <summary>
        /// When a user filters the information in a table, this string is appended to the information (info) to give an idea of how strong the filtering is. <see href="https://datatables.net/reference/option/language.infoFiltered">Reference:</see>
        /// </summary>
        public string InfoFiltered { get; set; }

        /// <summary>
        /// If can be useful to append extra information to the info string at times, and this variable does exactly that. <see href="https://datatables.net/reference/option/language.infoPostFix">Reference:</see>
        /// </summary>
        public string InfoPostFix { get; set; }

        /// <summary>
        /// The thousands separator option is used for output of information only. <see href="https://datatables.net/reference/option/language.thousands">Reference:</see>
        /// </summary>
        public string Thousands { get; set; }

        /// <summary>
        /// Detail the action that will be taken when the drop down menu for the pagination length option is changed. <see href="https://datatables.net/reference/option/language.lengthMenu">Reference:</see>
        /// </summary>
        public string LengthMenu { get; set; }

        /// <summary>
        /// This message is shown in an empty row in the table to indicate to the end user the the data is being loaded. <see href="https://datatables.net/reference/option/language.loadingRecords">Reference:</see>
        /// </summary>
        public string LoadingRecords { get; set; }

        /// <summary>
        /// Text that is displayed when the table is processing a user action (usually a sort command or similar). <see href="https://datatables.net/reference/option/language.processing">Reference:</see>
        /// </summary>
        public string Processing { get; set; }

        /// <summary>
        /// Sets the string that is used for DataTables filtering input control. <see href="https://datatables.net/reference/option/language.search">Reference:</see>
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Text shown inside the table records when the is no information to be displayed after filtering. <see href="https://datatables.net/reference/option/language.zeroRecords">Reference:</see>
        /// </summary>
        public string ZeroRecords { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            var language = CommonHelpers.GetGrid(context).Language;
            language.URL = URL;
            language.Decimal = Decimal;
            language.EmptyTable = EmptyTable;
            language.Info = Info;
            language.InfoEmpty = InfoEmpty;
            language.InfoFiltered = InfoFiltered;
            language.InfoPostFix = InfoPostFix;
            language.Thousands = Thousands;
            language.LengthMenu = LengthMenu;
            language.LoadingRecords = LoadingRecords;
            language.Processing = Processing;
            language.Search = Search;
            language.ZeroRecords = ZeroRecords;
        }
    }
}
