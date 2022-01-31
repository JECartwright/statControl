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
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Mvvm.Model.SteamUserAchievements;
using StatControl.Mvvm.Model.SteamGameStats;
using System.ComponentModel;
using StatControl.Mvvm.Model.ApplicationAPIData;

namespace StatControl.Mvvm.ViewModel
{
    internal class HomePageVm : MvvmZeroBaseVm
    {
        private readonly IPageServiceZero _pageService;
        private SteamUserProfileResponse _resultProfile;
        private CarouselPageVm daddy;
        public ICommand ReloadUser { get; }
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
            set
            {
                SetProperty(ref _isButtonVisible, value);
            }
        }

        private async Task RealoadUserCommand()
        {
            await ApplicatationDataHandler.ReloadMain();
            daddy.RefreshAll();
        }

        public void getParent(CarouselPageVm dad)
        {
            daddy = dad;
        }

        public void DataRefresh()
        {
            if (ApplicatationDataHandler.CheckAPI)
            {
                ResultProfile = ApplicatationDataHandler.resultProfile;
                if (ResultProfile.response.players[0].steamid == ApplicatationDataHandler.MainUserID)
                {
                    IsButtonVisible = false;
                }
                else
                {
                    IsButtonVisible = true;
                }
            }
            
            OnPropertyChanged();
        }

        public HomePageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
            ReloadUser = new CommandBuilder().SetExecuteAsync(RealoadUserCommand).Build();
        }
    }
}
