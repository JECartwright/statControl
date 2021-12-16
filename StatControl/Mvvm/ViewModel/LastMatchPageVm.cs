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
    internal class LastMatchPageVm : MvvmZeroBaseVm
    {
        private SteamGameStatsResponse _resultStats;
        
        public SteamGameStatsResponse ResultStats
        {
            get => _resultStats;
            set
            {
                SetProperty(ref _resultStats, value);
                OnPropertyChanged();
            }
        }

        public LastMatchPageVm()
        {
            MessagingCenter.Subscribe<CarouselPageVm, SteamGameStatsResponse>(this, "resultStats", (sender, resultStats) =>
            {
                Debug.WriteLine("Received Lastmatch Fun");
                ResultStats = resultStats;
            });
        }
    }
}
