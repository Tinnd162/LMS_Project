using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Chưa nhập mail !")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Chưa nhập mật khẩu !")]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}