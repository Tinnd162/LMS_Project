using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using PagedList;
using DAL.ViewModel;
using System.Data.SqlClient;
using System.Data.Entity.Migrations;
namespace DAL.DAO
{
    public class CourseDAO
    {
        LMSProjectDBContext db = null;

        public CourseDAO() { db = new LMSProjectDBContext(); }

        public List<COURSE> GetCOURSEs()
        {
            return db.COURSEs.ToList();
        }
        public List<COURSE> getdetail(string id)
        {
            return db.COURSEs.Where(x=>x.ID==id).ToList();
        }
        public bool deletecourse(string id)
        {
            var course = db.COURSEs.Find(id);
            db.COURSEs.Remove(course);
            db.SaveChanges();
            return true;
        }
        public bool updatecourse(COURSE course)
        {
            var model = db.COURSEs.Find(course.ID);
            model.TILTE = course.TILTE;
            model.DESCRIPTION = course.DESCRIPTION;
            db.SaveChanges();
            return true;
        }
        public bool addcourse(COURSE course)
        {
            db.COURSEs.Add(course);
            db.SaveChanges();
            return true;
        }
        public COURSE GetCourseByID(string id)
        {
            return db.COURSEs.First(x => id == x.ID);
        }
    }
}
