using System.Collections.Concurrent;
using System.Collections.Generic;
using FunctionZero.MvvmZero;
using System.Threading.Tasks;
using StatControl.Services;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;
using StatControl.Mvvm.Model.DisplayModel;
using StatControl.Mvvm.Model.SteamUserFriends;
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Mvvm.Model.ApplicationAPIData;

namespace StatControl.Mvvm.ViewModel
{
    internal class SocialPageVm : MvvmZeroBaseVm
    {
        public ObservableCollection<SocialProfileDisplayModel> Friends { get; private set; }
        public ObservableCollection<SocialProfileDisplayModel> Points { get; private set; }
        private ConcurrentBag<SteamUserProfileResponse> _steamUserProfileResponsesConcurrentBag = new ConcurrentBag<SteamUserProfileResponse>();
        private List<SteamUserProfileResponse> _steamUserProfileResponsesList = new List<SteamUserProfileResponse>();
        private readonly IPageServiceZero _pageService;
        private CarouselPageVm daddy;

        private SteamFriendsResponse _steamFriendsResponse;
        public SteamFriendsResponse SteamFriendsResponse
        {
            get => _steamFriendsResponse;
            set
            {
                SetProperty(ref _steamFriendsResponse, value);
                DisplayStuff();

                OnPropertyChanged();
            }
        }

        private SteamUserProfileService _steamUserProfileService;
        public SteamUserProfileService SteamUserProfileService
        {
            get => _steamUserProfileService;
            set => SetProperty(ref _steamUserProfileService, value);
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
            await ApplicatationDataHandler.Update(current.ID);
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

        private async Task GetFriendData(Friend friend)
        {
            string steamid = friend.steamid;
            var resultProfile = await SteamUserProfileService.GetUserSummaryAsync(steamid);
            //Checking to see if the response was successful
            if (resultProfile.status == 0)
            {
                //Checking to see if the response contains data
                if (resultProfile.payload.response.players != null)
                {
                    _steamUserProfileResponsesConcurrentBag.Add(resultProfile.payload);
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
        
        private async void DisplayStuff()
        {
            var friendsTasks = new List<Task>();
            foreach (var t in _steamFriendsResponse.friendslist.friends)
            {
                var friendsTask = GetFriendData(t);
                friendsTasks.Add(friendsTask);
            }

            await Task.WhenAll(friendsTasks);
            
            _steamUserProfileResponsesList = _steamUserProfileResponsesConcurrentBag.ToList();

            for(int i = 0;i < _steamUserProfileResponsesList.Count;i++)
            {
                SocialProfileDisplayModel toAdd = new SocialProfileDisplayModel
                {
                    ID = _steamUserProfileResponsesList[i].response.players[0].steamid,
                    Name = _steamUserProfileResponsesList[i].response.players[0].personaname,
                    ProfilePicture = _steamUserProfileResponsesList[i].response.players[0].avatarfull,
                    Score = "Score: 0"
                };
                Friends.Add(toAdd);
                Points.Add(toAdd); // please remove me when you add point system
            }
            OnPropertyChanged();
            Debug.WriteLine("Finished Adding Users To Social Page");
        }

        public void DataRefresh()
        {
            if (ApplicatationDataHandler.CheckAPI)
            {
                SteamUserProfileService = ApplicatationDataHandler.GetUserProfileServiceForSocial();
                SteamFriendsResponse = ApplicatationDataHandler.ResultFriends;
            }
            OnPropertyChanged();
        }

        public void GetParent(CarouselPageVm dad)
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
