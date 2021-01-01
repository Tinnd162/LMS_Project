using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Common;
using DAL.DAO;
using DAL.EF;

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
        public JsonResult GetPieData ()
        {
            int male = new InfoTeacherDAO().GetMaleTeacher();
            int female = new InfoTeacherDAO().GetFemaleTeacher();
            int total = male + female;
            float MalePercent = (float)male / total;
            float FemalePercent = (float)female / total;
            return Json(new
            {
                male = Math.Round(MalePercent,2),
                female= Math.Round(FemalePercent, 2)
            }, JsonRequestBehavior.AllowGet);
        }
    }
}