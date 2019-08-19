using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthService.Core.Model.Authorize
{
    /// <summary>
    /// 授权注册验证等 返回结果的viewmodel
    /// </summary>
    public class AuthorizeResult
    {
        public bool IsTrue { get; set; }
        public string Log { get; set; }
    }
}
