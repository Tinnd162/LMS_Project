using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.Models;



namespace LMS.Common
{
    public class CommonFunc
    {
        public CommonFunc() { }
        public int GetIdUserBySession()
        {

            User user = new User();
            user = HttpContext.Current.Session[CommonConstants.USER_SESSION] as User;
            return user.id;
        }
    }
}