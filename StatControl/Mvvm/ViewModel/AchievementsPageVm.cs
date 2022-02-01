using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FunctionZero.MvvmZero;
using StatControl.Mvvm.Model.ApplicationAPIData;
using StatControl.Mvvm.Model.DisplayModel;
using StatControl.Mvvm.Model.SteamAchievementData;
using StatControl.Mvvm.Model.SteamUserAchievements;
using Xamarin.Forms;

namespace StatControl.Mvvm.ViewModel
{
    internal class AchievementsPageVm : MvvmZeroBaseVm
    {
        private readonly IPageServiceZero _pageService;

        private ObservableCollection<AchievementDisplayModel> _achievements;

        private ConcurrentBag<AchievementDisplayModel> _achievementsToSort = new ConcurrentBag<AchievementDisplayModel>();
        private SteamAchievementDataResponse _resultAchieveData;
        private SteamUserAchievementsResponse _resultUserAchieve;

        public AchievementsPageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
            Achievements = new ObservableCollection<AchievementDisplayModel>();
        }

        public ObservableCollection<AchievementDisplayModel> Achievements
        {
            get => _achievements;
            set => SetProperty(ref _achievements, value);
        }

        private SteamAchievementDataResponse ResultAchieveData
        {
            get => _resultAchieveData;
            set
            {
                SetProperty(ref _resultAchieveData, value);

                Achievements.Clear();
                CallServer();
                OnPropertyChanged();
            }
        }

        private SteamUserAchievementsResponse ResultUserAchieve
        {
            get => _resultUserAchieve;
            set
            {
                SetProperty(ref _resultUserAchieve, value);
                OnPropertyChanged();
            }
        }


        private void CallServer()
        {
            _achievementsToSort = new ConcurrentBag<AchievementDisplayModel>();
            //Goes through all the achievements
            Parallel.For(0, ResultUserAchieve.playerstats.achievements.Count, i =>
            {
                var toPush = new AchievementDisplayModel
                {
                    APIName = ResultUserAchieve.playerstats.achievements[i].apiname,
                    Name = ResultUserAchieve.playerstats.achievements[i].name,
                    Description = ResultUserAchieve.playerstats.achievements[i].description,
                    Achieved = ResultUserAchieve.playerstats.achievements[i].achieved
                };

                switch (toPush.Achieved)
                {
                    //Assigns tick or cross, Colour, and image depending if it is achieved by the player
                    case 1:
                        toPush.AchievedText = "✓";
                        toPush.AchievedColor = new Color(0, 255, 0);
                        toPush.ImageAddress = ResultAchieveData.game.availableGameStats.achievements[i].icon;
                        break;
                    case 0:
                        toPush.AchievedText = "✗";
                        toPush.AchievedColor = new Color(255, 0, 0);
                        toPush.ImageAddress = ResultAchieveData.game.availableGameStats.achievements[i].icongray;
                        break;
                    default:
                        toPush.AchievedText = "!";
                        toPush.AchievedColor = new Color(255, 0, 0);
                        toPush.ImageAddress = "Backup Image.jpg";
                        break;
                }

                _achievementsToSort.Add(toPush);
            });

            //Sorts achievements by achieved
            var sortedAchievements = _achievementsToSort.OrderByDescending(o => o.Achieved).ToList();

            //Adds list to observable collection
            var ob2List = Achievements.ToList();
            ob2List.AddRange(sortedAchievements);
            Achievements = new ObservableCollection<AchievementDisplayModel>(ob2List);
        }

        public void DataRefresh()
        {
            if (ApplicatationDataHandler.CheckAPI)
            {
                ResultUserAchieve = ApplicatationDataHandler.ResultUserAchieve;
                ResultAchieveData = ApplicatationDataHandler.ResultAchieveData;
            }

            OnPropertyChanged();
        }
    }
}