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
using LMS.Common;
using System.Text;

namespace LMS.Areas.Admin.Controllers
{
    public class CoursesController : Controller
    {
        LMSProjectDBContext db = new LMSProjectDBContext();
        // GET: Admin/Courses
        public ActionResult Index()
        {
            return View();
        }
        //Done
        public JsonResult getcourse(int page, int pageSize=2)
        {
            var model = new CourseDAO().GetCOURSEs().Select(x => new {
                ID = x.ID,
                TILTE = x.TILTE,
                DESCRIPTION = x.DESCRIPTION
            }).Skip((page-1)*pageSize).Take(pageSize);
            int totalRow = model.Count();
            return Json(new
            {
                data = model,
                total=totalRow,
                status = true
            },
            JsonRequestBehavior.AllowGet);
        }
        //Done
        public JsonResult detail(string id)
        {
            var model = new CourseDAO().getdetail(id).Select(x => new {
                ID = x.ID,
                TILTE = x.TILTE,
                DESCRIPTION = x.DESCRIPTION
            });
            return Json(new
            {
                data = model,
                status = true
            });
        }
        //Done
        public JsonResult delete(string id)
        {
            var model = new CourseDAO().deletecourse(id);
            return Json(new
            {
                status = true
            },
            JsonRequestBehavior.AllowGet);
        }
        //Done
        public JsonResult save(string strCourse)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            COURSE course = serializer.Deserialize<COURSE>(strCourse);
            bool status = false;
            string message = string.Empty;
            if (course.ID != "0")
            {
                try
                {
                    var model = new CourseDAO().updatecourse(course);
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
                    course.ID = createID("COUR");
                    db.COURSEs.Add(course);
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
        //Done
        public string createID(string code)
        {
            var builder = new StringBuilder();
            builder.Append(code + DateTime.Now.ToString("yyyyMMddHHmmssff"));
            return builder.ToString();
        }
        //Done
        public JsonResult subjectIncourse(string id= "JZDN2020112521542821")
        {
            var model = new SubjectDAO().subjectIncourse(id).Select(x => new {
                ID = x.ID,
                NAME = x.NAME,
                DESCRIPTION = x.DESCRIPTION,
                C_USER1 = x.C_USER1.Select(b => new {
                    ID = b.ID,
                    FIRST_NAME = b.FIRST_NAME,
                    LAST_NAME=b.LAST_NAME,
                    MIDDLE_NAME=b.MIDDLE_NAME,
                })
            });
            return Json(new
            {
                data = model,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        //Done
        public JsonResult deleteSubjectInCourse(string id)
        {
            SubjectDAO model = new SubjectDAO();
            var course = model.deletesubject(id);
            return Json(new
            {
                status = true
            },
            JsonRequestBehavior.AllowGet);
        }
    }
}