using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Domain.Base
{
    public class EntityBase
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
