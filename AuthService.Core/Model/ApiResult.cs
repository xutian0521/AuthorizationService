using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Core.Model
{
    public class ApiResult
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public dynamic Content { get; set; }
    }
}
