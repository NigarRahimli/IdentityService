using IdentityService.Core.Utilities.Results.Abstract;
using IdentityService.Entities.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Business.Abstract
{
    public interface IUserService
    {
        IResult Login(UserLoginDTO userLogin);
        Task<IResult> Register(UserRegisterDTO userRegister);
        IResult VerifyEmail(string email, string verifyToken);
  

    }
}
