using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamUserFriends
{
    public class SteamFriendsResponse
    {
        public SteamFriendsResponse(Friendslist friendslist)
        {
            this.friendslist = friendslist;
        }

        public Friendslist friendslist { get; private set; }
    }
}
