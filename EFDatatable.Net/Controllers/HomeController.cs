using EFDatatable.Net.Models;
using EFDatatable.Net.Sql;
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
            var result = ctx.People.ToDataResult(request);
            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            ctx.Dispose();
            base.Dispose(disposing);
        }

    }
}