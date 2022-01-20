using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamUserFriends
{
    public class SteamFriendsResponce
    {
        public Friendslist friendslist { get; private set; }
        SteamFriendsResponce(Friendslist friendslist)
        {
            this.friendslist = friendslist;
        }
    }
}
