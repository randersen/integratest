using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integratest.Security
{
    public class JwtRequest
    {
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
