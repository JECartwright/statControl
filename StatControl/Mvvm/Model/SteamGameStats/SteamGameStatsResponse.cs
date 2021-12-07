using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamGameStats
{
    public class SteamGameStatsResponse
    {
        public SteamGameStatsResponse(PlayerStatsModel playerstats)
        {
            this.playerstats = playerstats;
        }

        public PlayerStatsModel playerstats { get; private set; }
    }
}
