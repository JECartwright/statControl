using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamUserAchievements
{
    public class AchievementModel
    {
        public AchievementModel(string apiname, int achieved, int unlocktime, string name, string description)
        {
            this.apiname = apiname;
            this.achieved = achieved;
            this.unlocktime = unlocktime;
            this.name = name;
            this.description = description;
        }

        public string apiname { get; private set; }
        public int achieved { get; private set; }
        public int unlocktime { get; private set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
