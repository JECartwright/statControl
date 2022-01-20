using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamUserFriends
{
    public class Friend
    {
        public string steamid { get; private set; }
        public string relationship { get; private set; }
        public int friend_since { get; private set; }

        public Friend(string steamid, string relationship, int friend_since)
        {
            this.steamid = steamid;
            this.relationship = relationship;
            this.friend_since = friend_since;
        }
    }
}
