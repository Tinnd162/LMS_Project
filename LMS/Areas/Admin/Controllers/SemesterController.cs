using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DAL.DAO;
using DAL.EF;

using Newtonsoft.Json;
using LMS.Common;
using System.Text;

namespace LMS.Areas.Admin.Controllers
{
    public class SemesterController : Controller
    {
        LMSProjectDBContext db = new LMSProjectDBContext();
        // GET: Admin/Courses
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetSemester(int page, int pageSize)
        {
            var model = new SemesterDAO().GetSEMESTERs().Select(x => new
            {
                ID = x.ID,
                TITLE = x.TITLE,
                DESCRIPTION = x.DESCRIPTION,
            });
            var subjects = model.Skip((page - 1) * pageSize).Take(pageSize);
            int totalRow = model.Count();
            return Json(new
            {
                total = totalRow,
                data = subjects,
                status = true
            },
            JsonRequestBehavior.AllowGet);
        }
        public JsonResult Detail(string id)
        {
            SEMESTER sem = new SemesterDAO().GetSemesterByID(id);

            var model = new
            {
                ID = sem.ID,
                TITLE = sem.TITLE,
                DESCRIPTION = sem.DESCRIPTION,
                START = sem.START,
                END_SEM = sem.END_SEM
            };
            
           
            return Json(new
            {
                data = model,
                status = true
            });
        }
        public JsonResult Delete(string id)
        {
            if (new SemesterDAO().deletesemester(id))
            {
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(SEMESTER semester)
        {
            bool status = false;
            string message = string.Empty;
            if (semester.ID != "0")
            {
                try
                {
                    var model = new SemesterDAO().updatesemester(semester);
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
                    semester.ID = createID("SEME");
                    db.SEMESTERs.Add(semester);
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
        public string createID(string code)
        {
            var builder = new StringBuilder();
            builder.Append(code + DateTime.Now.ToString("yyyyMMddHHmmssff"));
            return builder.ToString();
        }
        public JsonResult CourseInSemester(string id = "19201")
        {
            var model = new CourseDAO().courseinsemester(id).Select(x => new {
                ID = x.ID,
                NAME = x.NAME,
                DESCRIPTION = x.DESCRIPTION,
                TEACH = new TEACH
                {
                    C_USER = new C_USER
                    {
                        ID = x.TEACH.C_USER.ID,
                        FIRST_NAME = x.TEACH.C_USER.FIRST_NAME,
                        LAST_NAME = x.TEACH.C_USER.LAST_NAME,
                        MIDDLE_NAME = x.TEACH.C_USER.MIDDLE_NAME
                    }
                },
            });
            return Json(new
            {
                data = model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCourseInSemester(string id)
        {
            CourseDAO model = new CourseDAO();
            var course = model.deletecourse(id);
            return Json(new
            {
                status = true
            },
            JsonRequestBehavior.AllowGet);
        }
    }
}