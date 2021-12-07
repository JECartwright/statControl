using StatControl.Mvvm.Model.SteamGameStats;
using StatControl.Mvvm.Model.SteamUserAchievements;
using StatControl.Mvvm.Model.SteamUserProfile;
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
        SteamUserAchievementsResponse _resultAchieve;
        SteamUserProfileResponse _resultProfile;
        SteamGameStatsResponse _resultStats;
        private readonly IPageServiceZero _pageService;
        public ICommand TestCommand { get; }


        public CarouselPageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
        }

        internal void Init(SteamUserAchievementsResponse resultAchieve, SteamUserProfileResponse resultProfile, SteamGameStatsResponse resultStats)
        {
            _resultAchieve = resultAchieve;
            _resultProfile = resultProfile;
            _resultStats = resultStats;

            //Need to initialise all the view models with api calls
            Debug.WriteLine("Sending resultAchieve");
            MessagingCenter.Send<CarouselPageVm, SteamUserAchievementsResponse>(this, "resultAchieve", _resultAchieve);
            Debug.WriteLine("Sending resultProfile");
            MessagingCenter.Send<CarouselPageVm, SteamUserProfileResponse>(this, "resultProfile", _resultProfile);
            Debug.WriteLine("Sending resultStats");
            MessagingCenter.Send<CarouselPageVm, SteamGameStatsResponse>(this, "resultStats", _resultStats);
            OnPropertyChanged();
            
        }
    }
}
