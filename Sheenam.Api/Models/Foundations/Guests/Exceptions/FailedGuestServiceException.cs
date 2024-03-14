﻿// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Guests.Exceptions
{
    public class FailedGuestServiceException:Xeption
    {
        public FailedGuestServiceException(Exception innerException)
            :base(message:"Failed guest service error occurred,contact support",innerException)
        { }
            
        
    }
}
