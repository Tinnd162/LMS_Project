using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.EF;
using DAL.DAO;
using System.Text;
using System.Web.Script.Serialization;
using LMS.Common;

namespace LMS.Areas.Admin.Controllers
{
    [CustomAuthorize("ADMIN")]
    public class CourseController : Controller
    {
        LMSProjectDBContext db = new LMSProjectDBContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetCourse(string name, int page, int pageSize)
        {
            var ListCourse = new CourseDAO().GetCOURSEs().Select(x => new
            {
                ID = x.ID,
                NAME = x.NAME,
                DESCRIPTION = x.DESCRIPTION,
                SUBJECT = new SUBJECT { FACULTY_ID=x.SUBJECT.FACULTY_ID}
            });
            if (!string.IsNullOrEmpty(name))
            {
                ListCourse = ListCourse.Where(x => x.NAME.Contains(name));
            }
            int TotalRow = ListCourse.Count();
            var lstCourse = ListCourse.Skip((page - 1) * pageSize).Take(pageSize);
            return Json(new
            {
                total = TotalRow,
                data = lstCourse,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            var model = new CourseDAO().deletecourse(id);
            return Json(new
            {
                status = true
            });
        }
        public JsonResult GetNameSemester()
        {
            var model = new SemesterDAO().getsemester().Select(x => new { ID = x.ID, TITLE = x.TITLE });
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNameSubject()
        {
            var model = new SubjectsDAO().getsubject().Select(x => new { ID = x.ID, NAME = x.NAME });
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }
        public string createID(string code)
        {
            var builder = new StringBuilder();
            builder.Append(code + DateTime.Now.ToString("yyyyMMddHHmmssff"));
            return builder.ToString();
        }
        public JsonResult Detail(string id)
        {
            var model = new CourseDAO().getdetail(id).Select(x => new {
                IDCOURSE = x.ID,
                NAMECOURSE = x.NAME,
                DESCRIPTION = x.DESCRIPTION,
                SEMESTER = new SEMESTER { ID = x.SEMESTER.ID, TITLE = x.SEMESTER.TITLE },
                SUBJECT = new SUBJECT { ID = x.SUBJECT.ID, NAME = x.SUBJECT.NAME },
                TEACH = new TEACH
                {
                    C_USER = new C_USER
                    {
                        ID = x.TEACH.C_USER.ID,
                        FIRST_NAME = x.TEACH.C_USER.FIRST_NAME,
                        LAST_NAME = x.TEACH.C_USER.LAST_NAME,
                        MIDDLE_NAME = x.TEACH.C_USER.MIDDLE_NAME,
                        FACULTY = new FACULTY { ID = x.TEACH.C_USER.FACULTY.ID, NAME = x.TEACH.C_USER.FACULTY.NAME }
                    }
                },
            });
            return Json(new
            {
                data = model,
                status = true
            });
        }
        public JsonResult InfoCourse(string idcourse)
        {
            var course = new CourseDAO().InfoTeacherStudentInCourse(idcourse);
            return Json(new
            {
                data = course,
                status = true
            });
        }
        public JsonResult Save(COURSE Course, TEACH Teach)
        {
            bool status = false;
            string message = string.Empty;
            if (Course.ID != "0")
            {
                try
                {
                    var model = new TeachDAO().updateteach(Teach);
                    var course = new CourseDAO().updatecourse(Course);
                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }
            }
            else
            {
                try
                {
                    Course.ID = createID("COUR");
                    var course = new CourseDAO().addcourse(Course);
                    var teach = new TeachDAO().addteach(Teach, Course);
                    status = true;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
            return Json(new
            {
                status = status,
                message = message
            });
        }
        public JsonResult DeleteStudent(string id)
        {
            var delstudent = new InfoStudentDAO().deletestudent(id);
            return Json(new
            {
                status = true
            });
        }
        public JsonResult GetTeacherInFaculy(string id)
        {
            var model = new FacultyDAO().getteacherinfaculty(id).Select(x => new
            {
                C_USER = x.C_USER.Select(a => new { IDTEA=a.ID, FIRST_NAME=a.FIRST_NAME, LAST_NAME=a.LAST_NAME, MIDDLE_NAME=a.MIDDLE_NAME})
            });
            return Json(new { data = model });
        }
    }
}