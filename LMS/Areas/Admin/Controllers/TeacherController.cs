using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DAL.DAO;
using DAL.EF;
using DAL.ViewModel;
using Newtonsoft.Json;

namespace LMS.Areas.Admin.Controllers
{
    public class TeacherController : Controller
    {
        LMSProjectDBContext db = new LMSProjectDBContext();
        // GET: Admin/Teacher
        public ActionResult Index()
        {
            return View();
        }
        //Done
        public JsonResult getteacher()
        {
            var model = new InfoTeacherDAO().getteacher().Select(x => new
            {
                ID = x.ID,
                FIRST_NAME = x.FIRST_NAME,
                LAST_NAME=x.LAST_NAME,
                MIDDLE_NAME=x.MIDDLE_NAME,
                PHONE_NO = x.PHONE_NO,
                DoB = x.DoB,
                MAIL = x.MAIL,
                FACULTY = new
                {
                    ID = x.FACULTY.ID,
                    NAME = x.FACULTY.NAME
                }
            });
            return Json(new
            {
                data = model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        //Done
        public JsonResult delete(string id)
        {
            var model = new InfoTeacherDAO().deleteteacher(id);
            return Json(new
            {
                status = true
            });
        }
        //Done
        public JsonResult detail(string idnameteacher)
        {
            var model = new InfoTeacherDAO().detailteacher(idnameteacher).Select(x => new
            {
                ID = x.ID,
                FIRST_NAME = x.FIRST_NAME,
                LAST_NAME=x.LAST_NAME,
                MIDDLE_NAME=x.MIDDLE_NAME,
                PHONE_NO = x.PHONE_NO,
                DoB = x.DoB,
                SEX = x.SEX,
                MAIL = x.MAIL,
                PASSWORD = x.PASSWORD,
                LASTVISITDATE = x.LASTVISITDATE,
                FACULTY = new { ID = x.FACULTY.ID, NAME = x.FACULTY.NAME }
            }).ToList();
            return Json(new
            {
                data = model,
                status=true
            });
        }

        public JsonResult save (C_USER infoteacher)
        {
            bool status = false;
            string message = string.Empty;
            if (infoteacher.ID!=null)
            {
                try
                {
                    var model = new InfoTeacherDAO().updateteacher(infoteacher);
                    status = true;
                }
                catch(Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }
            }
            return Json(new
            {
                status = status,
                message = message
            });
        }
        public JsonResult getfacultyID_NAME()
        {
            var model = new FacultyDAO().getfaculty().Select(x => new { ID = x.ID, NAME = x.NAME });
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getsubbyID(string idteacher)
        {
            var sub = new InfoTeacherDAO().getsubbyID(idteacher).Select(x => new
            {
                ID = x.ID,
                FIRST_NAME = x.FIRST_NAME,
                MIDDLE_NAME = x.MIDDLE_NAME,
                LAST_NAME = x.LAST_NAME,
                SUBJECTs1 = x.SUBJECTs1.Select(y => new { IDSUB = y.ID, NAMESUB = y.NAME, COURSE = new COURSE {ID=y.COURSE.ID ,TILTE = y.COURSE.TILTE } })
            });
            return Json(new
            {
                data = sub,
                status = true
            });
        }
        public JsonResult deletesubbyID(string idsub, string idcourse)
        {
            var model = new SubjectDAO().deletesubbyID(idsub, idcourse);
            return Json(new
            {
                status = true
            });
        }
    }
}