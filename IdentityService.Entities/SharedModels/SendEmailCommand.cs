using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Entities.SharedModels
{
    public class SendEmailCommand
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Token { get; set; }
    }
}
