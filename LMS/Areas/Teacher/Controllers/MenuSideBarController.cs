using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using LMS.Common;
using DAL.EF;


namespace LMS.Areas.Teacher.Controllers
{
    public class MenuSideBarController : Controller
    {
        // GET: Teacher/MenuSideBar
        [ChildActionOnly]
        public ActionResult Index()
        {
            CommonFunc cf = new CommonFunc();
            
            CourseDAO courseDao = new CourseDAO();
            COURSE course = courseDao.GetCourseByID(cf.GetIdCourseBySession());
            UserDAO userDao = new UserDAO();
            Session["UserName"] = userDao.GetUserByID(cf.GetIdUserBySession()).FIRST_NAME;

            return View(course);
        }


        [ChildActionOnly]
        public ActionResult Courses()
        {
            CourseDAO courseDAO = new CourseDAO();
            var listCourse = courseDAO.GetCOURSEs();
            
            return View(listCourse);
        }

        [ChildActionOnly]
        public ActionResult Subjects(string course_id) //= "JZDN2020112521542821")
        {
            //**************************Test*******************************************
            //User user = new User();
            //user.id = "JZDN2020112521542805";
            //user.name = "A";
            //Session.Add(CommonConstants.USER_SESSION, user);
            //******************Test**************************************************

            CommonFunc comf = new CommonFunc();
            string user_id = comf.GetIdUserBySession();
            if(course_id == null)
            {
                course_id = comf.GetIdCourseBySession();
            }

            SubjectDAO subjectDAO = new SubjectDAO();
            var listSubject = subjectDAO.GetSubjectByTeacherAndCourse(user_id, course_id);
            return View(listSubject);
        }

         
        [ChildActionOnly]
        public ActionResult SubjectAssessments(string course_id)//string user_id = "JZDN2020112521542805", string course_id = "JZDN2020112521542821")
        {
            CommonFunc comf = new CommonFunc();
            string user_id = comf.GetIdUserBySession();
            if (course_id == null)
            {
                course_id = comf.GetIdCourseBySession();
            }

            SubjectDAO subjectDAO = new SubjectDAO();
            var listSubject = subjectDAO.GetSubjectByTeacherAndCourse(user_id, course_id);
            return View(listSubject);
        }
    }
}