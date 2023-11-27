using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrudForeignKey.Models;

namespace CrudForeignKey.Controllers
{
    public class CategoriesController : Controller
    {
        private FinalDbEntities1 db = new FinalDbEntities1();

       
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

       
       
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
           
        }

       
        public ActionResult Edit(int id)
        {

            Category category = db.Categories.Find(id);
            return View(category);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Category category)
        {
            
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
           
        }

       
        public ActionResult Delete(int id)
        {
            
            Category category = db.Categories.Find(id);
            
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}
