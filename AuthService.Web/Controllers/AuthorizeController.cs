using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthService.Core.Model.Authorize;
using AuthService.Core.Service;
using AuthService.Core.IService;
using AuthService.Core.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Web.Controllers
{
    public class AuthorizeController : Controller
    {
        private IAuthorizeService AuthService { get; set; }
        public AuthorizeController(IServiceProvider serviceProvider)
        {
            this.AuthService = serviceProvider.GetService<IAuthorizeService>();
        }
        public async Task<IActionResult> ListAsync()
        {
            var list =await this.AuthService.GetAuthorizationRecordListAsync();
            return View();
        }

        public IActionResult KeyGenerator()
        {
            var host= this.HttpContext.Request.Host;
            return View();

        }
        public async Task<IActionResult> KeyGeneratorResultAsync(AuthorizeVM model)
        {

            model.AuthorizedTimespan = DateTime.Now.AddMonths(model.TimespanMonth) - DateTime.Now;
            model.AuthorizeCode= this.AuthService.GenerateAuthorizationCode(model.MachineCode, model.AuthorizedTimespan);
            try
            {
                int row = await AuthService.AddAuthorizationRecordAsync(model);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //await LogHelper.LogErrorAsync(new Domain.Entity.LogInfo()
                //{
                //    Title = "同一用户注册，保存数据库异常：",
                //    Tag = "AuthorizeController/KeyGeneratorResultAsync"
                //}, ex);
            }

            //await LogHelper.logInfoAsync(new Domain.Entity.LogInfo() { OrdinaryInfo = model, Title = "同一用户注册成功：", Tag = "AuthorizeController/KeyGeneratorResultAsync" });
            return Json(model);
        }
        public IActionResult HDTKeyGenerator()
        {
            //todo:货代通 软件注册机页面。
            return View();
        }
        public IActionResult HDTKeyGeneratorResult(HDTAuthorizeVM model)
        {
            var code = this.AuthService.GenerateHDTAuthorizationCode(model.MachineCode, model.ClientMaxCount);
            model.AuthorizeCode = code;
            return Json(model);
        }
    }
}