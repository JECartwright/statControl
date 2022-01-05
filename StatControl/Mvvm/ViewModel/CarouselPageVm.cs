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
        SteamUserAchievementsResponse _resultUserAchieve;
        SteamUserProfileResponse _resultProfile;
        SteamGameStatsResponse _resultStats;
        SteamAchievementDataResponse _resultAchieveData;
        private readonly IPageServiceZero _pageService;

        public ObservableCollection<ContentPage> Pages { get; set; } = new ObservableCollection<ContentPage>();

        public ICommand TestCommand { get; }


        public CarouselPageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
        }

        internal void Init(SteamUserAchievementsResponse resultUserAchieve, SteamAchievementDataResponse resultAchieveData, SteamUserProfileResponse resultProfile, SteamGameStatsResponse resultStats)
        {
            _resultUserAchieve = resultUserAchieve;
            _resultAchieveData = resultAchieveData;
            _resultProfile = resultProfile;
            _resultStats = resultStats;

            Pages.Add(new HomePage());
            Pages.Add(new MainStatPage());


            Debug.WriteLine("CAROUSEL_PAGE: Sending resultUserAchieve");
            MessagingCenter.Send<CarouselPageVm, SteamUserAchievementsResponse>(this, "resultUserAchieve", _resultUserAchieve);
            Debug.WriteLine("CAROUSEL_PAGE: Sending resultAchieveData");
            MessagingCenter.Send<CarouselPageVm, SteamAchievementDataResponse>(this, "resultAchieveData", _resultAchieveData);
            Debug.WriteLine("CAROUSEL_PAGE: Sending resultProfile");
            MessagingCenter.Send<CarouselPageVm, SteamUserProfileResponse>(this, "resultProfile", _resultProfile);
            Debug.WriteLine("CAROUSEL_PAGE: Sending resultStats");
            MessagingCenter.Send<CarouselPageVm, SteamGameStatsResponse>(this, "resultStats", _resultStats);
            OnPropertyChanged();
        }
    }
}
