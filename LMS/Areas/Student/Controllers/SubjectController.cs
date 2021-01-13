using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL.EF;
using DAL.StudentView;
using LMS.Common;

namespace LMS.Areas.Student.Controllers
{
    [CustomAuthorize("STUDENT")]
    public class SubjectController : Controller
    {
        // GET: Student/Subject
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetTopicStudent(string course_id)
        {
            CommonFunc cFunc = new CommonFunc();
            TopicDAO TopicDAO = new TopicDAO();
            var ListTopic = TopicDAO.GetCourseDetailByStuAndCourseAndSubject(cFunc.GetIdUserBySession(), course_id, cFunc.GetIdSemesterBySession());
            return View(ListTopic);
        }
        public ActionResult GetSubjectAssessments(string course_id) 
        { 
            CommonFunc cFunc = new CommonFunc();
            SubmitDAO DAO = new SubmitDAO();
            List<COURSE> courses = DAO.GetSubmitAssessmentByStuAndCourseAndSem(cFunc.GetIdUserBySession());
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
        public ActionResult GetSubmitDetailsByStudentAndEvent(string event_id) {
            CommonFunc cFunc = new CommonFunc();
            SubmitDAO DAO = new SubmitDAO();
            List<COURSE> courses = DAO.GetSubmitAssessmentByStuAndCourseAndSem(cFunc.GetIdUserBySession());
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