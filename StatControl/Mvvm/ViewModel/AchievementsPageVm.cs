using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private readonly List<AchievementDisplayModel> _achievementsToSort = new List<AchievementDisplayModel>();
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
                _achievementsToSort.Clear();
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
            //Goes through all the achievements
            for (var i = 0; i < ResultUserAchieve.playerstats.achievements.Count; i++)
            {
                var toPush = new AchievementDisplayModel
                {
                    APIName = ResultUserAchieve.playerstats.achievements[i].apiname,
                    Name = ResultUserAchieve.playerstats.achievements[i].name,
                    Description = ResultUserAchieve.playerstats.achievements[i].description,
                    Achieved = ResultUserAchieve.playerstats.achievements[i].achieved
                };

                //Assigns tick or cross, Colour, and image depending if it is achieved by the player
                if (toPush.Achieved == 1)
                {
                    toPush.AchievedText = "✓";
                    toPush.AchievedColor = new Color(0, 255, 0);
                    toPush.ImageAddress = ResultAchieveData.game.availableGameStats.achievements[i].icon;
                }
                else if (toPush.Achieved == 0)
                {
                    toPush.AchievedText = "✗";
                    toPush.AchievedColor = new Color(255, 0, 0);
                    toPush.ImageAddress = ResultAchieveData.game.availableGameStats.achievements[i].icongray;
                }
                else
                {
                    toPush.AchievedText = "!";
                    toPush.AchievedColor = new Color(255, 0, 0);
                    toPush.ImageAddress = "Backup Image.jpg";
                }

                _achievementsToSort.Add(toPush);
            }

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