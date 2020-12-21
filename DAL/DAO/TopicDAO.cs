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

        public List<TOPIC> GetAllTopicOfTeacherCourse(string userId, string semId, string courseId)
        {
            //var topicsInSubTeachedByTeacher = db.C_USER.Where(x => x.ID == userId)
            //                    .Select(x => x.SUBJECTs1.Where(s => s.COURSE_ID == courseId && s.ID == subjectId)
            //                                            .Select(s => s.TOPICs)).ToList();

            C_USER teacher = db.C_USER.Where(u => u.ID == userId).First();
           TEACH teach = teacher.TEACHES.Where(t => t.COURSE.ID == courseId).Select(t => new TEACH { COURSE = t.COURSE}).First();
            List<TOPIC> topics = teach.COURSE.TOPICs.Select(t => new TOPIC
                {
                    ID = t.ID,
                    TITLE = t.TITLE,
                    DESCRIPTION = t.DESCRIPTION,
                    COURSE_ID = t.COURSE_ID,
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
                }).ToList();
            
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
        public TOPIC GetTopicById(string id)
        {
            return db.TOPICs.First(x => x.ID == id);
        }

    }
}
