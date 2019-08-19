using AuthService.Core.IService;
using AuthService.Core.Model.Authorize;
using AuthService.Core.Utility;
using AuthService.Data.EntityFramework.Context;
using AuthService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Core.Service
{
    public class AuthorizeService: IAuthorizeService
    {
        private EFContext _dbContext;

        public AuthorizeService(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetService<EFContext>();
        }

        public string GenerateAuthorizationCode(string MCode, TimeSpan span)
        { 
            ServiceAuthorizeHelper helper = new ServiceAuthorizeHelper();

            return helper.GetAuthorizeCode(MCode, span);
        }
        public async Task<List<AuthorizationRecord>> GetAuthorizationRecordListAsync()
        {
           var list= await this._dbContext.Set<AuthorizationRecord>().Where(t => t.IsDeleted == false).ToListAsync();

           return list;
        }
        public async Task<int> AddAuthorizationRecordAsync(AuthorizeVM vm)
        {
            var RecordDb = this._dbContext.Set<AuthorizationRecord>();
            var firstRecord =await RecordDb.AsNoTracking().Where(t => t.IsDeleted == false && t.AuthorizeCode == vm.AuthorizeCode).FirstOrDefaultAsync();
            if (firstRecord != null)
            {
                return -1;
            }
            var record = new AuthorizationRecord()
            {
                AuthorizeCode = vm.AuthorizeCode,
                AuthorizedTimespan = vm.AuthorizedTimespan.ToString(),
                CreateDate = DateTime.Now,
                IsDeleted = false,
                MachineCode = vm.MachineCode,
                TimespanMonth = vm.TimespanMonth
            };

            await RecordDb.AddAsync(record);

            int row =await this._dbContext.SaveChangesAsync();

            return row;
        }
        public string GenerateHDTAuthorizationCode(string MCode, int clientMaxCount)
        {
            string DTDAuthorizeCode = null;
            HDTAuthorizeHelper helper = new HDTAuthorizeHelper();
            try
            {
                //硬件码(注册码)
                var time_ = helper.GetTailFlagToInt(MCode); //软件注册次数
                var timeCode = EncryptUtil.Base64Encrypt((time_ + 1).ToString());  //加密后的次数
                var connNum_ = clientMaxCount;  

                var connNumCode = EncryptUtil.Base64Encrypt(connNum_.ToString()); //加密后的连接数
                string strLicence = helper.GetRNum(MCode) + timeCode + connNumCode; //注册码 = 根据机器生产的注册码 + 注册次数码 + 最大连接数码
                DTDAuthorizeCode = strLicence;
            }
            catch (System.Exception)
            {
                //"输入的机器码格式错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return DTDAuthorizeCode;
        }
    }
}
