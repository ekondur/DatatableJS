using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.ComponentModel;
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

        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
    }
}
