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
    public class T_Shop_BasketController : Controller
    {
        private MyShopEntities db = new MyShopEntities();

        public ActionResult addbasketall(string ids)
        {
            string[] id = ids.Split(',');

            foreach (var item in id)
            {
                item.Replace(",", "");
                if (item == "")
                {
                    break;
                }
                else
                {
                    T_Shop_Basket basket = db.T_Shop_Basket.Find(int.Parse(item));

                    T_Shop_Product product = db.T_Shop_Product.Find(basket.ProductId);

                    T_Shop_OFProduct ofproduct = new Models.T_Shop_OFProduct();
                    ofproduct.OrderId = 1;
                    ofproduct.ProductId = product.Id;
                    ofproduct.Count = basket.Count;
                    db.T_Shop_OFProduct.Add(ofproduct);
                    db.SaveChanges();
                }
               
                
            }
            return Redirect("/T_Shop_OFProduct/index");
        }
        // GET: T_Shop_Basket
        public ActionResult Index()
        {
            var t_Shop_Basket = db.T_Shop_Basket.Include(t => t.T_Shop_Product).Include(t => t.T_Shop_Users);
            return View(t_Shop_Basket.ToList());
        }

        // GET: T_Shop_Basket/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Shop_Basket t_Shop_Basket = db.T_Shop_Basket.Find(id);
            if (t_Shop_Basket == null)
            {
                return HttpNotFound();
            }
            return View(t_Shop_Basket);
        }

        // GET: T_Shop_Basket/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.T_Shop_Product, "Id", "Name");
            ViewBag.UserId = new SelectList(db.T_Shop_Users, "Id", "Name");
            return View();
        }

        // POST: T_Shop_Basket/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ProductId,Count,Total,Time")] T_Shop_Basket t_Shop_Basket)
        {
            if (ModelState.IsValid)
            {
                db.T_Shop_Basket.Add(t_Shop_Basket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.T_Shop_Product, "Id", "Name", t_Shop_Basket.ProductId);
            ViewBag.UserId = new SelectList(db.T_Shop_Users, "Id", "Name", t_Shop_Basket.UserId);
            return View(t_Shop_Basket);
        }

        // GET: T_Shop_Basket/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Shop_Basket t_Shop_Basket = db.T_Shop_Basket.Find(id);
            if (t_Shop_Basket == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.T_Shop_Product, "Id", "Name", t_Shop_Basket.ProductId);
            ViewBag.UserId = new SelectList(db.T_Shop_Users, "Id", "Name", t_Shop_Basket.UserId);
            return View(t_Shop_Basket);
        }

        // POST: T_Shop_Basket/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ProductId,Count,Total,Time")] T_Shop_Basket t_Shop_Basket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Shop_Basket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.T_Shop_Product, "Id", "Name", t_Shop_Basket.ProductId);
            ViewBag.UserId = new SelectList(db.T_Shop_Users, "Id", "Name", t_Shop_Basket.UserId);
            return View(t_Shop_Basket);
        }

        // GET: T_Shop_Basket/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Shop_Basket t_Shop_Basket = db.T_Shop_Basket.Find(id);
            if (t_Shop_Basket == null)
            {
                return HttpNotFound();
            }
            return View(t_Shop_Basket);
        }

        // POST: T_Shop_Basket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Shop_Basket t_Shop_Basket = db.T_Shop_Basket.Find(id);
            db.T_Shop_Basket.Remove(t_Shop_Basket);
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
        public ActionResult AddtoOFproduct(int ProductId, int Amount)
        {
            T_Shop_Product product = db.T_Shop_Product.Find(ProductId);
            T_Shop_Basket basket = db.T_Shop_Basket.Find(ProductId);

            T_Shop_OFProduct ofproduct = new Models.T_Shop_OFProduct();
            ofproduct.OrderId = 1;
            ofproduct.ProductId = product.Id;
            ofproduct.Count = Amount;
            db.T_Shop_OFProduct.Add(ofproduct);
            db.SaveChanges();
            return Redirect("/T_Shop_OFProduct/index");
        }


    }
}
