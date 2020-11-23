using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

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
    }
}
