// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Guests.Exceptions
{
    public class GuestDependencyException:Xeption
    {
        public GuestDependencyException(Exception innerException)
            :base(message:"Guest dependency error occured,contact support",innerException)
        { }
    }
}
