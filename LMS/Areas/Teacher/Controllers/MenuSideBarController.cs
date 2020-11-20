using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Areas.Teacher.Controllers
{
    public class MenuSideBarController : Controller
    {
        // GET: Teacher/MenuSideBar
        [ChildActionOnly]
        public ActionResult Index()
        {
            return View();
        }
    }
}