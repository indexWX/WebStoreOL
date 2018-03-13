using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShopDesign.Models;

namespace MyShopDesign.Areas.Admin.Controllers
{
    public class WorkersController : Controller
    {
        MyShopEntities db = new MyShopEntities();
        int pageSize = 4;
        // GET: Admin/Workers
        public ActionResult Index(string searchName, string orderField = "Id desc", int pageIndex = 1)
        {
            IEnumerable<T_Shop_Workers> query = db.T_Shop_Workers;

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

            List<T_Shop_Workers> list = query.ToList();
            ViewBag.list = list;

            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult AddSave()
        {
            return RedirectToAction("index");
        }

        public ActionResult Delete(int Id)
        {
            return RedirectToAction("index");
        }

        public ActionResult Edit(int Id)
        {
            return View();
        }

        public ActionResult EditSave()
        {
            return RedirectToAction("index");
        }

    }
}