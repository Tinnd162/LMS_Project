using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.EF;
using DAL.DAO;
using System.Text;
using System.Web.Script.Serialization;

namespace LMS.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        LMSProjectDBContext db = new LMSProjectDBContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetCourse(int page, int pageSize)
        {

     


            var listcourse = new CourseDAO().GetCOURSEs().Select(x => new
            {
                ID = x.ID,
                NAME = x.NAME,
                DESCRIPTION = x.DESCRIPTION,
                SEMESTER_ID = x.SEMESTER_ID
            });
            var course = listcourse.Skip((page - 1) * pageSize).Take(pageSize);
            int totalRow = listcourse.Count();
            return Json(new
            {
                total = totalRow,
                data = course,
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
                SUBJECT = new SUBJECT { ID = x.SUBJECT.ID, NAME = x.SUBJECT.NAME }
            });
            return Json(new
            {
                data = model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InfoCourse(string idcourse)
        {
            var course = new CourseDAO().InfoTeacherStudentInCourse(idcourse);
            return Json(new
            {
                data = course,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(COURSE Course)
        {
            bool status = false;
            string message = string.Empty;
            if (Course.ID != "0")
            {
                try
                {
                    var model = new CourseDAO().updatecourse(Course);
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
                    db.COURSEs.Add(Course);
                    db.SaveChanges();
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
            var user = new InfoStudentDAO().deletestudent(id);
            return Json(new
            {
                status = true
            });
        }
    }
}