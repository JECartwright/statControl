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
    internal class FunPageVm : MvvmZeroBaseVm
    {
        private SteamGameStatsResponse _resultStats;
        private string _windowsBroken;
        private string _pistolRounds;
        private string _enemyWeapon;
        private string _enemyFlash;
        private string _knifeKill;
        private string _zoomSniperKill;
        private string _domination;
        private string _overkill;
        private string _revenges;
        private string _ggRoundWon;
        private string _ggRoundPlayed;
        private string _ggLifetimeScore;
        private string _hostagesSaved;

        public string WindowsBroken
        {
            get => _windowsBroken;
            set
            {
                SetProperty(ref _windowsBroken, value);
            }
        }
        
        public string PistolRounds
        {
            get => _pistolRounds;
            set
            {
                SetProperty(ref _pistolRounds, value);
            }
        }
                
        public string EnemyWeapon
        {
            get => _enemyWeapon;
            set
            {
                SetProperty(ref _enemyWeapon, value);
            }
        }
                
        public string EnemyFlash
        {
            get => _enemyFlash;
            set
            {
                SetProperty(ref _enemyFlash, value);
            }
        }
                
        public string KnifeKill
        {
            get => _knifeKill;
            set
            {
                SetProperty(ref _knifeKill, value);
            }
        }
                
        public string ZoomSniperKill
        {
            get => _zoomSniperKill;
            set
            {
                SetProperty(ref _zoomSniperKill, value);
            }
        }
        
        public string Domination
        {
            get => _domination;
            set
            {
                SetProperty(ref _domination, value);
            }
        }
        
        public string Overkill
        {
            get => _overkill;
            set
            {
                SetProperty(ref _overkill, value);
            }
        }
                
        public string Revenges
        {
            get => _revenges;
            set
            {
                SetProperty(ref _revenges, value);
            }
        }
                
        public string GGRoundWon
        {
            get => _ggRoundWon;
            set
            {
                SetProperty(ref _ggRoundWon, value);
            }
        }
                
        public string GGRoundPlayed
        {
            get => _ggRoundPlayed;
            set
            {
                SetProperty(ref _ggRoundPlayed, value);
            }
        }
                
        public string GGLifetimeScore
        {
            get => _ggLifetimeScore;
            set
            {
                SetProperty(ref _ggLifetimeScore, value);
            }
        }
        
        public string HostagesSaved
        {
            get => _hostagesSaved;
            set
            {
                SetProperty(ref _hostagesSaved, value);
            }
        }



        public SteamGameStatsResponse ResultStats
        {
            get => _resultStats;
            set
            {
                SetProperty(ref _resultStats, value);

                WindowsBroken = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_broken_windows")).value.ToString();
                PistolRounds = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_wins_pistolround")).value.ToString();
                EnemyWeapon = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_kills_enemy_weapon")).value.ToString();
                EnemyFlash = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_kills_enemy_blinded")).value.ToString();
                KnifeKill = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_kills_knife_fight")).value.ToString();
                ZoomSniperKill = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_kills_against_zoomed_sniper")).value.ToString();
                Domination = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_dominations")).value.ToString();
                Overkill = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_domination_overkills")).value.ToString();
                Revenges = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_revenges")).value.ToString();
                GGRoundWon = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_gun_game_rounds_won")).value.ToString();
                GGRoundPlayed = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_gun_game_rounds_played")).value.ToString();
                GGLifetimeScore = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_gun_game_contribution_score")).value.ToString();
                HostagesSaved = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_rescued_hostages")).value.ToString();
                
                OnPropertyChanged();
            }
        }

        public FunPageVm()
        {
            MessagingCenter.Subscribe<CarouselPageVm, SteamGameStatsResponse>(this, "resultStats", (sender, resultStats) =>
            {
                Debug.WriteLine("Received Stats Fun");
                ResultStats = resultStats;
            });
        }
    }
}
