using IdentityService.Core.Utilities.Results.Abstract;
using IdentityService.Core.Utilities.Results.Concrete.ErrorResults;
using IdentityService.Core.Utilities.Results.Concrete.SuccessResults;
using System;

namespace IdentityService.Core.Utilities.Business
{
    public class BusinessRule
    {
        public static IResult CheckRules(params IResult[] logic)
        {
            foreach (var method in logic)
            {
                if (!method.Success)
                {
                    return new ErrorResult();
                }
            }
            return new SuccessResult();
        }
    }
}

