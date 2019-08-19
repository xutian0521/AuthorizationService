using AuthService.Core.IService;
using AuthService.Core.MongoDB;
using AuthService.Core.Utility;
using AuthService.Domain.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Core.Service
{
    public class LogService : ILogService
    {
        internal MongoDbHelper _mongo;
        public LogService(IServiceProvider serviceProvider) 
        {
            this._mongo = serviceProvider.GetService<MongoDbHelper>();
        }
        public async Task<List<LogInfo>> GetOrdinaryInfoListAsync()
        {
            var sort= Builders<LogInfo>.Sort.Descending(p => p.CreateTime);
            var list = await this._mongo.FindPagdeListAsync<LogInfo>("LogInfos", p => p.Tag != "user", sort, 1, 10);
            return list;
        }
        public async Task<List<LogInfo>> GetErrorInfoListAsync()
        {
            var client = LogHelper.client;
            var db = client.GetDatabase("log");
            var collect = db.GetCollection<LogInfo>("LogInfos");
            var filter = Builders<LogInfo>.Filter.Where(p => p.Tag != "user");
            var results = await collect.FindAsync(filter);
            var list = await results.ToListAsync();
            return list;
        }
    }
}
