//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Guests.Exceptions
{
    public class GuestValidationException:Xeption
    {
        public GuestValidationException(Xeption innerException)
            :base(message:"Guest Validation error occurred,fix the errors and try again",
                 innerException)
        {
            
        }
    }
}
