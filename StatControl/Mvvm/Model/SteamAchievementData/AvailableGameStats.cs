using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamAchievementData
{
    internal class AvailableGameStats
    {
        public List<Achievement> achievements { get; private set; }

        public AvailableGameStats(List<Achievement> achievements)
        {
            this.achievements = achievements;
        }
    }
}
