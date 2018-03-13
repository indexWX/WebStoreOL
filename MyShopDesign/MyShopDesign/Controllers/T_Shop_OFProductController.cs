using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyShopDesign.Models;

namespace MyShopDesign.Controllers
{
    public class T_Shop_OFProductController : Controller
    {
        private MyShopEntities db = new MyShopEntities();

        // GET: T_Shop_OFProduct
        public ActionResult Index()
        {
            var t_Shop_OFProduct = db.T_Shop_OFProduct.Include(t => t.T_Shop_OrderForm).Include(t => t.T_Shop_Product);
            return View(t_Shop_OFProduct.ToList());
        }

        // GET: T_Shop_OFProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Shop_OFProduct t_Shop_OFProduct = db.T_Shop_OFProduct.Find(id);
            if (t_Shop_OFProduct == null)
            {
                return HttpNotFound();
            }
            return View(t_Shop_OFProduct);
        }

        // GET: T_Shop_OFProduct/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.T_Shop_OrderForm, "Id", "FahuoId");
            ViewBag.ProductId = new SelectList(db.T_Shop_Product, "Id", "Name");
            return View();
        }

        // POST: T_Shop_OFProduct/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,ProductId,Count,Total")] T_Shop_OFProduct t_Shop_OFProduct)
        {
            if (ModelState.IsValid)
            {
                db.T_Shop_OFProduct.Add(t_Shop_OFProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.T_Shop_OrderForm, "Id", "FahuoId", t_Shop_OFProduct.OrderId);
            ViewBag.ProductId = new SelectList(db.T_Shop_Product, "Id", "Name", t_Shop_OFProduct.ProductId);
            return View(t_Shop_OFProduct);
        }

        // GET: T_Shop_OFProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Shop_OFProduct t_Shop_OFProduct = db.T_Shop_OFProduct.Find(id);
            if (t_Shop_OFProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.T_Shop_OrderForm, "Id", "FahuoId", t_Shop_OFProduct.OrderId);
            ViewBag.ProductId = new SelectList(db.T_Shop_Product, "Id", "Name", t_Shop_OFProduct.ProductId);
            return View(t_Shop_OFProduct);
        }

        // POST: T_Shop_OFProduct/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,ProductId,Count,Total")] T_Shop_OFProduct t_Shop_OFProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Shop_OFProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.T_Shop_OrderForm, "Id", "FahuoId", t_Shop_OFProduct.OrderId);
            ViewBag.ProductId = new SelectList(db.T_Shop_Product, "Id", "Name", t_Shop_OFProduct.ProductId);
            return View(t_Shop_OFProduct);
        }

        // GET: T_Shop_OFProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Shop_OFProduct t_Shop_OFProduct = db.T_Shop_OFProduct.Find(id);
            if (t_Shop_OFProduct == null)
            {
                return HttpNotFound();
            }
            return View(t_Shop_OFProduct);
        }

        // POST: T_Shop_OFProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Shop_OFProduct t_Shop_OFProduct = db.T_Shop_OFProduct.Find(id);
            db.T_Shop_OFProduct.Remove(t_Shop_OFProduct);
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
