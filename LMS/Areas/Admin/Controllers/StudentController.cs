using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL.EF;

namespace LMS.Areas.Admin.Controllers
{
    public class StudentController : Controller
    {
        // GET: Admin/Student
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getstudent()
        {
            var model = new InfoStudentDAO().getstudent().Select(x => new
            {
                ID = x.ID,
                FIRST_NAME = x.FIRST_NAME,
                LAST_NAME = x.LAST_NAME,
                MIDDLE_NAME = x.MIDDLE_NAME,
                PHONE_NO = x.PHONE_NO,
                MAIL = x.MAIL,
                FACULTY = new
                {
                    ID = x.FACULTY.ID,
                    NAME = x.FACULTY.NAME
                },
                CLASS = new
                {
                    ID = x.CLASS.ID,
                    NAME = x.CLASS.NAME,
                    MAJOR = x.CLASS.MAJOR
                }
            });
            return Json(new
            {
                data = model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult delete(string id)
        {
            var info = new InfoStudentDAO().deletestudent(id);
            return Json(new
            {
                status = true
            });
        }
        public JsonResult detail(string idstudent)
        {
            var model = new InfoStudentDAO().detailstudent(idstudent).Select(x => new
            {
                ID = x.ID,
                FIRST_NAME = x.FIRST_NAME,
                LAST_NAME = x.LAST_NAME,
                MIDDLE_NAME = x.MIDDLE_NAME,
                PHONE_NO = x.PHONE_NO,
                DoB = x.DoB,
                SEX = x.SEX,
                MAIL = x.MAIL,
                PASSWORD = x.PASSWORD,
                LASTVISITDATE = x.LASTVISITDATE,
                FACULTY = new { ID = x.FACULTY.ID, NAMEFACULTY = x.FACULTY.NAME },
                CLASS= new {ID=x.CLASS.ID, NAMECLASS=x.CLASS.NAME, MAJOR=x.CLASS.MAJOR}
            }).ToList();
            return Json(new
            {
                data = model,
                status = true
            });
        }
        public JsonResult getclassinfaculty(string id="1")
        {
            var model = new FacultyDAO().getclassinfaculty(id).Select(x => new
            {
                CLASSes = x.CLASSes.Select(a => new { IDCLASS = a.ID, NAMECLASS = a.NAME, MAJORCLASS = a.MAJOR })
            });
            return Json(new { data = model },JsonRequestBehavior.AllowGet);
        }
        public JsonResult save(C_USER infostudent)
        {
            bool status = false;
            string message = string.Empty;
            if (infostudent.ID != null)
            {
                try
                {
                    var model = new InfoStudentDAO().updatestudent(infostudent);
                    status = true;
                }
                catch (Exception ex)
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
        public JsonResult getsubbyID(string idstudent)
        {
            var sub = new InfoStudentDAO().getsubbyID(idstudent).Select(x => new
            {
                ID = x.ID,
                FIRST_NAME = x.FIRST_NAME,
                MIDDLE_NAME = x.MIDDLE_NAME,
                LAST_NAME = x.LAST_NAME,
                SUBJECTs = x.SUBJECTs.Select(y => new { IDSUB = y.ID, NAMESUB = y.NAME, COURSE = new COURSE { ID = y.COURSE.ID, TILTE = y.COURSE.TILTE } })
            });
            return Json(new
            {
                data = sub,
                status = true
            });
        }
    }
}