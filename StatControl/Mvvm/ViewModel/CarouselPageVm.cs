using StatControl.Mvvm.Model.SteamGameStats;
using StatControl.Mvvm.Model.SteamUserAchievements;
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Mvvm.Model.SteamAchievementData;
using StatControl.Mvvm.View;
using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using System.Windows.Input;
using System.Threading.Tasks;
using StatControl.Services.Rest;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace StatControl.Mvvm.ViewModel
{
    internal class CarouselPageVm : CarouselPage
    {
        private readonly IPageServiceZero _pageService;
        SteamUserAchievementsResponse _resultUserAchieve;
        SteamUserProfileResponse _resultProfile;
        SteamGameStatsResponse _resultStats;
        SteamAchievementDataResponse _resultAchieveData;
        public ICommand TestCommand { get; }

        public HomePageVm HomeVm { get; private set; }
        public MainStatPageVm MainVm { get; private set; }
        public LastMatchPageVm LastVm { get; private set; }
        public WeaponsSelectPageVm WeaSelectVm { get; private set; }
        public MapPageVm MapVm { get; private set; }
        public FunPageVm FunVm { get; private set; }
        public AchievementsPageVm AchieveVm { get; private set; }


        public CarouselPageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
            HomeVm = new HomePageVm(_pageService);
            MainVm = new MainStatPageVm(_pageService);
            LastVm = new LastMatchPageVm(_pageService);
            WeaSelectVm = new WeaponsSelectPageVm(_pageService);
            MapVm = new MapPageVm(_pageService);
            FunVm = new FunPageVm(_pageService);
            AchieveVm = new AchievementsPageVm(_pageService);
        }

        internal void Init(SteamUserAchievementsResponse resultUserAchieve, SteamAchievementDataResponse resultAchieveData, SteamUserProfileResponse resultProfile, SteamGameStatsResponse resultStats)
        {
            _resultUserAchieve = resultUserAchieve;
            _resultAchieveData = resultAchieveData;
            _resultProfile = resultProfile;
            _resultStats = resultStats;

            HomeVm.ResultProfile = _resultProfile;
            MainVm.ResultStats = _resultStats;
            LastVm.ResultStats = _resultStats;
            WeaSelectVm.ResultStats = _resultStats;
            MapVm.ResultStats = _resultStats;
            FunVm.ResultStats = _resultStats;

            AchieveVm.ResultUserAchieve = _resultUserAchieve;
            AchieveVm.ResultAchieveData = _resultAchieveData;



            OnPropertyChanged();
        }
    }
}
