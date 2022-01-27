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
using StatControl.Mvvm.Model.SteamVanityUrl;
using Xamarin.Essentials;
using System.Text.RegularExpressions;

namespace StatControl.Mvvm.ViewModel
{
    internal class LoginPageVm : MvvmZeroBaseVm
    {
        private string _steamProfileIdText;
        
        private readonly SteamGameStatsService _steamGameStatsService;
        private readonly SteamUserAchievementsService _steamUserAchievementsService;
        private readonly SteamUserProfileService _steamUserProfileService;
        private readonly SteamAchievementService _steamAchievementDataService;
        private readonly SteamVanityUrlService _steamVanityUrlService;
        private readonly SteamFriendsService _steamFriendsService;
        private readonly IPageServiceZero _pageService;

        private string _errorMsgText;
        private string _errorMsgTextVisible;

        public string ErrorMsgText
        {
            get => _errorMsgText;
            set
            {
                SetProperty(ref _errorMsgText, value);
            }
        }
        
        public string ErrorMsgTextVisible
        {
            get => _errorMsgText;
            set
            {
                SetProperty(ref _errorMsgText, value);
            }
        }

        public ICommand HomePageCommand { get; }

        public string SteamProfileIdText
        {
            get => _steamProfileIdText;
            set => SetProperty(ref _steamProfileIdText, value);
        }

        private async Task HomePageCommandExecuteAsync()
        {
            await GetIdTypeAsync(SteamProfileIdText);

            //Getting Data
            var resultUserAchieve = await _steamUserAchievementsService.GetUserAchieveAsync(SteamProfileIdText);
            Debug.WriteLine("LOGIN_PAGE: User Achievements Response Received");       

            var resultProfile = await _steamUserProfileService.GetUserSummaryAsync(SteamProfileIdText);
            Debug.WriteLine("LOGIN_PAGE: User Profile Response Received");

            var resultStats = await _steamGameStatsService.GetUserStatsAsync(SteamProfileIdText);
            Debug.WriteLine("LOGIN_PAGE: Game Stats Response Received");

            var resultAchieveData = await _steamAchievementDataService.GetAchieveInfoAsync();
            Debug.WriteLine("LOGIN_PAGE: Steam Achievements Response Received");
            //

            //Checking to see if the response was successful
            if (resultUserAchieve.status == 0 & resultAchieveData.status == 0 & resultProfile.status == 0 & resultStats.status == 0)
            {
                //Checking to see if the response contains data
                if (resultStats.payload.playerstats.stats != null)
                {
                    ErrorMsgTextVisible = "False";
                    await _pageService.PushPageAsync<CarouselViewPage, CarouselPageVm>((vm) => vm.Init(resultUserAchieve.payload, resultAchieveData.payload, resultProfile.payload, resultStats.payload));
                }
                else
                {
                    ErrorMsgText = "The Data Received From The API Is Null\nPlease Try Again.";
                    ErrorMsgTextVisible = "True";
                    Debug.WriteLine("Data Returned is null.");
                }
            }
            else
            {
                ErrorMsgText = "Error In Trying To Retrieve Data From The API\nPlease Try Again.";
                ErrorMsgTextVisible = "True";
                Debug.WriteLine("Error in getting API.");
            }
        }

        //Check to see what type the steam ID is
        private async Task GetIdTypeAsync(string id)
        {
            string pattern = @"7656119[0-9]{10}";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(id);
            var resultFriends = await _steamFriendsService.GetFriendsListAsync(_steamProfileIdText);
            Debug.WriteLine("LOGIN_PAGE: Steam Friends Response Received");

            //Checking to see if the response was successful
            if (resultUserAchieve.status == 0 & resultAchieveData.status == 0 & resultProfile.status == 0 & resultStats.status == 0)
            {
                //Checking to see if the response contains data
                if (resultStats.payload.playerstats.stats != null)
                {
                    await _pageService.PushPageAsync<CarouselViewPage, CarouselPageVm>((vm) => vm.Init(resultUserAchieve.payload, resultAchieveData.payload, resultProfile.payload, resultStats.payload, resultFriends.payload, _steamUserProfileService, _steamGameStatsService, _steamUserAchievementsService, _steamAchievementDataService, _steamFriendsService));
                }
                else
                {
                    Debug.WriteLine("Data Returned is null.");
                }
            }
            else
            {
                Debug.WriteLine("Error in getting API.");
            }
            

            if (!m.Success) //if input is not Steam64 convert
            {
                var resultVantityUrl = await _steamVanityUrlService.GetVanityUrlSummaryAsync(id);
                SteamProfileIdText = resultVantityUrl.payload.response.steamid;
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
            _steamGameStatsService = steamGameStatsService;
            _steamUserAchievementsService = steamUserAchievementsService;
            _steamUserProfileService = steamUserProfileService;
            _steamAchievementDataService = steamAchievementDataService;
            _steamVanityUrlService = steamVanityUrlService;

            ErrorMsgTextVisible = "False";

            _steamFriendsService = steamFriendsService;

            SteamProfileIdText = "76561198045733101";

            if (IsFirstRun || !HasAgreed)
            {
                IsFirstRun = false;
                DisplayTerms();                
            }

            HomePageCommand = new CommandBuilder().SetExecuteAsync(HomePageCommandExecuteAsync).SetName("Search").Build();
        }
    }
}
