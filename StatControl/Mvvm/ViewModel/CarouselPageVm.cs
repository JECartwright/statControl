﻿using StatControl.Mvvm.Model.SteamGameStats;
using StatControl.Mvvm.Model.SteamUserAchievements;
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Mvvm.Model.SteamAchievementData;
using StatControl.Mvvm.Model.SteamUserFriends;
using StatControl.Mvvm.View;
using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using System.Windows.Input;
using System.Threading.Tasks;
using StatControl.Services.Rest;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;
using StatControl.Services;

namespace StatControl.Mvvm.ViewModel
{
    internal class CarouselPageVm : CarouselPage
    {
        private readonly IPageServiceZero _pageService;
        
        public ICommand TestCommand { get; }
        public SocialPageVm SocialVm { get; private set; }
        public HomePageVm HomeVm { get; private set; }
        public MainStatPageVm MainVm { get; private set; }
        public LastMatchPageVm LastVm { get; private set; }
        public WeaponsSelectPageVm WeaSelectVm { get; private set; }
        public MapPageVm MapVm { get; private set; }
        public FunPageVm FunVm { get; private set; }
        public AchievementsPageVm AchieveVm { get; private set; }

        public CarouselPageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
            SocialVm = new SocialPageVm(_pageService);
            HomeVm = new HomePageVm(_pageService);
            MainVm = new MainStatPageVm(_pageService);
            LastVm = new LastMatchPageVm(_pageService);
            WeaSelectVm = new WeaponsSelectPageVm(_pageService);
            MapVm = new MapPageVm(_pageService);
            FunVm = new FunPageVm(_pageService);
            AchieveVm = new AchievementsPageVm(_pageService);
        }

        internal void Init()
        {
            FunVm.DataRefresh();
            HomeVm.DataRefresh();
            MainVm.DataRefresh();
            LastVm.DataRefresh();
            WeaSelectVm.DataRefresh();
            SocialVm.DataRefresh();
            MapVm.DataRefresh();
            AchieveVm.DataRefresh();
            WeaSelectVm.WeaponsDisplay.Clear();
            WeaSelectVm.onStarted();
            WeaSelectVm.platformHelper();
            OnPropertyChanged();
        }
    }
}
