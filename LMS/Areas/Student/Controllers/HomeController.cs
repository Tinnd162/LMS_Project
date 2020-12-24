using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL.StudentView;

namespace LMS.Areas.Student.Controllers
{
    public class HomeController : Controller
    {
        // GET: Student/Home
        //public ActionResult Index()
        //{
        //    return View(); 
        //}
        public ViewResult Index(string searchString, string user_id = "U00008", string semester_id = "20211")
        {
            SubmitDAO Deadline = new SubmitDAO();
            var listDeadLine = from s in Deadline.GetDeadlinebyStuAndCourseAndSem(user_id, semester_id) select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                listDeadLine = listDeadLine.Where(s => s.submitID.Contains(null));
            }
            listDeadLine = listDeadLine.OrderBy(s => s.eventDeadline);
            return View(listDeadLine.ToList());
        }   

        //public ActionResult Index(string user_id = "U00008", string semester_id = "20211")
        //{
        //    SubmitDAO Deadline = new SubmitDAO();
        //    var listDeadLine = Deadline.GetDeadlinebyStuAndCourseAndSem(user_id, semester_id);
        //    return View(listDeadLine);
        //}
    }
}