using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL.EF;
using LMS.Areas.Teacher.Data;

namespace LMS.Areas.Teacher.Controllers
{
    public class AssessmentController : Controller
    {
        // GET: Teacher/Assessment
        public ActionResult Index(string id)
        {
            List<EVENT> listEvent = new List<EVENT>();
            EventDAO eventDao = new EventDAO();
            listEvent = eventDao.GetEventsOfSubject(id);
            return View(listEvent);
        }


        public ActionResult Detail(string eventID, string subID)
        {
     
           
            EventDAO eDao = new EventDAO();
            EVENT ev = eDao.GetEventByID(eventID);

            SubjectDAO subDao = new SubjectDAO();
            SUBJECT sub = subDao.GetSubByID(subID);

            AssessmentDetailView detail = new AssessmentDetailView();
            detail.eVent = ev;
            detail.subject = sub;
           
            return View(detail);
        }


        public ActionResult StudentDetail(string studentID, string eventID)
        {
            
            UserDAO userDao = new UserDAO();
            C_USER student = userDao.GetStudentsByIDWithSubmit(studentID, eventID);

            EventDAO eventDao = new EventDAO();
            EVENT ev = eventDao.GetEventByID(eventID);

            AssessmentStudentDetailView model = new AssessmentStudentDetailView();
            model.student = student;
            model.eVent = ev;
           
            return View(model);
        }

        [HttpPost]
        public JsonResult PostScore(int score, string comment, string submitID)
        {
            AssessmentDAO assDao = new AssessmentDAO();
            if(assDao.AddOrUpdate(score, comment, submitID))
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


    }
}