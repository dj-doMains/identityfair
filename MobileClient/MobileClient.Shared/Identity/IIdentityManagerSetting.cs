using System;
namespace MobileClient.Shared.Identity
{
    public interface IIdentityManagerSetting
    {
        string ClientID { get; set; }
        string TokenEndpoint { get; set; }
    }
}
