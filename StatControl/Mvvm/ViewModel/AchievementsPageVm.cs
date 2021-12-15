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
        private SteamUserAchievementsResponse _resultAchieve;
        public ObservableCollection<AchievementModel> Achievements { get;}


        public SteamUserAchievementsResponse ResultAchieve
        {
            get { return _resultAchieve; }
            set
            {

                SetProperty(ref _resultAchieve, value);
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
            ResultAchieve.playerstats.achievements.Sort();
            for (int i = 0; i< ResultAchieve.playerstats.achievements.Count;i++)
            {
                Achievements.Add(ResultAchieve.playerstats.achievements[i]);             
            }
        }

        public AchievementsPageVm()
        {
            Achievements = new ObservableCollection<AchievementModel>();
            MessagingCenter.Subscribe<CarouselPageVm, SteamUserAchievementsResponse>(this, "resultAchieve", (sender, resultAchieve) =>
            {
                Debug.WriteLine("Received Stats Fun");
                ResultAchieve = resultAchieve;
                CallServer();
            });
            
        }

    }
}
