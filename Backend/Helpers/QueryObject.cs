using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Helpers
{
    public class QueryObject
    {
        public string? auditTitle {get;set;} = null;

        public string? sortby {get;set;} = null;

        public bool isDecsending {get;set;} = false;

        public int pageNumber {get;set;} = 1;

        public int  pageSize {get;set;} = 10;        

    }
}