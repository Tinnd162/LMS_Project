using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Models;
using LMS.Common;
using DAL.DAO;
using DAL.EF;
using System.Web.Script.Serialization;


namespace LMS.Areas.Teacher.Controllers
{
    [CustomAuthorize("TEACHER")]
    public class SubjectController : Controller
    {
        // GET: Teacher/Subject
        public ActionResult Index(string id)
        {
            CourseDAO courseDao = new CourseDAO();   
            return View(courseDao.GetCourseByID(id));
        }

       [HttpPost]
        public ActionResult PostTopic(TOPIC topic)
        {
            TopicDAO topicDao = new TopicDAO();

            if (topicDao.InsertTopic(topic))
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }


        [HttpPost]
        public ActionResult PostDoc(DOCUMENT doc)
        {
            DocumentDAO docDao = new DocumentDAO();
            if (docDao.InsertDocument(doc))
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        [HttpPost]
        public ActionResult UpdateDoc(DOCUMENT doc)
        {
            DocumentDAO docDao = new DocumentDAO();
            if (docDao.UpdateDocument(doc))
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult DeleteDoc(string id)
        {
            DocumentDAO docDao = new DocumentDAO();
            if (docDao.DeleteDocument(id))
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult PostEvent(EVENT ev)
        {
            EventDAO eventDao = new EventDAO();
            if (eventDao.InsertEvent(ev))
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult UpdateEvent(EVENT ev)
        {
            EventDAO eventDao = new EventDAO();
            if (eventDao.UpdateEvent(ev))
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult DeleteEvent(string id)
        {
            EventDAO eventDao = new EventDAO();
            if (eventDao.DeleteEvent(id))
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }



        [HttpGet]
        public JsonResult GetTopic(string courseId)//string userId, string courseId, string subjectId)
        {
            CommonFunc cFunc = new CommonFunc();
            if(cFunc.GetSession() != null)
            {

                TopicDAO topicDao = new TopicDAO();
                List<TOPIC> topicEntities = new List<TOPIC>();

                topicEntities = topicDao.GetAllTopicOfTeacherCourse(cFunc.GetIdUserBySession(), cFunc.GetIdSemesterBySession(), courseId);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(topicEntities);
                return Json(new { data = json, status = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = "/Home/Error", status = false });

        }
        
        
        [HttpPost]
        public JsonResult DeleteTopic(string id)
        {
            TopicDAO topicDao = new TopicDAO();
            if (topicDao.DeleteTopic(id))
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}