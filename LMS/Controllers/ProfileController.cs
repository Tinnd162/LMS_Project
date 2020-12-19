using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL.EF;
using LMS.Common;
using LMS.Models;

namespace LMS.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            CommonFunc comf = new CommonFunc();
            string user_id = comf.GetIdUserBySession();

            UserDAO userDao = new UserDAO();
            C_USER user = userDao.GetUserByID(user_id);
            return View(user);
        }
    }
}