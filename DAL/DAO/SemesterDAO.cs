using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using System.Data.SqlClient;
using System.Data.Entity.Migrations;
namespace DAL.DAO
{
    public class SemesterDAO
    {
        LMSProjectDBContext db = null;

        public SemesterDAO() { db = new LMSProjectDBContext(); }

        public List<SEMESTER> GetSEMESTERs()
        {
            return db.SEMESTERs.ToList();
        }
        //public List<SEMESTER> getdetail(string id)
        //{
        //    return db.SEMESTERs.Where(x=>x.ID==id).ToList();
        //}
        public bool DelSemester(string id)
        {
            var sem = db.SEMESTERs.Find(id);
            db.SEMESTERs.Remove(sem);
            db.SaveChanges();
            return true;
        }
        public bool UpdateSemester(SEMESTER semester)
        {
            var model = db.SEMESTERs.Find(semester.ID);
            model.TITLE = semester.TITLE;
            model.DESCRIPTION = semester.DESCRIPTION;
            db.SaveChanges();
            return true;
        }
        public bool AddSemester(SEMESTER semester)
        {
            db.SEMESTERs.Add(semester);
            db.SaveChanges();
            return true;
        }
        public SEMESTER GetSemesterByID(string id)
        {
            return db.SEMESTERs.First(x => id == x.ID);
        }
    }
}
