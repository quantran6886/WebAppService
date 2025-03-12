﻿using System.ComponentModel.DataAnnotations;

namespace WebAppService.Areas.Admin.Modals
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Vui lòng nhập tài khoản.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
