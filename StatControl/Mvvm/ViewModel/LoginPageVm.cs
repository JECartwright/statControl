using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using StatControl.Mvvm.Model.SteamAchievementData;

namespace StatControl.Mvvm.ViewModel
{
    internal class LoginPageVm : MvvmZeroBaseVm
    {
        private string _steamProfileIdText;

        private readonly SteamGameStatsService _steamGameStatsService;
        private readonly SteamUserAchievementsService _steamUserAchievementsService;
        private readonly SteamUserProfileService _steamUserProfileService;
        private readonly SteamAchievementService _steamAchievementDataService;
        private readonly IPageServiceZero _pageService;
        
        public ICommand HomePageCommand { get; }

        public string SteamProfileIdText
        {
            get => _steamProfileIdText;
            set => SetProperty(ref _steamProfileIdText, value);
        }

        private async Task HomePageCommandExecuteAsync()
        {
            
            var resultUserAchieve = await _steamUserAchievementsService.GetUserAchieveAsync(_steamProfileIdText);
            Debug.WriteLine("User Achievements Received");

            var resultProfile = await _steamUserProfileService.GetUserSummaryAsync(_steamProfileIdText);
            Debug.WriteLine("User Profile Received");

            var resultStats = await _steamGameStatsService.GetUserStatsAsync(_steamProfileIdText);
            Debug.WriteLine("Game Stats Received");

            var resultAchieveData = await _steamAchievementDataService.GetAchieveInfoAsync(_steamProfileIdText);
            Debug.WriteLine("Steam Achievements Received");

            await _pageService.PushPageAsync<CarouselViewPage, CarouselPageVm>((vm) => vm.Init(resultUserAchieve.payload, resultAchieveData.payload, resultProfile.payload, resultStats.payload));

        }

        public LoginPageVm(IPageServiceZero pageService, SteamGameStatsService steamGameStatsService, SteamUserAchievementsService steamUserAchievementsService, SteamUserProfileService steamUserProfileService, SteamAchievementService steamAchievementService)
        {
            _pageService = pageService;
            _steamGameStatsService = steamGameStatsService;
            _steamUserAchievementsService = steamUserAchievementsService;
            _steamUserProfileService = steamUserProfileService;
            _steamAchievementDataService = steamAchievementService;
            SteamProfileIdText = "76561198045733101";

            HomePageCommand = new CommandBuilder().SetExecuteAsync(HomePageCommandExecuteAsync).SetName("Search").Build();
        }
    }
}
