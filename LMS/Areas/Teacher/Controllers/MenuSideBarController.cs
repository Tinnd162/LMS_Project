using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;

namespace LMS.Areas.Teacher.Controllers
{
    public class MenuSideBarController : Controller
    {
        // GET: Teacher/MenuSideBar
        [ChildActionOnly]
        public ActionResult Index()
        {
            return View();
        }


        [ChildActionOnly]
        public ActionResult Courses()
        {
            CourseDAO courseDAO = new CourseDAO();
            var listCourse = courseDAO.GetCOURSEs();
            
            return View(listCourse);
        }

        [ChildActionOnly]
        public ActionResult Subjects(int user_id = 2, int course_id = 4)
        {
            SubjectDAO subjectDAO = new SubjectDAO();
            var listSubject = subjectDAO.GetSubjectByTeacherAndCourse(user_id, course_id);
            return View(listSubject);
        }

        [ChildActionOnly]
        public ActionResult SubjectAssessments(int user_id = 2, int course_id = 4)
        {
            SubjectDAO subjectDAO = new SubjectDAO();
            var listSubject = subjectDAO.GetSubjectByTeacherAndCourse(user_id, course_id);
            return View(listSubject);
        }
    }
}