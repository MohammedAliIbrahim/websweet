using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication13.Models;

namespace WebApplication13.Controllers
{
    
    public class rolesController : Controller
    {

        ApplicationDbContext db1 = new ApplicationDbContext();
        // GET: roles
        public ActionResult Index()
        {
            return View(db1.Roles.ToList());
        }

        // GET: roles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
           var role = db1.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db1.Roles.Add(role);
                db1.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = db1.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: roles/Edit/5
        [HttpPost]
        public ActionResult Edit(IdentityRole role)
        {

            if (ModelState.IsValid)
            {
                db1.Entry(role).State = EntityState.Modified;
                db1.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = db1.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: roles/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {

           var myrole = db1.Roles.Find(role.Id);
            db1.Roles.Remove(myrole);
            db1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
