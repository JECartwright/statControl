using System;
using System.Collections.Generic;
using System.Text;
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
using System.ComponentModel;
using System.Collections.ObjectModel;
using StatControl.Mvvm.Model.DisplayModel;
using StatControl.Mvvm.Model.SteamUserFriends;
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Services;
using Xamarin.Essentials;

namespace StatControl.Mvvm.ViewModel
{
    internal class SocialPageVm : MvvmZeroBaseVm
    {
        public ObservableCollection<SocialProfileDisplayModel> Friends { get; private set; }
        public ObservableCollection<SocialProfileDisplayModel> Points { get; private set; }
        private List<SteamUserProfileResponse> steamUserProfileResponses = new List<SteamUserProfileResponse>();
        private readonly IPageServiceZero _pageService;

        private SteamFriendsResponse _response;
        public SteamFriendsResponse Response
        {
            get { return _response; }
            set
            {
                SetProperty(ref _response, value);
                DisplayStuff();



                OnPropertyChanged();
            }
        }

        private SteamUserProfileService _recivedProfileService;
        public SteamUserProfileService RecivedProfileService
        {
            get { return _recivedProfileService; }
            set
            {
                SetProperty(ref _recivedProfileService, value);
            }
        }

        private SteamGameStatsService _recivedGameStatsService;
        public SteamGameStatsService RecivedGameStatsService
        {
            get { return _recivedGameStatsService; }
            set
            {
                SetProperty(ref _recivedGameStatsService, value);
            }
        }

        private SteamUserAchievementsService _recivedAchivementsService;
        public SteamUserAchievementsService RecivedAchivementsService
        {
            get { return _recivedAchivementsService; }
            set
            {
                SetProperty(ref _recivedAchivementsService, value);
            }
        }

        private SteamAchievementService _recivedAchievementDataService;
        public SteamAchievementService RecivedAchievementDataService
        {
            get { return _recivedAchievementDataService; }
            set
            {
                SetProperty(ref _recivedAchievementDataService, value);
            }
        }

        private SteamFriendsService _recivedFreiendsService;
        public SteamFriendsService RecivedFreiendsService
        {
            get { return _recivedFreiendsService; }
            set
            {
                SetProperty(ref _recivedFreiendsService, value);
            }
        }

        SocialProfileDisplayModel _selectedProfileFriends;
        public SocialProfileDisplayModel SelectedProfileFriends
        {
            get => _selectedProfileFriends;
            set
            {

                if (value != null)
                {
                    OpenNewUser(value);
                }
                SetProperty(ref _selectedProfileFriends, value);
                OnPropertyChanged();
            }
        }
        SocialProfileDisplayModel _selectedProfilePoints;
        public SocialProfileDisplayModel SelectedProfilePoints
        {
            get => _selectedProfilePoints;
            set
            {

                if (value != null)
                {
                    OpenNewUser(value);
                }
                SetProperty(ref _selectedProfilePoints, value);
                OnPropertyChanged();
            }
        }

        private async void OpenNewUser(SocialProfileDisplayModel current)
        {
            await Task.Delay(10);
            SelectedProfileFriends = null;
            SelectedProfilePoints = null;
            string _steamProfileIdText = current.ID;
            var resultUserAchieve = await RecivedAchivementsService.GetUserAchieveAsync(_steamProfileIdText);
            Debug.WriteLine("LOGIN_PAGE: User Achievements Response Received");

            var resultProfile = await RecivedProfileService.GetUserSummaryAsync(_steamProfileIdText);
            Debug.WriteLine("LOGIN_PAGE: User Profile Response Received");

            var resultStats = await RecivedGameStatsService.GetUserStatsAsync(_steamProfileIdText);
            Debug.WriteLine("LOGIN_PAGE: Game Stats Response Received");

            var resultAchieveData = await RecivedAchievementDataService.GetAchieveInfoAsync();
            Debug.WriteLine("LOGIN_PAGE: Steam Achievements Response Received");

            var resultFriends = await RecivedFreiendsService.GetFriendsListAsync(_steamProfileIdText);
            Debug.WriteLine("LOGIN_PAGE: Steam Friends Response Received");

            //Checking to see if the response was successful
            if (resultUserAchieve.status == 0 & resultAchieveData.status == 0 & resultProfile.status == 0 & resultStats.status == 0)
            {
                //Checking to see if the response contains data
                if (resultStats.payload.playerstats.stats != null)
                {
                    /*
                    while (this.Navigation.ModalStack.Count > 0)
                    {
                        await this.Navigation.PopModalAsync();
                    }
                    */
                    await _pageService.PushPageAsync<CarouselViewPage, CarouselPageVm>((vm) => vm.Init(resultUserAchieve.payload, resultAchieveData.payload, resultProfile.payload, resultStats.payload, resultFriends.payload, RecivedProfileService, RecivedGameStatsService, RecivedAchivementsService, RecivedAchievementDataService, RecivedFreiendsService));
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
            OnPropertyChanged();

        }

        private async void DisplayStuff()
        {
            for (int b = 0;b < _response.friendslist.friends.Count; b++)
            {
                string steamid = _response.friendslist.friends[b].steamid;
                var resultProfile = await RecivedProfileService.GetUserSummaryAsync(steamid);
                //Checking to see if the response was successful
                if (resultProfile.status == 0)
                {
                    //Checking to see if the response contains data
                    if (resultProfile.payload.response.players != null)
                    {

                        if (resultProfile.payload != null)
                        {
                            steamUserProfileResponses.Add(resultProfile.payload);
                        }
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
            }
            Console.WriteLine("HELLO");
            for(int i = 0;i < steamUserProfileResponses.Count;i++)
            {
                SocialProfileDisplayModel ToAdd = new SocialProfileDisplayModel();
                ToAdd.ID = steamUserProfileResponses[i].response.players[0].steamid;
                ToAdd.Name = steamUserProfileResponses[i].response.players[0].personaname;
                ToAdd.ProfilePicture = steamUserProfileResponses[i].response.players[0].avatarfull;
                ToAdd.Score = "Score: 0";
                Friends.Add(ToAdd);
                Points.Add(ToAdd); // please remove me when you add point system
            }
            OnPropertyChanged();
            Debug.WriteLine("Finished Adding Users To Social Pannel");
        }

        public SocialPageVm(IPageServiceZero pageService)  
        {
            Friends = new ObservableCollection<SocialProfileDisplayModel>();
            Points = new ObservableCollection<SocialProfileDisplayModel>();
            _pageService = pageService;            
        }



    }
}
