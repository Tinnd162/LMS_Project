using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL.EF;
using LMS.Common;
using LMS.Models;


namespace LMS.Areas.Student.Controllers
{
    public class MenuSideBarController : Controller
    {
        // GET: Student/MenuSideBar
        [ChildActionOnly]
        public ActionResult Index()
        {
            //CommonFunc cf = new CommonFunc();

            //SemesterDAO semDao = new SemesterDAO();
            //SEMESTER sem = semDao.GetSemesterByID(cf.GetIdSemesterBySession());
            //UserDAO userDao = new UserDAO();
            //Session["UserName"] = userDao.GetUserByID(cf.GetIdUserBySession()).FIRST_NAME;

            return View();
        }


        [ChildActionOnly]
        public ActionResult Semester()
        { 
            SemesterDAO semDAO = new SemesterDAO();
            var listSem = semDAO.GetSEMESTERs();

            return View(listSem);
        }

        [ChildActionOnly]
        public ActionResult Subjects(string semester_id = "20211") //= "JZDN2020112521542821")
        {
            //**************************Test*******************************************
            //User user = new User();
            //user.id = "JZDN2020112521542805";
            //user.name = "A";
            //Session.Add(CommonConstants.USER_SESSION, user);
            //******************Test**************************************************

            //CommonFunc comf = new CommonFunc();
            //string user_id = comf.GetIdUserBySession();
            //if (semester_id == null)
            //{
            //    semester_id = comf.GetIdSemesterBySession();
            //}
            string user_id = "U00008";
            CourseDAO courseDAO = new CourseDAO();
            var listCourse = courseDAO.GetCourseByStudentAndSemester(user_id, semester_id);
            return View(listCourse);
        }


        [ChildActionOnly]
        public ActionResult SubjectAssessments(string semester_id = "20211")//string user_id = "JZDN2020112521542805", string course_id = "JZDN2020112521542821")
        {
            //CommonFunc comf = new CommonFunc();
            //string user_id = comf.GetIdUserBySession();
            //if (semester_id == null)
            //{
            //    semester_id = comf.GetIdSemesterBySession();
            //}
            string user_id = "U00008";
            CourseDAO courseDao = new CourseDAO();
            var listCourse = courseDao.GetCourseByStudentAndSemester(user_id, semester_id);
            return View(listCourse);
        }
    }

}