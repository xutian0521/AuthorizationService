using AuthService.Core.Model.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Core.IService
{
    public interface IUserService: IServiceSupport
    {
        Task<MvcResult> UserLoginAsync(string userName, string passWord);
    }
}
