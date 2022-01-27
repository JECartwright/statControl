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
using StatControl.Mvvm.Model.ApplicationAPIData;
using Xamarin.Essentials;

namespace StatControl.Mvvm.ViewModel
{
    internal class LoginPageVm : MvvmZeroBaseVm
    {
        private string _steamProfileIdText;
        private readonly IPageServiceZero _pageService;

        public ICommand HomePageCommand { get; }

        public string SteamProfileIdText
        {
            get => _steamProfileIdText;
            set => SetProperty(ref _steamProfileIdText, value);
        }

        private async Task HomePageCommandExecuteAsync()
        {
            await AplicatationDataHandler.Update(SteamProfileIdText);
            if (AplicatationDataHandler.CheckAPI)
            {
                await _pageService.PushPageAsync<CarouselViewPage, CarouselPageVm>((vm) => vm.Init());
            }             
        }

        public string privatepolicy = "";

        public bool IsFirstRun 
        {
            get => Preferences.Get(nameof(IsFirstRun), true);
            set => Preferences.Set(nameof(IsFirstRun), value);
        }

        public bool HasAgreed
        {
            get => Preferences.Get(nameof(HasAgreed), false);
            set => Preferences.Set(nameof(HasAgreed), value);
        }

        async void DisplayTerms()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Question?", "Do You Agree To Our Private Policy?\n\r Found At: https://www.privacypolicies.com/live/5b5de50a-58df-4081-8360-4047ead0a94a \n\r Warning Without Agreeing We Cannot Show Your Stats Inprovement \n\r You Can Reboot The App To Select Yes At A Future Point", "Yes", "No");
            if (answer)
            {
                HasAgreed = true;
            }
            await Application.Current.MainPage.DisplayAlert("Alert", "Swipe left and right to navigate", "OK");
        }

        public LoginPageVm(IPageServiceZero pageService, SteamGameStatsService steamGameStatsService, SteamUserAchievementsService steamUserAchievementsService, SteamUserProfileService steamUserProfileService, SteamAchievementService steamAchievementDataService, SteamFriendsService steamFriendsService)
        {
            _pageService = pageService;
            SteamProfileIdText = "76561198045733101";

            if (IsFirstRun || !HasAgreed)
            {
                IsFirstRun = false;
                DisplayTerms();                
            }

            AplicatationDataHandler.SetService(steamGameStatsService,steamUserAchievementsService,steamUserProfileService,steamAchievementDataService,steamFriendsService);

            HomePageCommand = new CommandBuilder().SetExecuteAsync(HomePageCommandExecuteAsync).SetName("Search").Build();
        }
    }
}
