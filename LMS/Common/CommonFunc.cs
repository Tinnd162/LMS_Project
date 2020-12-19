using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LMS.Models;
using DAL.EF;
using DAL.DAO;


namespace LMS.Common
{
    public class CommonFunc
    {
        public CommonFunc() { }       

        public void SetSession(string userID=null, string courseID=null, string subjectID=null)
        {
            Session s = new Session();
            s.id_user = userID;
            s.id_course = courseID;
            s.id_subject = subjectID;
            HttpContext.Current.Session[CommonConstants.SESSION] = s;
        }

        public string GetIdUserBySession()
        {
            Session s = new Session();
            s =  HttpContext.Current.Session[CommonConstants.SESSION] as Session;
            return s.id_user;
        }
        
        public string GetIdCourseBySession()
        {
            Session s = new Session();
            s = HttpContext.Current.Session[CommonConstants.SESSION] as Session;
            return s.id_course;
        }
        public string GetIdSubjectBySession()
        {
            Session s = new Session();
            s = HttpContext.Current.Session[CommonConstants.SESSION] as Session;
            return s.id_subject;
        }

        public Session GetSession()
        {
            Session s = new Session();
            return HttpContext.Current.Session[CommonConstants.SESSION] as Session;
        }




        //************************Cookie***************************************************




        public void SetCookie(string id_user, string path)
        {
            HttpCookie httpCookie = new HttpCookie(id_user);
            httpCookie.Path = path;
            httpCookie.Expires = DateTime.Now.AddDays(5);
            HttpContext.Current.Response.SetCookie(httpCookie);
        }

        public string GetPathByCookie(string id_user)
        {
            return HttpContext.Current.Request.Cookies[id_user].Path;
        }

        public bool isExistCookie(string id_user)
        {
            if (HttpContext.Current.Response.Cookies.AllKeys.Contains(id_user))
                return true;
            return false;
        }




        //******************************Permission***************************************************

        public bool CheckPermission(string id_user, string id_subject)
        {
            UserDAO userDao = new UserDAO();
            C_USER user = userDao.GetUserByID(id_user);
            if(user.ROLEs.Where(r => r.ROLE1 == "TEACHER").FirstOrDefault() != null)
            {
                if (user.SUBJECTs1.Where(s => s.ID == id_subject).FirstOrDefault() != null)
                    return true;
            }
            else if(user.ROLEs.Where(r => r.ROLE1 == "STUDENT").FirstOrDefault() != null)
            {
                if (user.SUBJECTs.Where(s => s.ID == id_subject).FirstOrDefault() != null)
                    return true;
            }           
            return false;
        }

        public bool checkRole(string id_user, string role)
        {
            RoleDAO roleDao = new RoleDAO();
            if (roleDao.GetRoles(id_user).Where(r => r.ROLE1 == role).FirstOrDefault() != null)
                return true;
            return false;
        }


      
    }
}

