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
    }
}