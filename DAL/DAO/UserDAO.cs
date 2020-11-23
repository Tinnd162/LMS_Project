using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using System.Data;
using System.Data.SqlClient;

namespace DAL.DAO
{
    public class UserDAO
    {
        LMSProjectDBContext db = null;
        public UserDAO() { db = new LMSProjectDBContext(); }

        public bool Login(string mail, string password)
        {
            int count = db.C_USER.Count(x => x.MAIL == mail && x.PASSWORD == password);
            if (count > 0)
            {
                return true;
            }
            else return false;
        }
        public C_USER GetUser(string mail)
        {
            return db.C_USER.SingleOrDefault(x => x.MAIL == mail);

        }

        
    }
}
