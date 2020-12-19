using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace DAL.DAO
{
    public class EventDAO
    {
        LMSProjectDBContext db = null;
        public EventDAO() { db = new LMSProjectDBContext(); }

        public bool InsertEvent(EVENT e)
        {
            try
            {
                db.EVENTs.Add(e);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateEvent(EVENT e)
        {
            try
            {
                db.EVENTs.AddOrUpdate(e);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteEvent(string id)
        {
            try
            {
                EVENT e = db.EVENTs.First(x => x.ID == id);
                db.EVENTs.Remove(e);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
