using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShopDesign.Models;

namespace MyShopDesign.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        //GET: Admin/Category
       MyShopEntities db = new MyShopEntities();
        int pageSize = 4;

        public ActionResult Index(string searchName, string orderField = "Id desc", int pageIndex = 1)
        {
            IEnumerable<T_Shop_ProductCategory> query = db.T_Shop_ProductCategory;

            if (string.IsNullOrEmpty(searchName))
            {

            }
            else
            {
                query = query.Where(m => m.Name.Contains(searchName));
            }

            #region 排序逻辑
            // orderField

            switch (orderField)
            {
                case "title":
                    query = query.OrderBy(m => m.Name);
                    break;
                case "Id desc":
                    query = query.OrderByDescending(m => m.Id);
                    break;
                default:
                    break;
            }
            #endregion

            #region 分页实现
            int recordCount = query.Count();
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.recordCount = recordCount;
            #endregion

            List<T_Shop_ProductCategory> list = query.ToList();
            ViewBag.list = list;
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(T_Shop_ProductCategory category)
        {
            T_Shop_ProductCategory temp = new T_Shop_ProductCategory();
            temp.Name = category.Name;
            temp.Description = category.Description;
            db.T_Shop_ProductCategory.Add(temp);
            db.SaveChanges();

            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            T_Shop_ProductCategory temp = db.T_Shop_ProductCategory.Find(id);
            db.T_Shop_ProductCategory.Remove(temp);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            T_Shop_ProductCategory temp = db.T_Shop_ProductCategory.Find(id);
            ViewBag.item = temp;
            return View();
        }

        [HttpPost]

        public ActionResult EditSave(string Name, string Description, int Id)
        {
            T_Shop_ProductCategory item = db.T_Shop_ProductCategory.Find(Id);
            item.Name = Name;
            item.Description = Description;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}