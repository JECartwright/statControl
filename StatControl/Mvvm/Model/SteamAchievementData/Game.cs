using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamAchievementData
{
    internal class Game
    {
        public string gameName { get; private set; }
        public string gameVersion { get; private set; }
        public AvailableGameStats availableGameStats { get; private set; }

        public Game(string gameName, string gameVersion, AvailableGameStats availableGameStats)
        {
            this.gameName = gameName;
            this.gameVersion = gameVersion;
            this.availableGameStats = availableGameStats;
        }
    }
}
