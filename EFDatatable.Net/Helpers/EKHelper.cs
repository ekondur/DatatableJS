using EFDatatable.Net.Helpers.Datatable;
using System;
using System.Reflection;
using System.Web.Mvc;

namespace EFDatatable.Net
{
    public class EKHelper
    {
        private readonly HtmlHelper _htmlHelper;
        public EKHelper(HtmlHelper helper)
        {
            _htmlHelper = helper;
        }

        public GridBuilder<T> GridFor<T>() where T : class
        {
            return new GridBuilder<T>(_htmlHelper);
        }
    }

    public static class EKHelperExtension
    {
        public static EKHelper EK(this HtmlHelper helper)
        {
            return new EKHelper(helper);
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