using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LMS.Common
{
    public class CreateID
    {
        public string createID(string code)
        {
            var builder = new StringBuilder();
            builder.Append(code + DateTime.Now.ToString("yyyyMMddHHmmssff"));
            return builder.ToString();
        }
    }
}