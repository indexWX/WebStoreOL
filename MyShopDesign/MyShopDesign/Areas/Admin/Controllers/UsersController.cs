using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShopDesign.Models;

namespace MyShopDesign.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        MyShopEntities db = new MyShopEntities();
        int pageSize = 4;
        // GET: Admin/Users
        public ActionResult Index(string searchName, string orderField = "Id desc", int pageIndex = 1)
        {
            IEnumerable<T_Shop_Users> query = db.T_Shop_Users;

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

            List<T_Shop_Users> list = query.ToList();
            ViewBag.list = list;

            return View();
        }

        public ActionResult ChangeState(int Id)
        {
            T_Shop_Users item = db.T_Shop_Users.Find(Id);
            if (item.State == 1)
                item.State = 2;
            else
                item.State = 1;
            db.SaveChanges();

            return RedirectToAction("index");
        }

        public ActionResult ResetPassword(int Id)
        {
            T_Shop_Users item = db.T_Shop_Users.Find(Id);
            item.Password = "1234";
            db.SaveChanges();

            return RedirectToAction("index");
        }
    }
}