using EFDatatable.Builders;
using System;
using System.Reflection;
using System.Web.Mvc;

namespace EFDatatable
{
    public class EFHelper
    {
        private readonly HtmlHelper _htmlHelper;
        public EFHelper(HtmlHelper helper)
        {
            _htmlHelper = helper;
        }

        public GridBuilder<T> GridFor<T>() where T : class
        {
            return new GridBuilder<T>(_htmlHelper);
        }
    }

    public static class EFHelperExtension
    {
        public static EFHelper EF(this HtmlHelper helper)
        {
            return new EFHelper(helper);
        }

        public static string ToLowString(this bool b)
        {
            return b.ToString().ToLower();
        }

        public static T GetAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute
        {
            var attributes = provider.GetCustomAttributes(typeof(T), true);
            return attributes.Length > 0 ? attributes[0] as T : null;
        }
    }
}
