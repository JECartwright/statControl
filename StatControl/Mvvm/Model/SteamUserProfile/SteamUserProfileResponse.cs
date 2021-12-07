using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamUserProfile
{
    public class SteamUserProfileResponse
    {
        public SteamUserProfileResponse(ResponseListModel response)
        {
            this.response = response;
        }

        public ResponseListModel response { get; private set; }
    }
}
