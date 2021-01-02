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
                        ID = a.TEACH.C_USER.ID,
                        FIRST_NAME = a.TEACH.C_USER.FIRST_NAME,
                        LAST_NAME = a.TEACH.C_USER.LAST_NAME,
                        MIDDLE_NAME = a.TEACH.C_USER.MIDDLE_NAME
                    }
                },
                C_USER = ((a.C_USER == null) ? null : (a.C_USER.Select(c => new C_USER
                {
                    ID = c.ID,
                    FIRST_NAME = c.FIRST_NAME,
                    LAST_NAME = c.LAST_NAME,
                    MIDDLE_NAME = c.MIDDLE_NAME
                }).ToList()))
            }).ToList();
        }
        public List<COURSE> getdetail(string id)
        {
            var model = db.COURSEs.Where(x => x.ID == id).ToList();
            return model.Select(a => new COURSE
            {
                ID = a.ID,
                NAME = a.NAME,
                DESCRIPTION = a.DESCRIPTION,
                SEMESTER = new SEMESTER { ID = a.SEMESTER.ID, TITLE = a.SEMESTER.TITLE },
                SUBJECT = new SUBJECT { ID = a.SUBJECT.ID, NAME = a.SUBJECT.NAME },
                TEACH = new TEACH
                {
                    C_USER = new C_USER
                    {
                        ID = a.TEACH.C_USER.ID,
                        FIRST_NAME = a.TEACH.C_USER.FIRST_NAME,
                        LAST_NAME = a.TEACH.C_USER.LAST_NAME,
                        MIDDLE_NAME = a.TEACH.C_USER.MIDDLE_NAME,
                        FACULTY= new FACULTY { ID=a.TEACH.C_USER.FACULTY.ID, NAME=a.TEACH.C_USER.FACULTY.NAME}
                    }
                },
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
        public List<COURSE> GetCourseByStudentAndSemester(string student_id, string sem_id)
        {
            C_USER stu = db.C_USER.First(x => x.ID == student_id);
            return stu.COURSEs.Where(c => c.SEMESTER_ID == sem_id).Select(c => new COURSE
            {
                ID = c.ID,
                NAME = c.NAME
            }).ToList();
        }
        public COURSE GetCourseByID(string id)
        {
            {
                COURSE s = db.COURSEs.First(x => x.ID == id);
                return new COURSE()
                {
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
        }
        public List<COURSE> GetCOURSEs()
        {
            var listcourse = db.COURSEs.ToList();
            return listcourse.Select(a => new COURSE
            {
                ID = a.ID,
                NAME = a.NAME,
                DESCRIPTION = a.DESCRIPTION,
                SUBJECT = new SUBJECT { ID = a.SUBJECT.ID, NAME = a.SUBJECT.NAME, FACULTY_ID=a.SUBJECT.FACULTY_ID},
            }).ToList();
        }
        public List<COURSE> GetCourseByTeacherAndSemester(string teacher_id, string sem_id)
        {
            C_USER teacher = db.C_USER.First(x => x.ID == teacher_id);
            List<TEACH> teaches = teacher.TEACHES.Select(c => new TEACH { COURSE = c.COURSE }).ToList();
            List<COURSE> courses = new List<COURSE>();
            foreach (TEACH teach in teaches)
            {
                courses.Add(teach.COURSE);
            }
            return courses;
        }

        public List<COURSE> GetCourseInSemester(string id_sem)
        {
            SEMESTER sem = db.SEMESTERs.Where(a => a.ID == id_sem).FirstOrDefault();
            List<COURSE> course = sem.COURSEs.Select(b => new COURSE
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
                        MIDDLE_NAME = b.TEACH.C_USER.MIDDLE_NAME,
                    }
                }
            }).ToList();
            return course;
        }
    }
}