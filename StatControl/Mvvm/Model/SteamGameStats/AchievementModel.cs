using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamGameStats
{
    public class AchievementModel
    {
        public AchievementModel(string name, bool achieved)
        {
            this.name = name;
            this.achieved = achieved;
        }
        public string name { get; private set; }
        public bool achieved { get; private set; }
    }
}
