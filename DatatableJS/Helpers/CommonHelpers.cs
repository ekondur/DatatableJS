using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Globalization;

namespace DatatableJS
{
    public static class CommonHelpers
    {
        public static string ToLowString(this bool b)
        {
            return b.ToString().ToLower(CultureInfo.InvariantCulture);
        }

        public static GridModel GetGrid(TagHelperContext context)
        {
            context.Items.TryGetValue("DataGrid", out object model);
            return (GridModel)model;
        }
    }
}
