using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamAchievementData
{
    internal class SteamAchievementDataResponse
    {
        public Game game { get; set; }

        public SteamAchievementDataResponse(Game game)
        {
            this.game = game;
        }
    }
}
