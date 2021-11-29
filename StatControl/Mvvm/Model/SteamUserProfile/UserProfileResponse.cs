using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamUserProfile
{
    public class UserProfileResponse
    {
        public UserProfileResponse(UserProfileResponse response)
        {
            this.response = response;
        }

        public UserProfileResponse response { get; private set; }
    }
}
