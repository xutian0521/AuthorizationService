using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Core.Model.Login
{
    public class MvcResult
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public long UserId { get; set; }
        public dynamic Content { get; set; }
    }
}
