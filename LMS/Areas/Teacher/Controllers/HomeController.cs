using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL.EF;
using LMS.Common;
using LMS.Models;
namespace LMS.Areas.Teacher.Controllers
{
    public class HomeController : Controller
    {
        // GET: Teacher/Home
        public ActionResult Index(string semester_id) //= "JZDN2020112521542821")
        {

            CommonFunc cFunc = new CommonFunc();
            if(cFunc.GetSession() != null)
            {
                string user_id = cFunc.GetIdUserBySession();
                if (cFunc.checkRole(user_id, "TEACHER"))
                {
                    if (semester_id == null)
                    {
                        semester_id = cFunc.GetIdCourseBySession();
                    }
                    CourseDAO courseDao = new CourseDAO();
                    List<COURSE> listCourse = courseDao.GetCourseByTeacherAndSemester(user_id, semester_id);

                    //********************************************
                   // cFunc.SetCookie(user_id, "/Teacher/Home/Index");

                    return View(listCourse);
                }
            }  
            return RedirectToAction("Error", "Home", new { area = ""});  
        }
    }
}