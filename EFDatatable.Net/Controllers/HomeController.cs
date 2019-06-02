using EFDatatable.Models.Data;
using EFDatatable.Net.Models;
using EFDatatable.Sql;
using System.Web.Mvc;

namespace EFDatatable.Net.Controllers
{
    public class HomeController : Controller
    {
        EFContext ctx;
        public HomeController()
        {
            ctx = new EFContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DataTable()
        {
            return View();
        }

        public JsonResult GetDataResult(DataRequest request, AddData data)
        {
            var result = ctx.Customers.ToDataResult(request);
            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            ctx.Dispose();
            base.Dispose(disposing);
        }

    }
}