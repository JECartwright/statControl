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

namespace StatControl.Mvvm.ViewModel
{
    internal class HomePageVm : MvvmZeroBaseVm
    {
        private SteamUserProfileResponse _resultProfile;
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

        public HomePageVm()
        {
            
            MessagingCenter.Subscribe<CarouselPageVm, SteamUserProfileResponse>(this, "resultProfile", (sender, resultProfile) =>
            {
                Debug.WriteLine("HOME_PAGE: Received resultProfile");
                ResultProfile = resultProfile;
            });
        }
    }
}
