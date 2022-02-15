using FunctionZero.MvvmZero;
using System.Windows.Input;
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Mvvm.Model.ApplicationAPIData;

namespace StatControl.Mvvm.ViewModel
{
    internal class HomePageVm : MvvmZeroBaseVm
    {
        private readonly IPageServiceZero _pageService;
        private SteamUserProfileResponse _resultProfile;
        private CarouselPageVm _daddy;

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

        public void GetParent(CarouselPageVm dad)
        {
            _daddy = dad;
        }

        public void DataRefresh()
        {
            if (ApplicatationDataHandler.CheckAPI)
            {
                ResultProfile = ApplicatationDataHandler.ResultProfile;
            }
            OnPropertyChanged();
        }

        public HomePageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
        }
    }
}
