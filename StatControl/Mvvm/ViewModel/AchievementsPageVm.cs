using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using StatControl.Mvvm.View;
using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using System.Windows.Input;
using System.Threading.Tasks;
using StatControl.Mvvm.Model.SteamUserAchievements;
using StatControl.Mvvm.Model.SteamAchievementData;
using Xamarin.Forms;
using System.Diagnostics;
using System.Linq;
using StatControl.Mvvm.Model.DisplayModel;
using StatControl.Mvvm.Model.ApplicationAPIData;

namespace StatControl.Mvvm.ViewModel
{
    internal class AchievementsPageVm : MvvmZeroBaseVm
    {
        private readonly IPageServiceZero _pageService;
        private SteamUserAchievementsResponse _resultUserAchieve;
        private SteamAchievementDataResponse _resultAchieveData;

        public ObservableCollection<AchievementDisplayModel> Achievements { get; private set; } //Achievements that get binded to
        private List<AchievementDisplayModel> AchievementsToSort = new List<AchievementDisplayModel>();

        public SteamAchievementDataResponse ResultAchieveData
        {
            get { return _resultAchieveData; }
            set
            {
                SetProperty(ref _resultAchieveData, value);

                Achievements.Clear();
                AchievementsToSort.Clear();
                CallServer();
                OnPropertyChanged();
            }
        }

        public SteamUserAchievementsResponse ResultUserAchieve
        {
            get { return _resultUserAchieve; }
            set
            {
                SetProperty(ref _resultUserAchieve, value);
                OnPropertyChanged();
            }
        }


        void CallServer()
        {
            //Goes through all the achievements
            for (int i = 0; i < ResultUserAchieve.playerstats.achievements.Count; i++)
            {
                AchievementDisplayModel toPush = new AchievementDisplayModel
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
                AchievementsToSort.Add(toPush);
            }

            //Sorts achievements by achieved
            List<AchievementDisplayModel> SortedAchievements = AchievementsToSort.OrderByDescending(o => o.Achieved).ToList();

            //Adds list to observable collection
            var ob2list = Achievements.ToList();
            ob2list.AddRange(SortedAchievements);
            Achievements = new ObservableCollection<AchievementDisplayModel>(ob2list);
        }

        public void DataRefresh()
        {
            if (ApplicatationDataHandler.CheckAPI)
            {
                ResultUserAchieve = ApplicatationDataHandler.resultUserAchieve;
                ResultAchieveData = ApplicatationDataHandler.resultAchieveData;
            }
            OnPropertyChanged();
        }

        public AchievementsPageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
            Achievements = new ObservableCollection<AchievementDisplayModel>();
        }
    }
}
