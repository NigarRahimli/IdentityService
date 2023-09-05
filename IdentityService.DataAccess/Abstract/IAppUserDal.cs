using IdentityService.Core.DataAccess;
using IdentityService.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.Abstract
{
    public interface IAppUserDal:IRepositoryBase<AppUser>
    {
    }
}
