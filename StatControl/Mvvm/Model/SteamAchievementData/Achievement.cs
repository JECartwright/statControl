using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamAchievementData
{
    internal class Achievement
    {
        public string name { get; private set; }
        public int defaultvalue { get; private set; }
        public string displayName { get; private set; }
        public int hidden { get; private set; }
        public string description { get; private set; }
        public string icon { get; private set; }
        public string icongray { get; private set; }

        public Achievement(string name, int defaultvalue, string displayName, int hidden, string description, string icon, string icongray)
        {
            this.name = name;
            this.defaultvalue = defaultvalue;
            this.displayName = displayName;
            this.hidden = hidden;
            this.description = description;
            this.icon = icon;
            this.icongray = icongray;
        }
    }
}
