using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication13.Models;
using System.IO;
using ImageTools;
using System.Drawing;

namespace WebApplication13.Controllers
{
    
    [Authorize]
    public class piesController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: pies
        [AllowAnonymous]
        public ActionResult Index()
        
        {

            var pie = db.pie.Include(p => p.category);
            return View(pie.ToList());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(string NameSearch)

        {

            var sweets = db.pie.Include(b => b.category);

            if (NameSearch != null && NameSearch != " ")

            {
                sweets = db.pie.Where(b => b.name.Contains(NameSearch));



            }







            //books = db.books.Include(b => b.category);
            return View(sweets.ToList());
        }
        [AllowAnonymous]
        public JsonResult GetStudents(string term)
        {
           
            List<string> students = db.pie.Where(s => s.name.StartsWith(term))
                .Select(x => x.name).ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }

        // GET: pies/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pie pie = db.pie.Find(id);
            if (pie == null)
            {
                return HttpNotFound();
            }
            return View(pie);
        }

        // GET: pies/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.catid = new SelectList(db.category, "Id", "name");
            return View();
        }

        // POST: pies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "Id,name,details,prce,imageurl,upload,catid")] pie pie)
        {
            
            if (ModelState.IsValid)
            {
                if (pie.upload != null)
                {
                    if (pie.upload.ContentType.Contains("image") && pie.upload.ContentLength <= (50 * 1024))  
                    {

                        pie.imageurl = Guid.NewGuid().ToString() + "" + Path.GetExtension(pie.upload.FileName);
                        string path = Path.Combine(Server.MapPath("~/img/"), pie.imageurl);
                     
                        pie.upload.SaveAs(path);
                        //Image newImage = Image.FromFile(path);
                        //ImageReducer.SaveJpeg("~/img/", newImage, 100);
                    }
                    else
                    {
                        TempData["notice"] = "Selected file should be an iamge and its size should be less than 3Μ";
                    }
                }
                
                db.pie.Add(pie);
                //try
                //{
                //    db.SaveChanges();
                //}
                //catch
                //{
                //    Response.Write("please enter the necsseray fields");
                //    return View();
                //}
                db.SaveChanges();
                TempData["messege"] = "done successfully";
                    /*string.Format(" {0} has been saved ", pie.name);*/
                return RedirectToAction("Index");
            }

            ViewBag.catid = new SelectList(db.category, "Id", "name", pie.catid);
            return View(pie);
        }

        // GET: pies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pie pie = db.pie.Find(id);
            if (pie == null)
            {
                return HttpNotFound();
            }
            ViewBag.catid = new SelectList(db.category, "Id", "name", pie.catid);
            return View(pie);
        }

        // POST: pies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,details,prce,imageurl,catid")] pie pie , HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    string oldpath = Path.Combine(Server.MapPath("~/img/"), pie.imageurl);

                    if (upload != null)
                    {

                        System.IO.File.Delete(oldpath);
                        pie.imageurl = Guid.NewGuid().ToString() + "" + Path.GetExtension(upload.FileName);
                        string path = Path.Combine(Server.MapPath("~/img/"), pie.imageurl);
                        upload.SaveAs(path);


                    }
                }
                catch
                {
                    if (upload != null)
                    {

                        //System.IO.File.Delete(oldpath);
                        pie.imageurl = Guid.NewGuid().ToString() + "" + Path.GetExtension(upload.FileName);
                        string path = Path.Combine(Server.MapPath("~/img/"), pie.imageurl);
                        upload.SaveAs(path);
                        //Image newImage = Image.FromFile(path);
                        //ImageReducer.SaveJpeg(path, newImage, 1500);


                    }
                }
                
             
                db.Entry(pie).State = EntityState.Modified;
                //try
                //{
                    db.SaveChanges();
                //}
                //catch
                //{
                //    Response.Write("please enter the necsseray fields");
                //}
                return RedirectToAction("Index");
            }
            ViewBag.catid = new SelectList(db.category, "Id", "name", pie.catid);
            return View(pie);
        }

        // GET: pies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pie pie = db.pie.Find(id);
            if (pie == null)
            {
                return HttpNotFound();
            }
            return View(pie);
        }

        // POST: pies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pie pie = db.pie.Find(id);
            string fullPath = Request.MapPath("~/img/" +pie.imageurl );
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            

            db.pie.Remove(pie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
