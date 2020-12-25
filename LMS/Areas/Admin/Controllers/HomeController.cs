using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Common;

namespace LMS.Areas.Admin.Controllers
{
    [CustomAuthorize("ADMIN")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            CommonFunc cFunc = new CommonFunc();
            return View();
        }
    }
}