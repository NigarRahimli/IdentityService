using IdentityService.Core.Entities.Abstract;
using IdentityService.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Entities.Entities
{
    public class AppUser : User, IEntity
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool EmailConfirmed { get; set; }
        public string ConfirmationToken { get; set; }
        public int LoginAttempt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
    }
}
