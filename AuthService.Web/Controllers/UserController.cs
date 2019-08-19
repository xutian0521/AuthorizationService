using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthService.Core.IService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using AuthService.Core.Model.Login;
using StackExchange.Redis;

namespace AuthService.Web.Controllers
{
    public class UserController : Controller
    {
        protected IUserService userService { get; set; }
        protected ConnectionMultiplexer multiplexer { get; set; }
        public UserController(IServiceProvider serviceProvider)
        {
            this.userService = serviceProvider.GetService<IUserService>();
            multiplexer = serviceProvider.GetService<ConnectionMultiplexer>();
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {


            var result = await userService.UserLoginAsync(model.UserName, model.PassWord);
            if (result.Code == StatusCodes.Status200OK)
            {
                base.HttpContext.Response.Cookies.Append($"Token", result.UserId.ToString());

            }

            return Json(result);
        }
    }
}