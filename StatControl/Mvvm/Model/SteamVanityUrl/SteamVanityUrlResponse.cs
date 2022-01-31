using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamVanityUrl
{
    internal class SteamVanityUrlResponse
    {
        public ResponseModel response { get; private set; }

        public SteamVanityUrlResponse(ResponseModel response)
        {
            this.response = response;
        }
    }
}
