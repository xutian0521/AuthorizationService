using AuthService.Domain.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Core.Utility
{
    public  class LogHelper
    {
        public LogHelper()
        {

        }
        public static MongoClient  client = new MongoClient("mongodb://localhost");

        public static async Task LogInfoAsync(string title, string message, string stackTrace, string tag)
        {
            var info = new LogInfo();
            var logdb= client.GetDatabase("log");
            var collect= logdb.GetCollection<LogInfo>("LogInfo");
            await collect.InsertOneAsync(info);
        }
        public static async Task LogErrorAsync(string title,string message,string stackTrace,string tag)
        {
            LogError info = new LogError();
            var logdb = client.GetDatabase("log");
            var collect = logdb.GetCollection<LogError>("LogError");
            await collect.InsertOneAsync(info);
        }
    }
}
