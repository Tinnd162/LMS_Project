using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.EF;
using DAL.DAO;
using DAL.ViewModel;
using System.Text;
using System.Web.Script.Serialization;

namespace LMS.Areas.Admin.Controllers
{
    public class SubjectsController : Controller
    {
        LMSProjectDBContext db = new LMSProjectDBContext();
  
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult getsubject()
        {
            var listsubject = new SubjectDAO().getsubject().Select(x => new { ID = x.ID, NAME = x.NAME , DESCRIPTION=x.DESCRIPTION});
            return Json(new
            {
                data = listsubject,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getNAMEcourse()
        {
            var model = new CourseDAO().GetCOURSEs().Select(x=> new { ID=x.ID, TILTE=x.TILTE});
            return Json( new 
            { 
                data = model
            },JsonRequestBehavior.AllowGet);
        }

        //Hàm tạo ID tự động
        public string createID(string code)
        {
            var builder = new StringBuilder();
            builder.Append(code + DateTime.Now.ToString("yyyyMMddHHmmssff"));
            return builder.ToString();
        }
        [HttpPost]
        public JsonResult delete(string id)
        {
            SubjectDAO model = new SubjectDAO();
            var subject=model.deletesubject(id);
            return Json(new { status = true });
        }
         public JsonResult detail(string id)
        {
            var model = new SubjectDAO().getdetail(id);
            return Json(new
            {
                data = model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult save(SubjectModel subject)
        {
            bool status = false;
            string message = string.Empty;
            if (subject.ID != "0")
            {
                SubjectDAO model = new SubjectDAO();
                model.updatesubject(subject.ID, subject.NAME, subject.DESCRIPTION, subject.COURSE_ID);
                status = true;
            }
            else
            {
                subject.ID = createID("SUBJ");
                SubjectDAO model = new SubjectDAO();
                model.insertsubject(subject.ID, subject.NAME, subject.DESCRIPTION, subject.COURSE_ID);
                status = true;
            }
            return Json(new
            {
                status = status,
                message = message
            });
        }
        public JsonResult infosubject(string idsub)
        {
            var sub = new SubjectDAO().Info_Teacher_Student_Subject(idsub);
            return Json(new
            {
                data = sub,
                status = true
            });
        }
    }
}