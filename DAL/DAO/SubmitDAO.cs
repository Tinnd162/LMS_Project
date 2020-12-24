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
        public List<SubmitAssessmentView> GetSubmitAssessmentByStuAndCourseAndSem(string user_id, string course_id, string semester_id)
        {
            C_USER stu = db.C_USER.Where(u => u.ID == user_id).First();
            var model = (from a in db.COURSEs
                         join b in db.TOPICs
                         on a.ID equals b.COURSE_ID
                         join c in db.EVENTs
                         on b.ID equals c.TOPIC_ID
                         join d in db.SUBMITs
                         on c.ID equals d.EVENT_ID
                         join e in db.ASSESSMENTs
                         on d.ID equals e.SUBMIT_ID
                         where a.ID == course_id && stu.ID == d.USER_ID && a.SEMESTER_ID == semester_id
                         select new
                         {
                             courseID = a.ID,
                             courseName = a.NAME,
                             topicID = b.ID,
                             topcTitle = b.TITLE,
                             eventID = c.ID,
                             eventTitle = c.TITLE,
                             eventDeadline = c.DEADLINE,
                             submitID = d.ID,
                             submitLink = d.LINK,
                             submitTime = d.TIME,
                             assScore = e.SCORE,
                             assComment = e.COMMENT
                         }).AsEnumerable().Select(x => new SubmitAssessmentView()
                         {
                             courseID = x.courseID,
                             courseName = x.courseName,
                             topicID = x.topicID,
                             topicTitle = x.topcTitle,
                             eventID = x.eventID,
                             eventTitle = x.eventTitle,
                             eventDeadline = x.eventDeadline,
                             submitID = x.submitID,
                             submitLink = x.submitLink,
                             submitTime = x.submitTime,
                             assScore = (float)x.assScore,
                             assComment = x.assComment
                         });
            return model.ToList();
        }
        public List<DeadlineView> GetDeadlinebyStuAndCourseAndSem(string user_id, string semester_id)
        {
            C_USER stu = db.C_USER.Where(u => u.ID == user_id).First();
            var model = (from a in db.COURSEs 
                         join b in db.TOPICs 
                         on a.ID equals b.COURSE_ID
                         join c in db.EVENTs
                         on b.ID equals c.TOPIC_ID
                         join d in db.SUBMITs
                         on c.ID equals d.EVENT_ID
                         where stu.ID == d.USER_ID && a.SEMESTER_ID == semester_id
                         select new
                         {
                             courseID = a.ID,
                             courseName = a.NAME,
                             eventID = c.ID,
                             eventTitle = c.TITLE,
                             eventDeadline = c.DEADLINE,
                             submitID = d.ID
                         }).AsEnumerable().Select(x => new DeadlineView()
                         {
                             courseID = x.courseID,
                             courseName = x.courseName,
                             eventID = x.eventID,
                             eventTitle = x.eventTitle,
                             eventDeadline = x.eventDeadline,
                             submitID = x.submitID
                         });
            return model.ToList();
        }
    }
}
