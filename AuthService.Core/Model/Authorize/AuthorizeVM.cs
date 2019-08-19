using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuthService.Core.Model.Authorize
{
    public class AuthorizeVM
    {
        [Required]
        [DisplayName("平台识别码")]
        public string MachineCode { get; set; }
        [DisplayName("授权时长(月)")]
        public int TimespanMonth { get; set; }
        [DisplayName("授权时长")]
        public TimeSpan AuthorizedTimespan { get; set; }
        [DisplayName("授权码")]
        public string AuthorizeCode { get; set; }

    }
}
