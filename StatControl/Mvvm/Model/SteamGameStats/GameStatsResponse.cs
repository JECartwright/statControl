using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamGameStats
{
    public class GameStatsResponse
    {
        public GameStatsResponse(PlayerStatsModel playerstats)
        {
            this.playerstats = playerstats;
        }

        public PlayerStatsModel playerstats { get; set; }
    }
}
