using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Common;
using LMS.Models;
using DAL.DAO;
using System.Security.Cryptography;
using System.Text;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(FormCollection f)
        {
            string email = f["email"].ToString();
            string password = f["password"].ToString();
         
            UserDAO userDao = new UserDAO();
            var res = userDao.Login(email, GetMD5(password));
            if (res)
            {
                var user = userDao.GetUser(email);
                var userModel = new User();
                userModel.id = user.ID;
                userModel.name = user.FIRST_NAME;

                var roleDao = new RoleDAO();
                var listRole = roleDao.GetRoles(user.ID);

                Session.Add(CommonConstants.USER_SESSION, user);
                Session.Add(CommonConstants.ROLE_SESSION, listRole);

                if(listRole.Contains("ADMIN"))
                    return Content("/Admin/Home/Index/" + user.ID);
                else if(listRole.Contains("TEACHER"))
                    return Content("/Teacher/Home/Index/" + user.ID);
                else if (listRole.Contains("STUDENT"))
                    return Content("/Student/Home/Index/" + user.ID);
            }
            return Content("false");
        }

        public string GetMD5(string str)
        {
            str = str + "ahihi_do_ngox";
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {

                sbHash.Append(String.Format("{0:x2}", b));

            }
            return sbHash.ToString();
        }

    }
}