using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.DisplayModel
{
    internal class MapDisplayModel
    {
        public string ApiName { get; set; }
        public string TotalRounds { get; set; }
        public string TotalRoundsWon { get; set; }

        public MapDisplayModel(String apiName, int totalRounds, int totalRoundsWon)
        {
            ApiName = apiName;
            TotalRounds = totalRounds.ToString();
            TotalRoundsWon = totalRoundsWon.ToString();
        }
    }
}
