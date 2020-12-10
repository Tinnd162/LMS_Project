using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace DAL.DAO
{
    public class DocumentDAO
    {
        LMSProjectDBContext db = null;
        public DocumentDAO() { db = new LMSProjectDBContext(); }

        public bool InsertDocument(DOCUMENT doc)
        {
            try
            {
                db.DOCUMENTs.Add(doc);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDocument(DOCUMENT doc)
        {
            try
            {
                db.DOCUMENTs.AddOrUpdate(doc);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDocument(string id)
        {
            try
            {
                DOCUMENT doc = db.DOCUMENTs.First(x => x.ID == id);
                db.DOCUMENTs.Remove(doc);
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
