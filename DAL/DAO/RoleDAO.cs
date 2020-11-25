using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.DAO
{
    public class RoleDAO
    {
        LMSProjectDBContext db = null;

        public RoleDAO() { db = new LMSProjectDBContext(); }


        public List<string> GetRoles(string user_id)
        {
            object[] sqlParams =
            {
                new SqlParameter("@user_id", user_id)
            };
            var roles = db.Database.SqlQuery<string>("sp_GET_ROLES @user_id", sqlParams).ToList();
            return roles;
        }
    }
}
