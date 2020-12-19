using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.DAO
{
    public class SubjectDAO
    {
        LMSProjectDBContext db = null;
        public SubjectDAO() { db = new LMSProjectDBContext(); }

        public List<SUBJECT> GetSubjectByTeacherAndCourse(string teacher_id, string course_id)
        {
            C_USER teacher = db.C_USER.First(x => x.ID == teacher_id);
            return teacher.SUBJECTs1.Where(x => x.COURSE_ID == course_id).ToList();
        }

        public SUBJECT GetSubByID(string id)
        {
            SUBJECT s =  db.SUBJECTs.First(x => x.ID == id);
            return new SUBJECT() {
                ID = s.ID,
                NAME = s.NAME,
                DESCRIPTION = s.DESCRIPTION,
                COURSE_ID = s.COURSE_ID,
                C_USER = s.C_USER.Select(u => new C_USER
                {
                    ID = u.ID,
                    FIRST_NAME = u.FIRST_NAME,
                    LAST_NAME = u.LAST_NAME,
                    MIDDLE_NAME = u.MIDDLE_NAME
                }).ToList()
            };
        }

        //public bool DelSub(string id)
        //{
        //    try
        //    {
        //        SUBJECT sub = db.SUBJECTs.First(x => x.ID == id);
        //        db.SUBJECTs.Remove(sub);
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
