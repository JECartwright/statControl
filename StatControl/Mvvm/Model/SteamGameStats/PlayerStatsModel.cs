using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamGameStats
{
    public class PlayerStatsModel
    {
        public PlayerStatsModel(string steamID, string gameName, List<StatModel> stats, List<AchievementModel> achievements)
        {
            this.steamID = steamID;
            this.gameName = gameName;
            this.stats = stats;
            this.achievements = achievements;
        }

        public string steamID { get; private set; }
        public string gameName { get; private set; }
        public List<StatModel> stats { get; private set; }
        public List<AchievementModel> achievements { get; private set; }
    }
}
