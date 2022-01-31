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
        private readonly Dictionary<string, String> _mapImageDictionary;
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
                        _resultStats.playerstats.stats.Find(x => x.name.Equals($"total_wins_map_{map}"))?.value ?? 0,
                        _mapImageDictionary[map]));
                }

                OnPropertyChanged();
            }
        }

        public void DataRefresh()
        {
            if (AplicatationDataHandler.CheckAPI)
            {
                ResultStats = AplicatationDataHandler.resultStats;
            }
            OnPropertyChanged();
        }

        public MapPageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
            MapDisplay = new ObservableCollection<MapDisplayModel>();
            _mapImageDictionary = new Dictionary<string, String>()
            {
                    {"ar_baggage", "https://static.wikia.nocookie.net/cswikia/images/8/8e/Ar_baggage.jpg"},
                    {"ar_monastery", "https://static.wikia.nocookie.net/cswikia/images/e/e1/Ar_monastery.png"},
                    {"ar_shoots", "https://static.wikia.nocookie.net/cswikia/images/2/23/Shoots-overview.png"},
                    {"cs_assault", "https://static.wikia.nocookie.net/cswikia/images/0/00/Cs_assault_go.png"},
                    {"cs_italy", "https://static.wikia.nocookie.net/cswikia/images/2/2c/Cs_italy_csgo.png"},
                    {"cs_militia", "https://static.wikia.nocookie.net/cswikia/images/2/2b/Csgo_militia_pic1.jpg"},
                    {"cs_office", "https://static.wikia.nocookie.net/cswikia/images/f/f7/Csgo-cs-office.png"},
                    {"de_aztec", "https://static.wikia.nocookie.net/cswikia/images/f/fd/Csgo-de-aztec.png"},
                    {"de_bank", "https://static.wikia.nocookie.net/cswikia/images/a/a9/Csgo-de-bank.png"},
                    {"de_cbble", "https://static.wikia.nocookie.net/cswikia/images/f/f1/De_cbble_loading_screen.jpg"},
                    {"de_dust", "https://static.wikia.nocookie.net/cswikia/images/6/6d/Csgo-de-dust.png"},
                    {"de_dust2", "https://static.wikia.nocookie.net/cswikia/images/4/42/Dust2_asite1.png"},
                    {"de_inferno", "https://static.wikia.nocookie.net/cswikia/images/f/f0/Inferno.jpg"},
                    {"de_lake", "https://static.wikia.nocookie.net/cswikia/images/0/08/Csgo-de-lake.png"},
                    {"de_nuke", "https://static.wikia.nocookie.net/cswikia/images/9/93/CSGO_Nuke_22_Nov_2019_update_picture_1.jpg"},
                    {"de_safehouse", "https://static.wikia.nocookie.net/cswikia/images/2/27/Csgo-de-safehouse.png"},
                    {"de_shorttrain", "https://static.wikia.nocookie.net/cswikia/images/4/4b/De_shorttrain_thumbnail.jpg"},
                    {"de_stmarc", "https://static.wikia.nocookie.net/cswikia/images/8/83/Stmarc_tspawn.png"},
                    {"de_sugarcane", "https://static.wikia.nocookie.net/cswikia/images/c/c7/Csgo-de-sugarcane.png"},
                    {"de_train", "https://static.wikia.nocookie.net/cswikia/images/4/4a/De_train_thumbnail.png"},
                    {"de_vertigo", "https://static.wikia.nocookie.net/cswikia/images/a/a5/Vertigo-b-site-overview.png"}
            };

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
