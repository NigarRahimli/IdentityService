using AutoMapper;
using IdentityService.Business.Abstract;
using IdentityService.Core.Entities.Concrete;
using IdentityService.Core.Utilities.Business;
using IdentityService.Core.Utilities.Results.Abstract;
using IdentityService.Core.Utilities.Results.Concrete.ErrorResults;
using IdentityService.Core.Utilities.Results.Concrete.SuccessResults;
using IdentityService.Core.Utilities.Security.Hashing;
using IdentityService.Core.Utilities.Security.Jwt;
using IdentityService.DataAccess.Abstract;
using IdentityService.DataAccess.Concrete;
using IdentityService.Entities.DTOs.UserDTOs;
using IdentityService.Entities.Entities;
using IdentityService.Entities.SharedModels;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IAppUserDal _userDal;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public UserManager(IAppUserDal userDal, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _userDal = userDal;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }
        public IResult Login(UserLoginDTO userLogin)
        {


            var result = BusinessRule.CheckRules(
                CheckUserByEmail(userLogin.Email),
                CheckUserPassword(userLogin.Email, userLogin.Password),
                ChekLoginAttempt(userLogin.Email)
                );
            var user = _userDal.Get(x => x.Email == userLogin.Email);
            if (!result.Success)
            {
                user.LoginAttempt += 1;
                _userDal.Update(user);
                return new ErrorResult();
            }

            var token = Token.CreateToken(user, "User");

            return new SuccessResult(token);

        }
        public async Task<IResult> Register(UserRegisterDTO userRegister)
        {
            var result = BusinessRule.CheckRules(CheckUserIsExist(userRegister.Email));
            if (!result.Success)
                return new ErrorResult();

            var mappingUser = _mapper.Map<AppUser>(userRegister);
            byte[] passwordHash, passordSalt;
            PasswordHashing.HashPassword(userRegister.Password, out passwordHash, out passordSalt);
            mappingUser.PasswordHash = passwordHash;
            mappingUser.PasswordSalt = passordSalt;
            mappingUser.ConfirmationToken = Guid.NewGuid().ToString();
            _userDal.Add(mappingUser);
            SendEmailCommand sendEmail = new();
            sendEmail.Email = userRegister.Email;
            sendEmail.Name = userRegister.Name;
            sendEmail.SurName = userRegister.Surname;
            sendEmail.Token = mappingUser.ConfirmationToken;
            _publishEndpoint.Publish<SendEmailCommand>(sendEmail);
            return new SuccessResult();
        }
  


        private IResult CheckUserEmailIsExist(string email)
        {
            var user = _userDal.Get(z => z.Email == email);
            if (user is not null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        public IResult VerifyEmail(string email, string verifyToken)
        {
            var user = _userDal.Get(x => x.Email == email);

            if (user.ConfirmationToken == verifyToken)
            {
                user.EmailConfirmed = true;
                _userDal.Update(user);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        private IResult CheckUserIsExist(string email)
        {
            var user = _userDal.Get(x => x.Email == email);
            if (user is not null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }


        private IResult CheckUserByEmail(string email)
        {
            var user = _userDal.Get(x => x.Email == email);
            if (user is null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();

        }


        private IResult CheckUserPassword(string email, string password)
        {
            var user = _userDal.Get(x => x.Email == email);
            bool checkPassword = PasswordHashing.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);

            if (!checkPassword)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IResult ChekLoginAttempt(string email)
        {
            var user = _userDal.Get(x => x.Email == email);
            if (user.LoginAttempt < 3)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

    }
}

