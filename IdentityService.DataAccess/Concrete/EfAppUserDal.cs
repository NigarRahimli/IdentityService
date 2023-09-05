using IdentityService.Core.DataAccess.EntityFramework;
using IdentityService.DataAccess.Abstract;
using IdentityService.DataAccess.Concrete.EntityFramework;
using IdentityService.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.Concrete
{
    public class EfAppUserDal :  EfRepositoryBase<AppUser, AppDbContext>, IAppUserDal
    {

    }
}
