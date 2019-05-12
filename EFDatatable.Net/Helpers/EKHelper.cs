using EFDatatable.Net.Helpers.Datatable;
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
}