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
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;
using StatControl.Mvvm.Model.DisplayModel;
using StatControl.Mvvm.Model.SteamUserFriends;
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Mvvm.Model.ApplicationAPIData;
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

        private CarouselPageVm daddy;

        private async void OpenNewUser(SocialProfileDisplayModel current)
        {
            await AplicatationDataHandler.Update(current.ID);
            if (AplicatationDataHandler.CheckAPI)
            {
                daddy.RefreshAll();                
            }
            else
            {
                await AplicatationDataHandler.ReloadMain();
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

        public void DataRefresh()
        {
            if (AplicatationDataHandler.CheckAPI)
            {
                RecivedProfileService = AplicatationDataHandler.GetServiceForSocial();
                Response = AplicatationDataHandler.resultFriends;                
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
