using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
namespace DAL.DAO
{
    public class FacultyDAO
    {
        LMSProjectDBContext db = null;
        public FacultyDAO() { db = new LMSProjectDBContext(); }

        public List<FACULTY> GetListFacultyWithTeacherIn()
        {
            List<FACULTY> listF =  db.FACULTies.ToList();

            List<FACULTY> listFac = listF.Select(f => new FACULTY
                                            {
                                                ID = f.ID,
                                                NAME = f.NAME,
                                                C_USER = f.C_USER.Where(u => u.ROLEs.Where(r => r.ROLE1 == "TEACHER")
                                                                                    .ToList()
                                                                                    .Count() == 1)
                                                                  .Select(u => new C_USER
                                                                    {
                                                                        ID = u.ID,
                                                                        FIRST_NAME = u.FIRST_NAME,
                                                                        ROLEs = u.ROLEs.Select(r => new ROLE { ROLE1 = r.ROLE1})
                                                                                       .ToList()
                                                                    })
                                                                  .ToList()
                                             })
                                            .ToList();
            return listFac;
        }
    }
}
