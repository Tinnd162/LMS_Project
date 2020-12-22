using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL.EF;

namespace LMS.Areas.Admin.Controllers
{
    public class SubjectsController : Controller
    {
        LMSProjectDBContext db = new LMSProjectDBContext();
        // GET: Admin/Subjects
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetSubjects(int page, int pageSize)
        {
            var sub = new SubjectsDAO().getsubject().Select(a => new
            {
                ID = a.ID,
                NAME = a.NAME,
                DESCRIPTION = a.DESCRIPTION
            });
            var subjects = sub.Skip((page - 1) * pageSize).Take(pageSize);
            int totalRow = sub.Count();
            return Json(new
            {
                total = totalRow,
                data = subjects,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            var model = new SubjectsDAO().delete(id);
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Detail(string id)
        {
            var model = new SubjectsDAO().getdetail(id).Select(x => new {
                ID = x.ID,
                NAME = x.NAME,
                DESCRIPTION = x.DESCRIPTION,
            });
            return Json(new
            {
                data = model,
                status = true
            });
        }
        public JsonResult Save(SUBJECT subjects)
        {
            bool status = false;
            string message = string.Empty;
            if (subjects.ID != "0")
            {
                try
                {
                    var model = new SubjectsDAO().update(subjects);
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
                    subjects.ID = createID("SUBJ");
                    db.SUBJECTs.Add(subjects);
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
        public JsonResult GetCourseInSubject(string idsub)
        {
            var model = new SubjectsDAO().getcourseinsubjects(idsub).Select(a => new
            {
                ID = a.ID,
                NAME = a.NAME,
                COURSE = a.COURSEs.Select(b => new
                {
                    IDCOURSE = b.ID,
                    NAMECOURSE = b.NAME,
                    DESCRIPTION = b.DESCRIPTION,
                    TEACH = new TEACH
                    {
                        C_USER = new C_USER
                        {
                            ID = b.TEACH.C_USER.ID,
                            FIRST_NAME = b.TEACH.C_USER.FIRST_NAME,
                            LAST_NAME = b.TEACH.C_USER.LAST_NAME,
                            MIDDLE_NAME = b.TEACH.C_USER.MIDDLE_NAME
                        }
                    },
                })
            });

            return Json(new
            {
                data = model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}