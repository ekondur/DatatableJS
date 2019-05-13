using EFDatatable.Models.Data;
using EFDatatable.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFDatatable.Net.Controllers
{
    public class HomeController : Controller
    {
        // GET: Datatable
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DataTable()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList(DataRequest request)
        {
            var result = new DataResult<Person>();
            result.draw = request.draw;
            result.data = new List<Person> { new Person { Id = 1, Name = "Jon Snow" } };
            result.recordsFiltered = 1;
            result.recordsTotal = 1;
            return Json(result);
        }
    }
}