using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthService.Core.Utility;
using AuthService.Core.IService;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Web.Controllers
{
    public class LogController : Controller
    {
        protected ILogService _logService;
        public LogController(IServiceProvider serviceProvider)
        {
            this._logService = serviceProvider.GetService<ILogService>();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var list=await _logService.GetOrdinaryInfoListAsync();
            return View(list);
        }
    }
}