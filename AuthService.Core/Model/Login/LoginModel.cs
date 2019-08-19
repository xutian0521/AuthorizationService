using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuthService.Core.Model.Login
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Required]
        public string ValidateCode { get; set; }
    }
}
