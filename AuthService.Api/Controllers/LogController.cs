using AuthService.Core.IService;
using AuthService.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Api.Controllers
{
    [Route("api/[controller]")]
    public class LogController: Controller
    {
        protected ILogService _logService;
        public LogController(ILogService logService)
        {
            this._logService = logService;
        }
        [HttpGet("GetList")]
        public async Task<List<LogInfo>> GetList()
        {
            var list= await _logService.GetOrdinaryInfoListAsync();
            return list;
        }
    }
}
