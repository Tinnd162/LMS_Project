using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL.StudentView;

namespace LMS.Areas.Student.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Student/Subject
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetTopicStudent(string course_id ,string semester_id = "20211", string user_id = "U00008")
        {
            course_id = "MATH143001_20211_1";
            TopicDAO TopicDAO = new TopicDAO();
            var ListTopic = TopicDAO.GetCourseDetailByStuAndCourseAndSubject(user_id, course_id, semester_id);
            return View(ListTopic);
        }
        public ActionResult GetSubjectAssessments(string course_id, string semester_id = "20211", string user_id = "U00008")
        {
            course_id = "MATH143001_20211_1";
            SubmitDAO submitDAO = new SubmitDAO();
            var ListSubmit = from s in submitDAO.GetSubmitAssessmentByStuAndCourseAndSem(user_id, course_id, semester_id) select s;
            ListSubmit = ListSubmit.OrderBy(s => s.eventDeadline);
            return View(ListSubmit);
        }
    }
}