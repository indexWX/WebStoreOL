using MyShopDesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShopDesign.Controllers
{
    public class ProductViewController : Controller
    {
        MyShopDesign.Models.MyShopEntities db = new Models.MyShopEntities();
        // GET: ProductView
        public ActionResult Index(int pageIndex = 1)
        {
            IEnumerable<T_Shop_Product> query = db.T_Shop_Product;
            query = query.Where(m => m.Month > 1000);
            List<T_Shop_Product> lst = query.ToList();
            ViewBag.lst = lst;
            return View();
        }
        public ActionResult ProductAll(string searchstring, string orderField = "Id desc", int pageIndex = 1)
        {
            IEnumerable<T_Shop_Product> query = db.T_Shop_Product;
            int pageSize = 6;
            #region 查询逻辑
            if (!String.IsNullOrEmpty(searchstring))
            {
                query = query.Where(m => m.Name.Contains(searchstring));
            }
            #endregion

            switch (orderField)
            {
                case "Month desc":
                    query = query.OrderByDescending(m => m.Month);
                    break;
                case "Price desc":
                    query = query.OrderBy(m => m.Price);
                    break;
                case "Id desc":
                    query = query.OrderByDescending(m => m.Id);
                    break;
                default:
                    break;
            }
            int recordCount = query.Count();
            #region 读取指定页数据
            //query = query.Take(4);
            //query = query.Skip(4).Take(4);
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            #endregion
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.recordCount = recordCount;

            List<T_Shop_Product> lst = query.ToList();
            ViewBag.lst = lst;
            return View();

        }
        public ActionResult Productkind(int id = 8, string orderField = "Id desc")
        {
            IEnumerable<T_Shop_ProductCategory> query = db.T_Shop_ProductCategory;
            List<T_Shop_ProductCategory> lst = query.ToList();

            IEnumerable<T_Shop_Product> query2 = db.T_Shop_Product;
            query2 = query2.Where(m => m.CategoryId == id);

            switch (orderField)
            {
                case "Month desc":
                    query2 = query2.OrderByDescending(m => m.Month);
                    break;
                case "Price desc":
                    query2 = query2.OrderBy(m => m.Price);
                    break;
                case "Id desc":
                    query2 = query2.OrderByDescending(m => m.Id);
                    break;
                default:
                    break;
            }


            List<T_Shop_Product> productList = query2.ToList();
            ViewBag.lst = lst;
            ViewBag.productList = productList;
            ViewBag.orderId = id;
            return View();
        }
        public ActionResult Detail(int id)
        {
            T_Shop_Product item = db.T_Shop_Product.Find(id);
            ViewBag.item = item;
            return View();
        }
        public ActionResult Basket(int id)
        {
            return View();
        }
        public ActionResult AddtoBasket(int ProductId, int Amount)
        {
            T_Shop_Product product = db.T_Shop_Product.Find(ProductId);
            T_Shop_Basket basket = new Models.T_Shop_Basket();
            basket.Total = product.Price;
            basket.UserId = 1;
            basket.ProductId = product.Id;
            basket.Count = Amount;
            db.T_Shop_Basket.Add(basket);
            db.SaveChanges();
            return Redirect("/T_Shop_Basket/index");
        }
        public ActionResult AddtoOFproduct(int ProductId, int Amount)
        {
            T_Shop_Product product = db.T_Shop_Product.Find(ProductId);
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