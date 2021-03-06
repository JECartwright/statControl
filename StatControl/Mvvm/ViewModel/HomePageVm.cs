using System;
using FunctionZero.MvvmZero;
using System.Windows.Input;
using System.Threading.Tasks;
using FunctionZero.CommandZero;
using Xamarin.Essentials;
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Mvvm.Model.ApplicationAPIData;
using Uri = System.Uri;


namespace StatControl.Mvvm.ViewModel
{
    internal class HomePageVm : MvvmZeroBaseVm
    {
        private readonly IPageServiceZero _pageService;
        private SteamUserProfileResponse _resultProfile;
        private CarouselPageVm _daddy;
        public ICommand ReloadUser { get; }
        public ICommand OpenProfile { get; }
        public ICommand UpdateCommand { get; }
        public SteamUserProfileResponse ResultProfile
        {
            get => _resultProfile;
            set
            {
                SetProperty(ref _resultProfile, value);
                OnPropertyChanged();
            }
        }

        private bool _isButtonVisible;
        public bool IsButtonVisible
        {
            get => _isButtonVisible;
            set => SetProperty(ref _isButtonVisible, value);
        }

        private async Task RealoadUserCommand()
        {
            await ApplicatationDataHandler.ReloadMain();
            _daddy.RefreshAll();
        }

        public void GetParent(CarouselPageVm dad)
        {
            _daddy = dad;
        }

        public void DataRefresh()
        {
            if (ApplicatationDataHandler.CheckAPI)
            {
                ResultProfile = ApplicatationDataHandler.resultProfile;
                IsButtonVisible = ResultProfile.response.players[0].steamid != ApplicatationDataHandler.MainUserId;
            }
            
            OnPropertyChanged();
        }

        public async Task OpenBrowser()
        {
            Uri uri = new Uri($"https://steamcommunity.com/profiles/{ApplicatationDataHandler.currentID}/");
            try
            {
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }

        public HomePageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
            ReloadUser = new CommandBuilder().SetExecuteAsync(RealoadUserCommand).Build();
            OpenProfile = new CommandBuilder().SetExecuteAsync(OpenBrowser).Build();
        }
    }
}
