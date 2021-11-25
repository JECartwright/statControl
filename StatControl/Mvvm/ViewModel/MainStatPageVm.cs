using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using StatControl.Mvvm.View;
using StatControl.Mvvm.Model.SteamGameStats;
using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Diagnostics;

namespace StatControl.Mvvm.ViewModel
{
    internal class MainStatPageVm : MvvmZeroBaseVm
    {
        public GameStatsResponse GameStatsRespones { get; private set; }
        public MainStatPageVm()
        {
            
        }

        internal void Init(GameStatsResponse payload)
        {
            GameStatsRespones = payload;
            Debug.WriteLine(GameStatsRespones.playerstats.steamID);
        }
    }
}
