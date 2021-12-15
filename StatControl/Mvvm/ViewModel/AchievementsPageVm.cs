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

namespace StatControl.Mvvm.ViewModel
{
    internal class AchievementsPageVm : MvvmZeroBaseVm
    {
        private string _name;
        private string _details;
        private string _apiName;
        private int _achieved;
        private SteamUserAchievementsResponse _resultUserAchieve;
        private SteamAchievementDataResponse _resultAchieveData;



        public ObservableCollection<AchievementModel> Achievements { get;}


        public SteamAchievementDataResponse ResultAchieveData
        {
            get { return _resultAchieveData; }
            set
            {
                SetProperty(ref _resultAchieveData, value);
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

        public string name
        {
            get { return _name;}
            set
            {
                SetProperty(ref _name, value);
            }
        }
        public string details
        {
            get { return _details; }
            set
            {
                SetProperty(ref _details, value);
            }
        }
        public string apiName
        {
            get { return _apiName; }
            set
            {
                SetProperty(ref _apiName, value);
            }
        }
        public string imagePath
        {
            get 
            {
                if (_achieved == 0)
                {
                    return "GRAY_" + _apiName + ".jpeg";
                }
                if (_achieved == 1)
                {
                    return _apiName + ".jpeg";
                }
                else
                {
                    return "DEFAULT";
                }
            }
        }

        void CallServer()
        {
            //_resultUserAchieve.playerstats.achievements.Sort();
            for (int i = 0; i< ResultUserAchieve.playerstats.achievements.Count;i++)
            {
                Achievements.Add(_resultUserAchieve.playerstats.achievements[i]);             
            }
        }

        public AchievementsPageVm()
        {
            Achievements = new ObservableCollection<AchievementModel>();
            MessagingCenter.Subscribe<CarouselPageVm, SteamUserAchievementsResponse>(this, "resultUserAchieve", (sender, resultAchieve) =>
            {
                Debug.WriteLine("Received UserAchieve Achievement Page");
                ResultUserAchieve = resultAchieve;
                CallServer();

            });
            MessagingCenter.Subscribe<CarouselPageVm, SteamAchievementDataResponse>(this, "resultAchieveData", (sender, resultAchieve) =>
            {
                Debug.WriteLine("Received AchieveData Achievement Page");
                ResultAchieveData = resultAchieve;
                CallServer();
            });
            
        }

    }
}
