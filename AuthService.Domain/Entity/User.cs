using AuthService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Domain.Entity
{
    public class User : EntityBase
    {
        public string RealName { get; set; }
        public string UserName { get; set; }

        public string PassWord { get; set; }//密码长度 6~16位


        public string Phone { get; set; }
        public string CompanyName { get; set; }
    }
}
