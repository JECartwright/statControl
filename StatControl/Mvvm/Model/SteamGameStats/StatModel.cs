using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StatControl.Mvvm.Model.SteamGameStats
{
    public class StatModel
    {
        public StatModel(string name, int value)
        {
            this.name = name;
            this.value = value;
        }

        public string name { get; private set; }

        public int value { get; private set; }

    }
}
