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
using Xamarin.Forms.Core;
using System.ComponentModel;
using System.Collections.ObjectModel;
using StatControl.Mvvm.Model.DisplayModel;
using StatControl.Mvvm.Model.SteamUserFriends;
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Mvvm.Model.ApplicationAPIData;
using Xamarin.Essentials;

namespace StatControl.Mvvm.ViewModel
{
    internal class SocialPageVm : MvvmZeroBaseVm
    {
        public ObservableCollection<SocialProfileDisplayModel> Friends { get; private set; }
        public ObservableCollection<SocialProfileDisplayModel> Points { get; private set; }
        private List<SteamUserProfileResponse> steamUserProfileResponses = new List<SteamUserProfileResponse>();
        private readonly IPageServiceZero _pageService;
        private CarouselPageVm daddy;

        private SteamFriendsResponse _steamFriendsResponse;
        public SteamFriendsResponse steamFriendsResponse
        {
            get { return _steamFriendsResponse; }
            set
            {
                SetProperty(ref _steamFriendsResponse, value);
                DisplayStuff();

                OnPropertyChanged();
            }
        }

        private SteamUserProfileService _steamUserProfileService;
        public SteamUserProfileService steamUserProfileService
        {
            get { return _steamUserProfileService; }
            set
            {
                SetProperty(ref _steamUserProfileService, value);
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
                    OpenNewUser(value.ID);
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
                    OpenNewUser(value.ID);
                }
                SetProperty(ref _selectedProfilePoints, value);
                OnPropertyChanged();
            }
        }

        private async void OpenNewUser(string ID)
        {
            await ApplicatationDataHandler.Update(ID);
            if (ApplicatationDataHandler.CheckAPI)
            {
                daddy.RefreshAll();
            }
            else
            {
                await ApplicatationDataHandler.ReloadMain();
            }
            await Task.Delay(10);
            SelectedProfileFriends = null;
            SelectedProfilePoints = null;

            OnPropertyChanged();
        }

        private async void DisplayStuff()
        {
            for (int b = 0; b < _steamFriendsResponse.friendslist.friends.Count; b++)
            {
                string steamid = _steamFriendsResponse.friendslist.friends[b].steamid;
                var resultProfile = await steamUserProfileService.GetUserSummaryAsync(steamid);
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

            for (int i = 0; i < steamUserProfileResponses.Count; i++)
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
            Debug.WriteLine("Finished Adding Users To Social Page");
        }

        public void DataRefresh()
        {
            if (ApplicatationDataHandler.CheckAPI)
            {
                steamUserProfileService = ApplicatationDataHandler.GetUserProfileServiceForSocial();
                steamFriendsResponse = ApplicatationDataHandler.resultFriends;
            }
            OnPropertyChanged();
        }

        public void getParent(CarouselPageVm dad)
        {
            daddy = dad;
        }

        public SocialPageVm(IPageServiceZero pageService)
        {
            Friends = new ObservableCollection<SocialProfileDisplayModel>();
            Points = new ObservableCollection<SocialProfileDisplayModel>();
            _pageService = pageService;
        }
    }
}
