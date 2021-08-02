using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Response
    {
        public int errorCode { get; set; }

        public string  errorMessage { get; set; }

        public string warningMessage { get; set; }

        public int responseCode { get; set; }

        public object data { get; set; }
    }
}
