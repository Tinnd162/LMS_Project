﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;


namespace DAL.DAO
{
    public class InfoTeacherDAO
    {
        LMSProjectDBContext db = null;
        public InfoTeacherDAO() { db = new LMSProjectDBContext(); }
        //Done
        public List<C_USER> detailteacher(string IDteacher)
        {
            ROLE role = db.ROLEs.Where(a => a.ROLE1 == "TEACHER").FirstOrDefault();
            List<C_USER> listteacher = role.C_USER.ToList();
            return listteacher.Where(a=>a.ID==IDteacher).Select(x => new C_USER
            {
                ID = x.ID,
                FIRST_NAME = x.FIRST_NAME,
                LAST_NAME = x.LAST_NAME,
                MIDDLE_NAME = x.MIDDLE_NAME,
                PHONE_NO = x.PHONE_NO,
                DoB = x.DoB,
                SEX = x.SEX,
                MAIL = x.MAIL,
                PASSWORD = x.PASSWORD,
                LASTVISITDATE = x.LASTVISITDATE,
                FACULTY = new FACULTY { ID = x.FACULTY.ID, NAME = x.FACULTY.NAME }

            }).ToList();
        }
        //Done
        public bool deleteteacher(string id)
        {
            var teacher = db.C_USER.Find(id);
            db.C_USER.Remove(teacher);
            db.SaveChanges();
            return true;
        }
        //Done
        public List<C_USER> getteacher()
        {
            ROLE role = db.ROLEs.Where(a => a.ROLE1 == "TEACHER").FirstOrDefault();
            List<C_USER> listteacher = role.C_USER.ToList();
            return listteacher.Select(x => new C_USER
            {
                ID = x.ID,
                FIRST_NAME = x.FIRST_NAME,
                LAST_NAME=x.LAST_NAME,
                MIDDLE_NAME=x.MIDDLE_NAME,
                PHONE_NO = x.PHONE_NO,
                DoB = x.DoB,
                MAIL = x.MAIL,
                FACULTY = new FACULTY { ID = x.FACULTY.ID, NAME = x.FACULTY.NAME }
            }).ToList();
        }
        public bool updateteacher(C_USER teacher)
        {
            var model = db.C_USER.Find(teacher.ID);
            model.FIRST_NAME = teacher.FIRST_NAME;
            model.LAST_NAME = teacher.LAST_NAME;
            model.MIDDLE_NAME = teacher.MIDDLE_NAME;
            model.PHONE_NO = teacher.PHONE_NO;
            model.DoB = teacher.DoB;
            model.SEX = teacher.SEX;
            model.MAIL = teacher.MAIL;
            model.PASSWORD = teacher.PASSWORD;
            model.FACULTY_ID = teacher.FACULTY_ID;
            db.SaveChanges();
            return true;
        }
        public List<C_USER> GetCourseByIDTeacher(string ID)
        {
            List<C_USER> teacher = db.C_USER.Where(x => x.ID == ID).ToList();
            return teacher.Select(a => new C_USER
            {
                ID = a.ID,
                FIRST_NAME = a.FIRST_NAME,
                MIDDLE_NAME = a.MIDDLE_NAME,
                LAST_NAME = a.LAST_NAME,
                COURSEs = a.COURSEs.Select(b => new COURSE { ID = b.ID, NAME = b.NAME, SEMESTER=new SEMESTER {ID=b.SEMESTER.ID, TITLE = b.SEMESTER.TITLE} }).ToList()             
            }).ToList();
        }
    }
}

