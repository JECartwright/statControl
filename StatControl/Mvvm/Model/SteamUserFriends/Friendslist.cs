using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamUserFriends
{
    public class Friendslist
    {
        public List<Friend> friends { get; private set; }

        public Friendslist(List<Friend> friends)
        {
            this.friends = friends;
        }
    }
}
