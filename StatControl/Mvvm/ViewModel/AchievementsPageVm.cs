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
using StatControl.Mvvm.Model.DisplayModel;

namespace StatControl.Mvvm.ViewModel
{
    internal class AchievementsPageVm : MvvmZeroBaseVm
    {
        private readonly IPageServiceZero _pageService;
        private SteamUserAchievementsResponse _resultUserAchieve;
        private SteamAchievementDataResponse _resultAchieveData;

        public ObservableCollection<AchievementDisplayModel> Achievements { get; private set; }
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
            }
        }

        public SteamUserAchievementsResponse ResultUserAchieve
        {
            get { return _resultUserAchieve; }
            set
            {

                SetProperty(ref _resultUserAchieve, value);
            }
        }


        void CallServer()
        {
            List<string> achievementNamesList = new List<string>();
            //_resultUserAchieve.playerstats.achievements.Sort();
            for (int i = 0; i< ResultUserAchieve.playerstats.achievements.Count;i++)
            {
                for (int b = 0; b<ResultAchieveData.game.availableGameStats.achievements.Count;b++)
                {
                    if (ResultUserAchieve.playerstats.achievements[i].apiname == ResultAchieveData.game.availableGameStats.achievements[b].name)
                    {
                        AchievementDisplayModel toPush = new AchievementDisplayModel();
                        toPush.APIName = ResultUserAchieve.playerstats.achievements[i].apiname;
                        toPush.Name = ResultUserAchieve.playerstats.achievements[i].name;
                        achievementNamesList.Add(ResultUserAchieve.playerstats.achievements[i].name);
                        toPush.Description = ResultUserAchieve.playerstats.achievements[i].description;
                        toPush.Achieved = ResultUserAchieve.playerstats.achievements[i].achieved;
                        if (toPush.Achieved == 1)
                        {
                            toPush.AchievedText = "✓";
                            toPush.AchievedColor = new Color(0,255,0);
                            toPush.ImageAddress = ResultAchieveData.game.availableGameStats.achievements[b].icon;
                        }
                        else if (toPush.Achieved == 0)
                        {
                            toPush.AchievedText = "✗";
                            toPush.AchievedColor = new Color(255, 0, 0);
                            toPush.ImageAddress = ResultAchieveData.game.availableGameStats.achievements[b].icongray;
                        }
                        else
                        {
                            toPush.AchievedText = "!";
                            toPush.AchievedColor = new Color(255, 0, 0);
                            toPush.ImageAddress = "Backup Image.jpg";
                        }
                        AchievementsToSort.Add(toPush);
                    }
                }          
            }
            List<AchievementDisplayModel> tempAchieved = new List<AchievementDisplayModel>();
            List<AchievementDisplayModel> tempNotAchieved = new List<AchievementDisplayModel>();
            achievementNamesList.Sort();
            for (int z = 0; z< achievementNamesList.Count; z++)
            {         
                for (int x = 0; x< AchievementsToSort.Count; x++)
                {
                    if (AchievementsToSort[x].Name == achievementNamesList[z] && AchievementsToSort[x].Achieved == 0)
                    {
                        tempNotAchieved.Add(AchievementsToSort[x]);
                    }
                    else if (AchievementsToSort[x].Name == achievementNamesList[z] && AchievementsToSort[x].Achieved == 1)
                    {
                        tempAchieved.Add(AchievementsToSort[x]);
                    }
                }                
            }
            for (int y = 0; y < tempAchieved.Count; y++)
            {
                Achievements.Add(tempAchieved[y]);
            }
            for (int w = 0; w < tempNotAchieved.Count; w++)
            {
                Achievements.Add(tempNotAchieved[w]);
            }
        }

        public AchievementsPageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
            Achievements = new ObservableCollection<AchievementDisplayModel>();           
        }
    }
}
