using System;
using System.Collections.Generic;
using System.Text;
using StatControl.Mvvm.View;
using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using System.Windows.Input;
using System.Threading.Tasks;
using StatControl.Services;
using System.Diagnostics;
using Xamarin.Forms;
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Mvvm.Model.SteamUserAchievements;
using StatControl.Mvvm.Model.SteamGameStats;
using System.ComponentModel;
using System.Collections.ObjectModel;
using StatControl.Mvvm.Model.DisplayModel;


namespace StatControl.Mvvm.ViewModel
{
    internal class MapPageVm : MvvmZeroBaseVm
    {
        private readonly IPageServiceZero _pageService;
        private SteamGameStatsResponse _resultStats;
        private readonly string[] _mapNames;
        public ObservableCollection<MapDisplayModel> MapDisplay { get; private set; }

        public SteamGameStatsResponse ResultStats
        {
            get => _resultStats;
            set
            {
                SetProperty(ref _resultStats, value);
                MapDisplay.Clear();
                foreach (string map in _mapNames)
                {
                    //Debug.WriteLine(map);
                    //Debug.WriteLine(_resultStats.playerstats.stats.Find(x => x.name.Equals($"total_rounds_map_{map}"))?.value ?? 0);
                    //Debug.WriteLine(_resultStats.playerstats.stats.Find(x => x.name.Equals($"total_wins_map_{map}"))?.value ?? 0);
                    MapDisplay.Add(new MapDisplayModel(map,
                        _resultStats.playerstats.stats.Find(x => x.name.Equals($"total_rounds_map_{map}"))?.value ?? 0,
                        _resultStats.playerstats.stats.Find(x => x.name.Equals($"total_wins_map_{map}"))?.value ?? 0));
                }

                OnPropertyChanged();
            }
        }


        public MapPageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
            MapDisplay = new ObservableCollection<MapDisplayModel>();
            _mapNames = new string[]{"ar_baggage",
                                    "ar_monastery",
                                    "ar_shoots",
                                    "cs_assault",
                                    "cs_italy",
                                    "cs_militia",
                                    "cs_office",
                                    "de_aztec",
                                    "de_bank",
                                    "de_cbble",
                                    "de_dust",
                                    "de_dust2",
                                    "de_inferno",
                                    "de_lake",
                                    "de_nuke",
                                    "de_safehouse",
                                    "de_shorttrain",
                                    "de_stmarc",
                                    "de_sugarcane",
                                    "de_train",
                                    "de_vertigo" };
        }
    }
}
