using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamUserAchievements
{
    public class SteamUserAchievementsResponse
    {
        public SteamUserAchievementsResponse(PlayerAchievementStatsModel playerstats)
        {
            this.playerstats = playerstats;
        }

        public PlayerAchievementStatsModel playerstats { get; private set; }
    }
}
