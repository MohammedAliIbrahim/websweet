using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication13.Models;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;


namespace WebApplication13.Controllers
{
    
    public class HomeController : Controller

    {
        private Database1Entities db = new Database1Entities();

        public ActionResult Index()
        {
            var pie = db.pie.Include(p => p.category);
            return View(pie.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Errorpage()
        {
            

            return View();
        }
    }
}