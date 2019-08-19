using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuthService.Core.Model.Authorize
{
    public class HDTAuthorizeVM
    {
        [Required]
        [DisplayName("序列号")]
        public string MachineCode { get; set; }

        [DisplayName("授权码")]
        public string AuthorizeCode { get; set; }
        [DisplayName("最大连接数")]
        public int ClientMaxCount { get; set; }
        //public int AuthorizationCount { get; set; }
    }
}
