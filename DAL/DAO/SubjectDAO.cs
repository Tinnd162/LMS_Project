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

        public List<SUBJECT> GetSubjectByTeacherAndCourse(int teacher_id, int course_id)
        {
            object[] sqlParams=
            {
                new SqlParameter("@user_id", teacher_id),
                new SqlParameter("@course_id", course_id)
            };
            var listSubject = db.Database.SqlQuery<SUBJECT>("GET_SUBJECT_BY_TEACHER_AND_COURSE @user_id, @course_id", sqlParams).ToList();
            return listSubject;
        }
    }
}
