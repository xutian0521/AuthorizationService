using AuthService.Core.IService;
using AuthService.Core.Model.Login;
using AuthService.Data.EntityFramework.Context;
using AuthService.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Core.Utility;
using AuthService.Core.MongoDB;

namespace AuthService.Core.Service
{
    public class UserService: IUserService
    {
        protected MongoDbHelper mongoDbHelper { get; set; }
        private EFContext _dbContext { get; set; }
        protected ConnectionMultiplexer multiplexer { get; set; }

        public UserService(IServiceProvider serviceProvider)
        {
            multiplexer = serviceProvider.GetService<ConnectionMultiplexer>();
            _dbContext = serviceProvider.GetService<EFContext>();
            this.mongoDbHelper = serviceProvider.GetService<MongoDbHelper>();
        }
        public async Task<MvcResult> UserLoginAsync(string userName, string passWord)
        {
            var logintResult = new MvcResult();

            logintResult.Code = 0;
            var db = _dbContext.Set<User>();
            try
            {
                var user = await db.AsNoTracking().Where(t => t.IsDeleted == false && t.UserName == userName)
.FirstOrDefaultAsync();
                if (user == null)
                {
                    logintResult.Message = "不存在该用户！";
                    return logintResult;
                }

                if (user.PassWord != passWord)
                {
                    logintResult.Message = "密码错误！";
                }
                else
                {
                    logintResult.Code = StatusCodes.Status200OK;
                    logintResult.UserId = user.Id;//登录成功，返回该用户的id
                    multiplexer.GetDatabase().StringSet($"Token:{user.Id}", user.ToJson());
                }
            }
            catch (Exception ex)
            {
                await LogHelper.LogErrorAsync("登录异常", ex.Message, ex.StackTrace, "UserLoginAsync");
                throw;
            }


            return logintResult;
        }
    }
}
