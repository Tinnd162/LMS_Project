using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.StudentView;

namespace DAL.DAO
{
    public class SubmitDAO
    {
        LMSProjectDBContext db = null;
        public SubmitDAO() { db = new LMSProjectDBContext(); }
        //public List<SubmitAssessmentView> GetSubmitAssessmentByStuAndCourseAndSem(string user_id, string course_id, string semester_id)
        //{
        //    C_USER stu = db.C_USER.First(x => x.ID == user_id);
        //    var course = stu.COURSEs.Where(c => c.SEMESTER_ID == semester_id).Select(c => new COURSE
        //    {
        //        ID = c.ID,
        //        NAME = c.NAME
        //    }).ToList();
        //    var model = (from a in course 
        //                 join b in db.TOPICs
        //                 on a.ID equals b.COURSE_ID
        //                 join c in db.EVENTs
        //                 on b.ID equals c.TOPIC_ID
        //                 join d in db.SUBMITs
        //                 on c.ID equals d.EVENT_ID
        //                 join e in db.ASSESSMENTs
        //                 on d.ID equals e.SUBMIT_ID
        //                 where a.ID == course_id
        //                 select new
        //                 {
        //                     courseID = a.ID,
        //                     courseName = a.NAME,
        //                     topicID = b.ID,
        //                     topcTitle = b.TITLE,
        //                     eventID = c.ID,
        //                     eventTitle = c.TITLE,
        //                     eventDeadline = c.DEADLINE,
        //                     submitID = d.ID,
        //                     submitLink = d.LINK,
        //                     submitTime = d.TIME,
        //                     assScore = e.SCORE,
        //                     assComment = e.COMMENT
        //                 }).AsEnumerable().Select(x => new SubmitAssessmentView()
        //                 {
        //                     courseID = x.courseID,
        //                     courseName = x.courseName,
        //                     topicID = x.topicID,
        //                     topicTitle = x.topcTitle,
        //                     eventID = x.eventID,
        //                     eventTitle = x.eventTitle,
        //                     eventDeadline = x.eventDeadline,
        //                     submitID = x.submitID,
        //                     submitLink = x.submitLink,
        //                     submitTime = x.submitTime,
        //                     assScore = (float)x.assScore,
        //                     assComment = x.assComment
        //                 });
        //    return model.ToList();
        //}
        public List<COURSE> GetSubmitAssessmentByStuAndCourseAndSem(string user_id, string semester_id)
        {
            C_USER stu = db.C_USER.Where(u => u.ID == user_id).First();

            List<COURSE> courses = stu.COURSEs.Select(x => new COURSE
            {
                ID = x.ID,
                NAME = x.NAME,
                TOPICs = ((x.TOPICs.Count == 0) ? null : (x.TOPICs.Select(t => new TOPIC
                {
                    EVENTs = ((t.EVENTs.Count() == 0) ? null : (t.EVENTs.Select(e => new EVENT
                    {
                        ID = e.ID,
                        TITLE = e.TITLE,
                        DEADLINE = e.DEADLINE,
                        SUBMITs = ((e.SUBMITs.FirstOrDefault(s => s.USER_ID == user_id) == null) ? null : (e.SUBMITs.Where(s => s.USER_ID == user_id)
                                                                                                                    .Select(s => new SUBMIT
                                                                                                                    {
                                                                                                                        ID = s.ID,
                                                                                                                        LINK = s.LINK,
                                                                                                                        TIME = s.TIME,
                                                                                                                        ASSESSMENT = ((s.ASSESSMENT == null) ? null : (new ASSESSMENT{ 
                                                                                                                                

                                                                                                                        }))
                                                                                                                    }).ToList()))
                    }).ToList()))
                }).ToList()))
            }).ToList();
            return courses;
        }
        public List<COURSE> GetDeadlinebyStuAndCourseAndSem(string user_id, string semester_id)
        {
            C_USER stu = db.C_USER.Where(u => u.ID == user_id).First();

            List<COURSE> courses = stu.COURSEs.Select(x => new COURSE {
                ID = x.ID,
                NAME = x.NAME,
                TOPICs = ((x.TOPICs.Count == 0) ? null : (x.TOPICs.Select(t => new TOPIC
                {
                    EVENTs = ((t.EVENTs.Count() == 0) ? null : (t.EVENTs.Select(e => new EVENT
                    {
                        ID = e.ID,
                        TITLE = e.TITLE,
                        DEADLINE = e.DEADLINE,
                        SUBMITs = ((e.SUBMITs.FirstOrDefault(s => s.USER_ID == user_id) == null) ? null : (e.SUBMITs.Where(s => s.USER_ID == user_id)
                                                                                                                    .Select(s => new SUBMIT
                                                                                                                    {
                                                                                                                        ID = s.ID
                                                                                                                    }).ToList()))
                    }).ToList()))
                }).ToList()))
            }).ToList(); 
            return courses;
        }
    }
}
