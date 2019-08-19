using AuthService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Core.IService
{
    public interface ILogService: IServiceSupport
    {
        Task<List<LogInfo>> GetOrdinaryInfoListAsync();
        Task<List<LogInfo>> GetErrorInfoListAsync();
    }
}
