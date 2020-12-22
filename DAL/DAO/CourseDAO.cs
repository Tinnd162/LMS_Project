using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;


namespace DAL.DAO
{
    public class CourseDAO
    {
        LMSProjectDBContext db = null;
        public CourseDAO()
        {
            db = new LMSProjectDBContext();
        }

        public List<COURSE> GetCourseByTeacherAndSemester(string teacher_id, string sem_id)
        {
            C_USER teacher = db.C_USER.First(x => x.ID == teacher_id);
            List<TEACH> teaches = teacher.TEACHES.Select(c => new TEACH { COURSE = c.COURSE }).ToList();
            List<COURSE> courses = new List<COURSE>();
            foreach(TEACH teach in teaches)
            {
                courses.Add(teach.COURSE);
            }
            return courses;
        }

        public COURSE GetCourseByID(string id)
        {
            COURSE s =  db.COURSEs.First(x => x.ID == id);
            return new COURSE() {
                ID = s.ID,
                NAME = s.NAME,
                DESCRIPTION = s.DESCRIPTION,
                SEMESTER_ID = s.SEMESTER_ID,
                C_USER = s.C_USER.Select(u => new C_USER
                {
                    ID = u.ID,
                    FIRST_NAME = u.FIRST_NAME,
                    LAST_NAME = u.LAST_NAME,
                    MIDDLE_NAME = u.MIDDLE_NAME
                }).ToList(),
                TOPICs = s.TOPICs.Select(t => new TOPIC
                {
                    ID = t.ID,
                    TITLE = t.TITLE,
                    DESCRIPTION = t.DESCRIPTION
                }).ToList()
            };
        }
        public List<COURSE> GetCOURSEs()
        {
            return db.COURSEs.ToList();
        }
        //public IEnumerable<SubjectModel> getdetail(string id)
        //{
        //    IQueryable<SubjectModel> model = from a in db.SUBJECTs
        //                                     join b in db.COURSEs on a.COURSE_ID equals b.ID
        //                                     where a.ID == id
        //                                     select new SubjectModel()
        //                                     {
        //                                         ID = a.ID,
        //                                         NAME = a.NAME,
        //                                         DESCRIPTION = a.DESCRIPTION,
        //                                         COURSE_ID = a.COURSE_ID,
        //                                         TILTE = b.TILTE,
        //                                     };
        //    return model.ToList();
        //}
        //public int insertsubject(string id, string name, string description, string course_id)
        //{
        //    object[] sqlParms = {
        //    new SqlParameter("@id", id),
        //    new SqlParameter("@name", name),
        //    new SqlParameter("@description", description),
        //    new SqlParameter("@course_id", course_id)
        //};
        //    var model = db.Database.ExecuteSqlCommand("sp_CREATE_SUBJECT @id, @name, @description, @course_id", sqlParms);
        //    return model;
        //}
        //public int updatesubject(string id, string name, string description, string course_id)
        //{
        //    object[] sqlParms = {
        //    new SqlParameter("@id", id),
        //    new SqlParameter("@name", name),
        //    new SqlParameter("@description", description),
        //    new SqlParameter("@course_id", course_id)
        //    };
        //    var model = db.Database.ExecuteSqlCommand("sp_UPDATE_SUBJECT  @id, @name, @description, @course_id", sqlParms);
        //    return model;
        //}
        //public bool deletesubject(string id)
        //{
        //    var sub = db.SUBJECTs.First(x => x.ID == id);
        //    db.SUBJECTs.Remove(sub);
        //    db.SaveChanges();
        //    return true;
        //}
        public List<COURSE> GetCourseInSemester(string id_sem)
        {
            SEMESTER sem = db.SEMESTERs.Where(a => a.ID == id_sem).FirstOrDefault();
            List<COURSE> course = sem.COURSEs.Select(b => new COURSE
            {
                ID = b.ID,
                NAME = b.NAME,
                DESCRIPTION = b.DESCRIPTION,
                TEACH = new TEACH 
                { C_USER = new C_USER 
                    { 
                        ID = b.TEACH.C_USER.ID,
                        FIRST_NAME = b.TEACH.C_USER.FIRST_NAME,
                        LAST_NAME = b.TEACH.C_USER.LAST_NAME,
                        MIDDLE_NAME = b.TEACH.C_USER.MIDDLE_NAME,
                    } 
                }
            }).ToList();
            return course;
        }
        public List<COURSE> Info_Teacher_Student_Course(string id)
        {
            List<COURSE> s = db.COURSEs.Where(x => x.ID == id).ToList();
            return s.Select(b=> new COURSE()
            {
                ID = b.ID,
                NAME = b.NAME,
                TEACH = new TEACH
                {
                    C_USER = new C_USER
                    {
                        ID = b.TEACH.C_USER.ID,
                        FIRST_NAME = b.TEACH.C_USER.FIRST_NAME,
                        LAST_NAME = b.TEACH.C_USER.LAST_NAME,
                        MIDDLE_NAME = b.TEACH.C_USER.MIDDLE_NAME,
                    }
                },
                C_USER = b.C_USER.Select(c => new C_USER
                {
                    ID = c.ID,
                    FIRST_NAME = c.FIRST_NAME,
                    LAST_NAME = c.LAST_NAME,
                    MIDDLE_NAME = c.MIDDLE_NAME
                }).ToList()
            }).ToList();
        }
        //public bool DelCourseByID(string idcourse,string idsem)
        //{
        //    SEMESTER sem = db.SEMESTERs.Where(x => x.ID == idsem).First();
        //    var course = sem.COURSEs.Where(y => y.ID == idcourse).First();
        //    db.COURSEs.Remove(course);
        //    db.SaveChanges();
        //    return true;
        //}

        public bool DelCourse(string id)
        {
            try
            {
                SUBJECT sub = db.SUBJECTs.First(x => x.ID == id);
                db.SUBJECTs.Remove(sub);
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