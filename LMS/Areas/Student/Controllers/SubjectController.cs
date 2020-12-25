using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL.EF;
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
            //course_id = "MATH143001_20211_1";
            //SubmitDAO submitDAO = new SubmitDAO();
            //var ListSubmit = from s in submitDAO.GetSubmitAssessmentByStuAndCourseAndSem(user_id, course_id, semester_id) select s;
            //ListSubmit = ListSubmit.OrderBy(s => s.eventDeadline);
            //return View(ListSubmit.ToList());
            course_id = "MATH143001_20211_1";
            SubmitDAO DAO = new SubmitDAO();
            List<COURSE> courses = DAO.GetSubmitAssessmentByStuAndCourseAndSem(user_id, course_id, semester_id);
            List<SubmitAssessmentView> dView = new List<SubmitAssessmentView>();

            foreach (COURSE course in courses)
            {
                if (course.TOPICs == null) continue;
                foreach (TOPIC topic in course.TOPICs)
                {
                    if (topic.EVENTs == null) continue;
                    foreach (EVENT ev in topic.EVENTs)
                    {
                        SubmitAssessmentView dV = new SubmitAssessmentView()
                        {
                            courseID = course.ID,
                            courseName = course.NAME,
                            eventID = ev.ID,
                            eventTitle = ev.TITLE,
                            eventDescription = ev.DESCRIPTION,
                            eventDeadline = ev.DEADLINE
                        };
                        if (ev.SUBMITs == null)
                        {
                            dView.Add(dV);
                            continue;
                        }
                        foreach (SUBMIT submit in ev.SUBMITs)
                        {
                            dV.submitID = submit.ID;
                            dV.submitLink = submit.LINK;
                            dV.submitTime = submit.TIME;
                            dView.Add(dV);
                            if (submit.ASSESSMENT == null)
                            {
                                dView.Add(dV);
                            }
                            else
                            {
                                dV.assComment = submit.ASSESSMENT.COMMENT;
                                dV.assScore = (float)submit.ASSESSMENT.SCORE;
                                dView.Add(dV);
                            }
                            
                        }

                    }
                }
            }
            var ListEvent = from s in dView select s;
            ListEvent = ListEvent.Where(s => s.courseID == course_id);
            ListEvent = ListEvent.OrderBy(s => s.eventDeadline);
            return View(ListEvent.ToList());

        }
        public ActionResult GetSubmitDetailsByStudentAndEvent(string event_id,string user_id = "U00008", string course_id = "MATH143001_20211_1", string semester_id = "20211")
        {
            event_id = "event16086191562960h";
            SubmitDAO DAO = new SubmitDAO();
            List<COURSE> courses = DAO.GetSubmitAssessmentByStuAndCourseAndSem(user_id, course_id, semester_id);
            List<SubmitAssessmentView> dView = new List<SubmitAssessmentView>();

            foreach (COURSE course in courses)
            {
                if (course.TOPICs == null) continue;
                foreach (TOPIC topic in course.TOPICs)
                {
                    if (topic.EVENTs == null) continue;
                    foreach (EVENT ev in topic.EVENTs)
                    {
                        SubmitAssessmentView dV = new SubmitAssessmentView()
                        {
                            courseID = course.ID,
                            courseName = course.NAME,
                            eventID = ev.ID,
                            eventTitle = ev.TITLE,
                            eventDescription = ev.DESCRIPTION,
                            eventDeadline = ev.DEADLINE
                        };
                        if (ev.SUBMITs == null)
                        {
                            dView.Add(dV);
                            continue;
                        }
                        foreach (SUBMIT submit in ev.SUBMITs)
                        {
                            dV.submitID = submit.ID;
                            dV.submitLink = submit.LINK;
                            dV.submitTime = submit.TIME;
                            dView.Add(dV);
                            if (submit.ASSESSMENT == null)
                            {
                                dView.Add(dV);
                            }
                            else
                            {
                                dV.assComment = submit.ASSESSMENT.COMMENT;
                                dV.assScore = (float)submit.ASSESSMENT.SCORE;
                                dView.Add(dV);
                            }

                        }

                    }
                }
            }
            var ListEvent = from s in dView select s;
            ListEvent = ListEvent.Where(s => s.eventID == event_id);
            return View(ListEvent.ToList());
        }
    }
    
}