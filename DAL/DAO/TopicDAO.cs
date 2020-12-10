using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;


namespace DAL.DAO
{
    public class TopicDAO
    {
        LMSProjectDBContext db = null;
        public TopicDAO() { db = new LMSProjectDBContext(); }

        public bool InsertTopic(TOPIC topic)
        {
            db.TOPICs.Add(topic);
            db.SaveChanges();
            return true;
        }

        public List<TOPIC> GetAllTopicOfTeacherSub(string userId, string courseId, string subjectId)
        {
            //var topicsInSubTeachedByTeacher = db.C_USER.Where(x => x.ID == userId)
            //                    .Select(x => x.SUBJECTs1.Where(s => s.COURSE_ID == courseId && s.ID == subjectId)
            //                                            .Select(s => s.TOPICs)).ToList();

            C_USER teacher = db.C_USER.Where(u => u.ID == userId).First();          
            SUBJECT subjects = teacher.SUBJECTs1.Where(s => s.COURSE_ID == courseId && s.ID == subjectId).First();
            List<TOPIC> topics = subjects.TOPICs.Select(
                    t => new TOPIC
                    {
                        ID = t.ID,
                        TITLE = t.TITLE,
                        DESCRIPTION = t.DESCRIPTION,
                        SUB_ID = t.SUB_ID,
                        DOCUMENTs = t.DOCUMENTs
                                    .Select(d => new DOCUMENT
                                    {
                                        ID = d.ID,
                                        TITLE = d.TITLE,
                                        DESCRIPTION = d.DESCRIPTION,
                                        LINK = d.LINK
                                    })
                                    .ToList(),

                        EVENTs = t.EVENTs
                                    .Select(e => new EVENT
                                    {
                                        ID = e.ID,
                                        TITLE = e.TITLE,
                                        DESCRIPTION = e.DESCRIPTION,
                                        STARTDATE = e.STARTDATE,
                                        DEADLINE = e.DEADLINE
                                    })
                                    .ToList()
                    }
                ).ToList();          
            return topics;

        }

        public bool DeleteTopic(string id)
        {
            try
            {
                TOPIC topic = db.TOPICs.First(x => x.ID == id);
                db.TOPICs.Remove(topic);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
