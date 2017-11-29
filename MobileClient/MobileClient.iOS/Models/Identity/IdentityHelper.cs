using System;
using Foundation;
using MobileClient.Shared.Identity;

namespace MobileClient.Models.Identity
{
    public static class IdentityHelper
    {
        public static IdentityManagerSettings CreateIdentityManagerSettings()
        {
            return new IdentityManagerSettings()
            {
                ClientID = NSBundle.MainBundle.ObjectForInfoDictionary("ClientID")?.ToString(),

                TokenEndpoint = NSBundle.MainBundle.ObjectForInfoDictionary("TokenEndpoint")?.ToString()
            };
        }
    }
}
