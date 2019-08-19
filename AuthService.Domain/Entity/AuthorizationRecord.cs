using AuthService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Domain.Entity
{
    public class AuthorizationRecord: EntityBase
    {
        public string AuthorizedCompany { get; set; }
        public string AuthorizedPlatform { get; set; }
        public string MachineCode { get;set;}
        public int TimespanMonth { get; set; }
        public string AuthorizedTimespan { get; set; }

        public string AuthorizeCode { get; set; }


    }
}
