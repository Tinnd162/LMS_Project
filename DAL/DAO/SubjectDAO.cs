using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.ViewModel;

namespace DAL.DAO
{
    public class SubjectDAO
    {
        LMSProjectDBContext db = null;
        public SubjectDAO()
        {
            db = new LMSProjectDBContext();
        }

        public List<SUBJECT> GetSubjectByTeacherAndCourse(string teacher_id, string course_id)
        {
            object[] sqlParams = {
        new SqlParameter("@user_id", teacher_id),
        new SqlParameter("@course_id", course_id)
      };
            var listSubject = db.Database.SqlQuery<SUBJECT>("sp_GET_SUBJECT_BY_TEACHER_AND_COURSE @user_id, @course_id", sqlParams).ToList();
            return listSubject;
        }
        public List<SUBJECT> getsubject()
        {
            return db.SUBJECTs.ToList();
        }
        public IEnumerable<SubjectModel> getdetail(string id)
        {
            IQueryable<SubjectModel> model = from a in db.SUBJECTs
                                             join b in db.COURSEs on a.COURSE_ID equals b.ID
                                             where a.ID == id
                                             select new SubjectModel()
                                             {
                                                 ID = a.ID,
                                                 NAME = a.NAME,
                                                 DESCRIPTION = a.DESCRIPTION,
                                                 COURSE_ID = a.COURSE_ID,
                                                 TILTE = b.TILTE,
                                             };
            return model.ToList();
        }
        public int insertsubject(string id, string name, string description, string course_id)
        {
            object[] sqlParms = {
            new SqlParameter("@id", id),
            new SqlParameter("@name", name),
            new SqlParameter("@description", description),
            new SqlParameter("@course_id", course_id)
        };
            var model = db.Database.ExecuteSqlCommand("sp_CREATE_SUBJECT @id, @name, @description, @course_id", sqlParms);
            return model;
        }
        public int updatesubject(string id, string name, string description, string course_id)
        {
            object[] sqlParms = {
            new SqlParameter("@id", id),
            new SqlParameter("@name", name),
            new SqlParameter("@description", description),
            new SqlParameter("@course_id", course_id)
            };
            var model = db.Database.ExecuteSqlCommand("sp_UPDATE_SUBJECT  @id, @name, @description, @course_id", sqlParms);
            return model;
        }
        public bool deletesubject(string id)
        {
            var sub = db.SUBJECTs.First(x => x.ID == id);
            db.SUBJECTs.Remove(sub);
            db.SaveChanges();
            return true;
        }
        public List<SUBJECT> subjectIncourse(string id)
        {
            COURSE course = db.COURSEs.Where(a => a.ID == id).FirstOrDefault();
            List<SUBJECT> subject = course.SUBJECTs.Select(b => new SUBJECT
            {
                ID = b.ID,
                NAME = b.NAME,
                DESCRIPTION = b.DESCRIPTION,
                C_USER1 = b.C_USER1.Select(d => new C_USER
                {
                    ID = d.ID,
                    FIRST_NAME = d.FIRST_NAME,
                    LAST_NAME=d.LAST_NAME,
                    MIDDLE_NAME=d.MIDDLE_NAME
                }).ToList()
            }).ToList();
            return subject;
        }
        public List<SUBJECT> Info_Teacher_Student_Subject(string id)
        {
            List<SUBJECT> s = db.SUBJECTs.Where(x => x.ID == id).ToList();
            return s.Select(a=> new SUBJECT()
            {
                ID = a.ID,
                NAME = a.NAME,
                C_USER1 = a.C_USER1.Select(d => new C_USER
                {
                    ID = d.ID,
                    FIRST_NAME = d.FIRST_NAME,
                    LAST_NAME = d.LAST_NAME,
                    MIDDLE_NAME = d.MIDDLE_NAME,
                }).ToList(),
                C_USER = a.C_USER.Select(c => new C_USER
                {
                    ID = c.ID,
                    FIRST_NAME = c.FIRST_NAME,
                    LAST_NAME = c.LAST_NAME,
                    MIDDLE_NAME = c.MIDDLE_NAME
                }).ToList()
            }).ToList();
        }
        public bool deletesubbyID(string idsub,string idcourse)
        {
            COURSE course = db.COURSEs.Where(x => x.ID == idcourse).First();
            var teacher = course.SUBJECTs.Where(y => y.ID == idsub).First();
            db.SUBJECTs.Remove(teacher);
            db.SaveChanges();
            return true;
        }
    }
}