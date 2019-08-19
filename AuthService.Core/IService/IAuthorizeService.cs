using AuthService.Core.Model.Authorize;
using AuthService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Core.IService
{
    public interface IAuthorizeService: IServiceSupport
    {
        string GenerateAuthorizationCode(string MCode, TimeSpan span);
        Task<List<AuthorizationRecord>> GetAuthorizationRecordListAsync();
        Task<int> AddAuthorizationRecordAsync(AuthorizeVM vm);
        string GenerateHDTAuthorizationCode(string MCode, int clientMaxCount);
    }
}
