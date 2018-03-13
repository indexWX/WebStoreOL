using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShopDesign.Models;

namespace MyShopDesign.Areas.Admin.Controllers
{
    public class OrderFormController : Controller
    {
        // GET: Admin/OrderForm
        public ActionResult Index(int Id)
        {
            //区分处理和未处理的订单
            if(Id == 1)
            {

            }
            else
            {

            }

            return View();
        }

        public ActionResult Send(int Id)
        {

            return RedirectToAction("index");
        }
    }
}