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
    public class SubjectController : Controller
    {
        // GET: Teacher/Subject
        public ActionResult Index(string id)
        {

            return View();
        }

       [HttpPost]
        public ActionResult PostTopic(TOPIC topic)
        {
           //TOPIC topicEntity = new TOPIC();
            //topicEntity.ID = topic.ID;
            //topicEntity.TITLE = topic.Title;
            //topicEntity.DESCRIPTION = topic.Description;
            //topicEntity.SUB_ID = topic.SubjectID;

            TopicDAO topicDao = new TopicDAO();

            if (topicDao.InsertTopic(topic))
            {
                //if(topic.DOCUMENTs != null)
                //{
                //    DocumentDAO docDao = new DocumentDAO();
                //    foreach (Document doc in topic.Documents)
                //    {
                //        DOCUMENT docEntity = new DOCUMENT();
                //        docEntity.ID = doc.ID;
                //        docEntity.TITLE = doc.Title;
                //        docEntity.DESCRIPTION = doc.Description;
                //        docEntity.LINK = doc.Link;
                //        docEntity.TYPE = null;
                //        docEntity.TOPIC_ID = topic.ID;

                //        docDao.InsertDocument(docEntity);
                //    }
                //}
                
                //if(topic.Events != null)
                //{
                //    EventDAO eventDao = new EventDAO();
                //    foreach (Event ev in topic.Events)
                //    {
                //        EVENT eventEntity = new EVENT();
                //        eventEntity.ID = ev.ID;
                //        eventEntity.TITLE = ev.Title;
                //        eventEntity.DESCRIPTION = ev.Description;
                //        eventEntity.STARTDATE = Convert.ToDateTime(ev.StartDate);
                //        eventEntity.DEADLINE = Convert.ToDateTime(ev.D);
                //        eventEntity.TOPIC_ID = topic.ID;

                //        eventDao.InsertEvent(eventEntity);
                //    }
                //}
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
        public JsonResult GetTopic(string userId, string courseId, string subjectId)
        {
            TopicDAO topicDao = new TopicDAO();
            List<TOPIC> topicEntities = new List<TOPIC>();
          
            topicEntities = topicDao.GetAllTopicOfTeacherSub(userId, courseId, subjectId);           

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(topicEntities);
            return Json(new { data = json }, JsonRequestBehavior.AllowGet);
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