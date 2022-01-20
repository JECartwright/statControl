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

namespace StatControl.Mvvm.ViewModel
{
    internal class SocialPageVm : MvvmZeroBaseVm
    {
        public ObservableCollection<SocialFriendsDisplayModel> Friends { get; private set; }
        public ObservableCollection<SocialPointsDisplayModel> PointsUsers { get; private set; }
        private readonly IPageServiceZero _pageService;
        private string _displayTest;
        public string DisplayTest
        {
            get { return _displayTest; }
            set
            {
                SetProperty(ref _displayTest, value);
                OnPropertyChanged();
            }
        }
        private SteamFriendsResponce _responce;
        public SteamFriendsResponce Responce
        {
            get { return _responce; }
            set
            {
                SetProperty(ref _responce, value);
                DisplayTest = _responce.friendslist.friends[0].steamid.ToString();




                OnPropertyChanged();
            }
        }

        public SocialPageVm(IPageServiceZero pageService)  
        {
            _pageService = pageService;            
        }



    }
}
