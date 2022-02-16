using System.Collections.Concurrent;
using System.Collections.Generic;
using FunctionZero.MvvmZero;
using System.Threading.Tasks;
using StatControl.Services;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FunctionZero.CommandZero;
using StatControl.Mvvm.Model.DisplayModel;
using StatControl.Mvvm.Model.SteamUserFriends;
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Mvvm.Model.ApplicationAPIData;
using Xamarin.Forms;

namespace StatControl.Mvvm.ViewModel
{
    internal class SocialPageVm : MvvmZeroBaseVm
    {
        private ObservableCollection<SocialProfileDisplayModel> _friends;
        public ObservableCollection<SocialProfileDisplayModel> Friends {
            get => _friends;
            set => SetProperty(ref _friends, value);
            
        }
        
        private string _steamProfileIdText;
        public string SteamProfileIdText
        {
            get => _steamProfileIdText;
            set => SetProperty(ref _steamProfileIdText, value);
        }

        private ObservableCollection<SocialProfileDisplayModel> _points;
        public ObservableCollection<SocialProfileDisplayModel> Points {
            get => _points;
            set => SetProperty(ref _points, value);
        }
        private ConcurrentBag<SteamUserProfileResponse> _steamUserProfileResponsesConcurrentBag = new ConcurrentBag<SteamUserProfileResponse>();
        private ConcurrentBag<SocialProfileDisplayModel> _friendsConcurrentBag = new ConcurrentBag<SocialProfileDisplayModel>();
        private ConcurrentBag<SocialProfileDisplayModel> _pointsConcurrentBag = new ConcurrentBag<SocialProfileDisplayModel>();
        private List<SteamUserProfileResponse> _steamUserProfileResponsesList = new List<SteamUserProfileResponse>();
        private readonly IPageServiceZero _pageService;
        private CarouselPageVm _daddy;

        public ICommand SearchCommand { get; }
        private Task SearchCommandExecuteAsync()
        {
            OpenNewUser(SteamProfileIdText);
            OnPropertyChanged();
            return Task.CompletedTask;
        }
        
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

        private SocialProfileDisplayModel _selectedProfileFriends;
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

        private SocialProfileDisplayModel _selectedProfilePoints;
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

        private async void OpenNewUser(string id)
        {
            await ApplicatationDataHandler.Update(id);
            if (ApplicatationDataHandler.CheckAPI)
            {
                _daddy.RefreshAll();
                await Application.Current.MainPage.DisplayAlert("Alert", "Successfully Selected User's Data.\nPlease Swipe Right.", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Error In Getting Selected User's Data.\nTheir Profile May Be Private.", "OK");
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
            var tasks = new List<Task>();
            foreach (var friend in _steamFriendsResponse.friendslist.friends)
            {
                var friendsTask = GetFriendData(friend);
                tasks.Add(friendsTask);
            }
            await Task.WhenAll(tasks);
            _steamUserProfileResponsesList = _steamUserProfileResponsesConcurrentBag.ToList();

            Parallel.ForEach(_steamUserProfileResponsesList, t =>
            {
                SocialProfileDisplayModel toAdd = new SocialProfileDisplayModel
                {
                    ID = t.response.players[0].steamid,
                    Name = t.response.players[0].personaname,
                    ProfilePicture = t.response.players[0].avatarfull,
                    Score = "Score: 0"
                };
                _friendsConcurrentBag.Add(toAdd);
                _pointsConcurrentBag.Add(toAdd); // please remove me when you add point system
            });
            

            Friends = new ObservableCollection<SocialProfileDisplayModel>(_friendsConcurrentBag.ToList());
            Points = new ObservableCollection<SocialProfileDisplayModel>(_pointsConcurrentBag.ToList());
            
            OnPropertyChanged();
            Debug.WriteLine("Finished Adding Users To Social Page");
        }

        public void DataRefresh()
        {
            if (ApplicatationDataHandler.CheckAPI)
            {
                SteamUserProfileService = ApplicatationDataHandler.GetUserProfileServiceForSocial();
                SteamFriendsResponse = ApplicatationDataHandler.resultFriends;
            }
            OnPropertyChanged();
        }

        public void GetParent(CarouselPageVm dad)
        {
            _daddy = dad;
        }

        public SocialPageVm(IPageServiceZero pageService)
        {
            Friends = new ObservableCollection<SocialProfileDisplayModel>();
            Points = new ObservableCollection<SocialProfileDisplayModel>();
            _pageService = pageService;            
            
            SearchCommand = new CommandBuilder().SetExecuteAsync(SearchCommandExecuteAsync).Build();
        }
    }
}
