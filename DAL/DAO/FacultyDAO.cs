using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.ViewModel;
namespace DAL.DAO
{
    public class FacultyDAO
    {
        LMSProjectDBContext db = null;
        public FacultyDAO() { db = new LMSProjectDBContext(); }

        public List<FACULTY> getfaculty ()
        {
            return db.FACULTies.ToList();
        }
        public List<FACULTY> getclassinfaculty(string id)
        {
            List<FACULTY> fac = db.FACULTies.Where(a=>a.ID==id).ToList();
            return fac.Select(x => new FACULTY
            {
                CLASSes= x.CLASSes.Select(a=> new CLASS { ID=a.ID, NAME=a.NAME, MAJOR=a.MAJOR}).ToList()
            }).ToList();
        }
    }
}
