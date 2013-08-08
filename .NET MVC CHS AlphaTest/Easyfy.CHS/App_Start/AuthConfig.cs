using System.Collections.Generic;
using DotNetOpenAuth.AspNet.Clients;
using FlexProviders.Membership;

namespace Easyfy.CHS
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            FlexMembershipProvider.RegisterClient(
                new GoogleOpenIdClient(),
                "Google", new Dictionary<string, object>());

            FlexMembershipProvider.RegisterClient(
                new FacebookClient( "148237362000842", "882968c7fa493a4cb7f8c3329c672039"),
                "Facebook", new Dictionary<string, object>());

            FlexMembershipProvider.RegisterClient(
               new TwitterClient("iGbb162BBnTEReV8mDmJQ", "OXGBhZIKI7pK5HVC6Q875zuvHyaSQ3m1HFakSdw6Iw"),
               "Twitter", new Dictionary<string, object>());
        }
    }
}