using EFDatatable.Models.Data;
using EFDatatable.Net.Models;
using EFDatatable.Sql;
using EFDatatable.Sql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        // GET: Datatable
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DataTable()
        {
            return View();
        }

        public JsonResult GetDataResult(DataRequest request)
        {
            var result = ctx.Customers.ToDataResult(request);
            return Json(result);
        }

        public JsonResult GetDataList(DataRequest request)
        {
            var result = new DataResult<Customer>();
            result.draw = request.draw;
            result.data = ctx.Customers.ToList();
            result.recordsTotal = result.data.Count;
            result.recordsFiltered = result.data.Count;
            if (request.draw > 0)
            {
                result.data = result.data.Skip(request.start).Take(request.length).ToList();
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetList(DataRequest request)
        {
            var result = new DataResult<Person>();
            result.draw = request.draw;
            result.data = new List<Person> {
                new Person { Id = 1, Name = "Jon Snow" },
                new Person { Id = 2, Name = "Ned Stark" },
                new Person { Id = 3, Name = "Arya Stark" },
                new Person { Id = 4, Name = "Daenerys Targaryen" },
                new Person { Id = 5, Name = "Bran Stark" },
                new Person { Id = 6, Name = "Jamie Lannister" },
                new Person { Id = 7, Name = "Sansa Stark" },
                new Person { Id = 8, Name = "Tyrion Lannister" },
                new Person { Id = 9, Name = "Sandor Clegane" },
                new Person { Id = 10, Name = "Lord Varys" },
                new Person { Id = 11, Name = "Cersei Lannister" }
            };
            result.recordsTotal = result.data.Count;
            result.recordsFiltered = result.data.Count;
            if (request.draw > 0)
            {
                result.data = result.data.Skip(request.start).Take(request.length).ToList();
            }
            return Json(result);
        }
        protected override void Dispose(bool disposing)
        {
            ctx.Dispose();
            base.Dispose(disposing);
        }

    }
}