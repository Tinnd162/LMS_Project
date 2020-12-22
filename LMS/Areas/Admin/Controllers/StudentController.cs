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
        public JsonResult GetStudent(int page, int pageSize)
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
            var subjects = model.Skip((page - 1) * pageSize).Take(pageSize);
            int totalRow = model.Count();
            return Json(new
            {
                total = totalRow,
                data = subjects,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            var info = new InfoStudentDAO().deletestudent(id);
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Detail(string idstudent)
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
                CLASS = new { ID = x.CLASS.ID, NAMECLASS = x.CLASS.NAME, MAJOR = x.CLASS.MAJOR }
            }).ToList();
            return Json(new
            {
                data = model,
                status = true
            });
        }
        public JsonResult GetClassInFaculty(string id)
        {
            var model = new FacultyDAO().getclassinfaculty(id).Select(x => new
            {
                CLASSes = x.CLASSes.Select(a => new { IDCLASS = a.ID, NAMECLASS = a.NAME, MAJORCLASS = a.MAJOR })
            });
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(C_USER infostudent)
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
        public JsonResult GetcoursebyID(string idstudent = "U00006")
        {
            var sub = new InfoStudentDAO().getcoursebyID(idstudent).Select(x => new
            {
                ID = x.ID,
                FIRST_NAME = x.FIRST_NAME,
                MIDDLE_NAME = x.MIDDLE_NAME,
                LAST_NAME = x.LAST_NAME,
                COURSE = x.COURSEs.Select(y => new
                {
                    IDCOURSE = y.ID,
                    NAMECOURSE = y.NAME,
                    DESCRIPTION = y.DESCRIPTION,
                    SUBJECT_ID = y.SUBJECT_ID,
                    SEMESTER = new SEMESTER
                    {
                        ID = y.SEMESTER.ID,
                        TITLE = y.SEMESTER.TITLE,
                    }
                })
            });
            return Json(new
            {
                data = sub,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}