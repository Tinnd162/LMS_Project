using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LMS.Models;



namespace LMS.Common
{
    public class CommonFunc
    {
        public CommonFunc() { }
        public string GetIdUserBySession()
        {
            User user = new User();
            user = HttpContext.Current.Session[CommonConstants.USER_SESSION] as User;
            return user.id;
        }

        public string RandomID(int size=4)
        {
            Random rd = new Random();
            var builder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                var rdchar = (char)rd.Next('A', 'A' + 26);
                builder.Append(rdchar);
            }
            builder.Append(DateTime.Now.ToString("yyyyMMddHHmmssff"));
            return builder.ToString();
        }

    }
}