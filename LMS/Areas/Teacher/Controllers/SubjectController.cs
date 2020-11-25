using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Areas.Teacher.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Teacher/Subject
        public ActionResult Index(string id= "JZDN2020112521542805")
        {

            return View();
        }
    }
}