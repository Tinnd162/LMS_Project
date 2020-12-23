using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Common;

namespace LMS.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            CommonFunc cFunc = new CommonFunc();
            try
            {
                if (cFunc.CheckSectionInvalid() == false || cFunc.checkRole("ADMIN") == false)
                {
                    if (cFunc.GetSession() == null)
                    {
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    else return RedirectToAction("Error", "Home", new { area = "" });
                }
            }
            catch
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
            return View();
        }
    }
}