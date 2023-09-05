﻿using System;

namespace IdentityService.Core.Utilities.Results.Concrete.SuccessResults
{
    public class SuccessResult : Result
    {
        public SuccessResult() : base(true)
        {
        }

        public SuccessResult(string message) : base(true, message)
        {
        }
    }
}

