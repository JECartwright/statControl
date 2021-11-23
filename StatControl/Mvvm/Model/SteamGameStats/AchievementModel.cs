using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamGameStats
{
    public class AchievementModel
    {
        public AchievementModel(string name, int achieved)
        {
            this.name = name;
            this.achieved = achieved;
        }
        public string name { get; set; }
        public int achieved { get; set; }
    }
}
