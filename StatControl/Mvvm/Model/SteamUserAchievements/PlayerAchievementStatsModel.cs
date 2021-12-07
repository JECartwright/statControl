using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamUserAchievements
{
    public class PlayerAchievementStatsModel
    {
        public PlayerAchievementStatsModel(string steamID, string gameName, List<AchievementModel> achievements, bool success)
        {
            this.steamID = steamID;
            this.gameName = gameName;
            this.achievements = achievements;
            this.success = success;
        }

        public string steamID { get; private set; }
        public string gameName { get; set; }
        public List<AchievementModel> achievements { get; private set; }
        public bool success { get; private set; }
        
    }
}
