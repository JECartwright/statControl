using FunctionZero.MvvmZero;
using System.Collections.Generic;
using Xamarin.Forms;
using StatControl.Mvvm.Model.ApplicationAPIData;
using System.Runtime.CompilerServices;
using StatControl.Mvvm.View;

namespace StatControl.Mvvm.ViewModel
{
    internal class CarouselPageVm : CarouselPage
    {
        private readonly IPageServiceZero _pageService;
        public SocialPageVm SocialVm { get; private set; }
        public HomePageVm HomeVm { get; private set; }
        public MainStatPageVm MainVm { get; private set; }
        public LastMatchPageVm LastVm { get; private set; }
        public WeaponsSelectPageVm WeaSelectVm { get; private set; }
        public MapPageVm MapVm { get; private set; }
        public FunPageVm FunVm { get; private set; }
        public AchievementsPageVm AchieveVm { get; private set; }

        private string _userTitle;
        public string UserTitle
        {
            get => _userTitle;
            set => SetProperty(ref _userTitle, value);
        }

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

        public void RefreshAll()
        {


            FunVm.DataRefresh();
            HomeVm.DataRefresh();//Not Sure
            MainVm.DataRefresh();
            LastVm.DataRefresh();
            WeaSelectVm.DataRefresh();
            MapVm.DataRefresh();
            AchieveVm.DataRefresh();
            WeaSelectVm.WeaponsDisplay.Clear();
            WeaSelectVm.OnStarted();
            WeaSelectVm.PlatformHelper();

            UserTitle = $"Viewing: {ApplicatationDataHandler.ResultProfile.response.players[0].personaname}";

            OnPropertyChanged();
        }

        internal void Init()
        {


            FunVm.DataRefresh();
            HomeVm.DataRefresh();
            HomeVm.GetParent(this);
            MainVm.DataRefresh();
            LastVm.DataRefresh();
            WeaSelectVm.DataRefresh();
            SocialVm.DataRefresh();
            SocialVm.GetParent(this);
            MapVm.DataRefresh();
            AchieveVm.DataRefresh();
            WeaSelectVm.WeaponsDisplay.Clear();
            WeaSelectVm.OnStarted();
            WeaSelectVm.PlatformHelper();

            UserTitle = $"Viewing: {ApplicatationDataHandler.ResultProfile.response.players[0].personaname}";

            OnPropertyChanged();
        }

        private void SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return;
            }

            backingStore = value;
            OnPropertyChanged(propertyName);
        }

    }
}
