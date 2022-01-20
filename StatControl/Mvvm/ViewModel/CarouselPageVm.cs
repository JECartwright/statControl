using StatControl.Mvvm.Model.SteamGameStats;
using StatControl.Mvvm.Model.SteamUserAchievements;
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Mvvm.Model.SteamAchievementData;
using StatControl.Mvvm.Model.SteamUserFriends;
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

namespace StatControl.Mvvm.ViewModel
{
    internal class CarouselPageVm : CarouselPage
    {
        SteamUserAchievementsResponse _resultUserAchieve;
        SteamUserProfileResponse _resultProfile;
        SteamGameStatsResponse _resultStats;
        SteamAchievementDataResponse _resultAchieveData;
        Root _resultFriends;
        private readonly IPageServiceZero _pageService;
        public ICommand TestCommand { get; }


        public CarouselPageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
        }

        internal void Init(SteamUserAchievementsResponse resultUserAchieve, SteamAchievementDataResponse resultAchieveData, SteamUserProfileResponse resultProfile, SteamGameStatsResponse resultStats, Root resultFriends)
        {
            _resultUserAchieve = resultUserAchieve;
            _resultAchieveData = resultAchieveData;
            _resultProfile = resultProfile;
            _resultStats = resultStats;
            _resultFriends = resultFriends;

            Debug.WriteLine("Sending resultUserAchieve");
            MessagingCenter.Send<CarouselPageVm, SteamUserAchievementsResponse>(this, "resultUserAchieve", _resultUserAchieve);
            Debug.WriteLine("Sending resultAchieveData");
            MessagingCenter.Send<CarouselPageVm, SteamAchievementDataResponse>(this, "resultAchieveData", _resultAchieveData);
            Debug.WriteLine("Sending resultProfile");
            MessagingCenter.Send<CarouselPageVm, SteamUserProfileResponse>(this, "resultProfile", _resultProfile);
            Debug.WriteLine("Sending resultStats");
            MessagingCenter.Send<CarouselPageVm, SteamGameStatsResponse>(this, "resultStats", _resultStats);
            OnPropertyChanged();
        }
    }
}
