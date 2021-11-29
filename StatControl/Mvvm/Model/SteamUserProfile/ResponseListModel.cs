using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamUserProfile
{
    internal class ResponseListModel
    {
        public ResponseListModel(List<UserModel> players)
        {
            this.players = players;
        }

        public List<UserModel> players { get; private set; }
    }
}
