using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrudForeignKey.Models;
using PagedList;
using PagedList.Mvc;

namespace CrudForeignKey.Controllers
{
    public class ProductsController : Controller
    {
        private FinalDbEntities1 db = new FinalDbEntities1();

       
        public ActionResult Index(int? page)
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Category1).OrderBy(p => p.ProductID);
            return View(products.ToPagedList(page ?? 1, 3));
        }

       
        
        public ActionResult Create()
        {   

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryId");
            ViewBag.CategoryName = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {

            
                db.Products.Add(product);
                db.SaveChanges();              
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewBag.CategoryName = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryName);

                return RedirectToAction("Index");
            
            
        }

       
        public ActionResult Edit(int id)
        {
            
            Product product = db.Products.Find(id);           
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewBag.CategoryName = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryName);
            return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Product product)
        {
            
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();              
             ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewBag.CategoryName = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryName);
            return RedirectToAction("Index");
        }

      
       public ActionResult Delete(int id)
        {
           
            Product product = db.Products.Find(id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
