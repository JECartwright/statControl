using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamVanityUrl
{
    internal class ResponseModel
    {
        public string steamid { get; private set; }
        public int success { get; private set; }

        public ResponseModel(string steamid, int success)
        {
            this.steamid = steamid;
            this.success = success;
        }
    }
}
