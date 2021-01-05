using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL.StudentView;
using DAL.EF;

namespace LMS.Areas.Student.Controllers
{
    public class HomeController : Controller
    {
        // GET: Student/Home
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ViewResult Index(string user_id = "U00008", string semester_id = "20211")
        {
            SubmitDAO dao = new SubmitDAO();
            List<COURSE> courses = dao.GetDeadlinebyStuAndCourseAndSem(user_id, semester_id);
            List<DeadlineView> dView = new List<DeadlineView>();

            foreach(COURSE course in courses)
            {
                if (course.TOPICs == null) continue;
                foreach(TOPIC topic in course.TOPICs)
                {
                    if (topic.EVENTs == null) continue;
                    foreach(EVENT ev in topic.EVENTs)
                    {
                        DeadlineView dV = new DeadlineView()
                        {
                            courseID = course.ID,
                            courseName = course.NAME,
                            eventID = ev.ID,
                            eventTitle = ev.TITLE,
                            eventDeadline = ev.DEADLINE
                        };
                        if (ev.SUBMITs == null) {
                            dView.Add(dV);
                            continue;
                        }                      
                        foreach(SUBMIT submit in ev.SUBMITs)
                        {
                            dV.submitID = submit.ID;
                            dView.Add(dV);
                        }
                    }
                }
            }
            var ListEvent = from s in dView select s;
            ListEvent = ListEvent.Where(s => s.eventDeadline > DateTime.Now);
            ListEvent = ListEvent.OrderBy(s => s.eventDeadline);
            return View(ListEvent.ToList());

        }  
    }
}