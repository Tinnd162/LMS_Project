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
    public class CourseDAO
    {
        LMSProjectDBContext db = null;
        public CourseDAO()
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
        public List<COURSE> getcourse()
        {
            return db.COURSEs.ToList();
        }
        public bool deletecourse(string id)
        {
            var course = db.COURSEs.First(x => x.ID == id);
            db.COURSEs.Remove(course);
            db.SaveChanges();
            return true;
        }
        public List<COURSE> courseinsemester(string id)
        {
            SEMESTER semester = db.SEMESTERs.Where(a => a.ID == id).FirstOrDefault();
            List<COURSE> course = semester.COURSEs.Select(b => new COURSE
            {
                ID = b.ID,
                NAME = b.NAME,
                DESCRIPTION = b.DESCRIPTION,
                TEACH = new TEACH
                {
                    C_USER = new C_USER
                    {
                        ID = b.TEACH.C_USER.ID,
                        FIRST_NAME = b.TEACH.C_USER.FIRST_NAME,
                        LAST_NAME = b.TEACH.C_USER.LAST_NAME,
                        MIDDLE_NAME = b.TEACH.C_USER.MIDDLE_NAME
                    }
                },
            }).ToList();
            return course;
        }
        public List<COURSE> InfoTeacherStudentInCourse(string id)
        {
            List<COURSE> s = db.COURSEs.Where(x => x.ID == id).ToList();
            return s.Select(a => new COURSE()
            {
                ID = a.ID,
                NAME = a.NAME,
                TEACH = new TEACH
                {
                    C_USER = new C_USER
                    {
                        ID=a.TEACH.C_USER.ID,
                        FIRST_NAME=a.TEACH.C_USER.FIRST_NAME,
                        LAST_NAME=a.TEACH.C_USER.LAST_NAME,
                        MIDDLE_NAME=a.TEACH.C_USER.MIDDLE_NAME
                    }
                },
                C_USER = a.C_USER.Select(c => new C_USER
                {
                    ID = c.ID,
                    FIRST_NAME = c.FIRST_NAME,
                    LAST_NAME = c.LAST_NAME,
                    MIDDLE_NAME = c.MIDDLE_NAME
                }).ToList()
            }).ToList();
        }
        public List<COURSE> getdetail(string id)
        {
            var model = db.COURSEs.Where(x => x.ID == id).ToList();
            return model.Select(a => new COURSE
            {
                ID = a.ID,
                NAME=a.NAME,
                DESCRIPTION=a.DESCRIPTION,
                SEMESTER= new SEMESTER { ID=a.SEMESTER.ID, TITLE=a.SEMESTER.TITLE},
                SUBJECT= new SUBJECT { ID=a.SUBJECT.ID, NAME=a.SUBJECT.NAME}
            }).ToList();
        }
        public bool addcourse(COURSE course)
        {
            db.COURSEs.Add(course);
            db.SaveChanges();
            return true;
        }
        public bool updatecourse(COURSE course)
        {
            var model = db.COURSEs.Find(course.ID);
            model.NAME = course.NAME;
            model.DESCRIPTION = course.DESCRIPTION;
            model.SUBJECT_ID = course.SUBJECT_ID;
            model.SEMESTER_ID = course.SEMESTER_ID;
            db.SaveChanges();
            return true;
        }
    }
}