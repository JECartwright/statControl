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

namespace StatControl.Mvvm.ViewModel
{
    internal class LoginPageVm : MvvmZeroBaseVm
    {
        private string _steamProfileIdText;

        private readonly SteamGameStatsService _steamGameStatsService;
        private readonly SteamUserAchievementsService _steamUserAchievementsService;
        private readonly SteamUserProfileService _steamUserProfileService;
        private readonly IPageServiceZero _pageService;
        
        public ICommand HomePageCommand { get; }

        public string SteamProfileIdText
        {
            get => _steamProfileIdText;
            set => SetProperty(ref _steamProfileIdText, value);
        }

        private async Task HomePageCommandExecuteAsync()
        {
            
            var resultAchieve = await _steamUserAchievementsService.GetUserAchieveAsync(_steamProfileIdText);
            var resultProfile = await _steamUserProfileService.GetUserSummaryAsync(_steamProfileIdText);
            var resultStats = await _steamGameStatsService.GetUserStatsAsync(_steamProfileIdText);

            if (resultAchieve.status == 0 & resultProfile.status == 0 & resultStats.status == 0)
            {
                await _pageService.PushPageAsync<CarouselViewPage, CarouselPageVm>((vm) => vm.Init(resultAchieve.payload, resultProfile.payload, resultStats.payload));
            }
            else
            {
                Debug.WriteLine("Error in getting API");
            }
            

        }

        public LoginPageVm(IPageServiceZero pageService, SteamGameStatsService steamGameStatsService, SteamUserAchievementsService steamUserAchievementsService, SteamUserProfileService steamUserProfileService)
        {
            _pageService = pageService;
            _steamGameStatsService = steamGameStatsService;
            _steamUserAchievementsService = steamUserAchievementsService;
            _steamUserProfileService = steamUserProfileService;
            SteamProfileIdText = "76561198045733101";

            HomePageCommand = new CommandBuilder().SetExecuteAsync(HomePageCommandExecuteAsync).SetName("Search").Build();
        }
    }
}
