using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AuthService.Domain.Entity
{
    public class LogInfo
    {
        public ObjectId _id { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Massage { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public string StackTrace { get; set; }
    }
    public class LogError
    {
        public ObjectId _id { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Massage { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public string StackTrace { get; set; }

    }

}
