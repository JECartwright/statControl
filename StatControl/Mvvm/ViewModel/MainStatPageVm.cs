using System;
using System.Collections.Generic;
using System.Text;
using StatControl.Mvvm.View;
using FunctionZero.CommandZero;
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
    internal class MainStatPageVm : MvvmZeroBaseVm
    {
        private SteamGameStatsResponse _resultStats;
        private string _roundsWon;
        private string _mVP;
        private string _moneyEarned;
        private string _bombsPlanted;
        private string _bombsDefused;
        private string _damage;
        private string _weaponsDonated;
        private string _headshots;
        private string _shots;
        private string _kills;
        private string _hits;
        private string _accuracy;
        private string _matchesPlayed;
        private string _matchesWon;
        private string _matchesWinRate;
        private string _contributionScore;

        public string RoundsWon
        {
            get => _roundsWon; 
            set
            {
                SetProperty(ref _roundsWon, value);
            }
        }

        public string MVP { 
            get => _mVP; 
            set
            {
                SetProperty(ref _mVP, value);
            } 
        }

        public string MoneyEarned { 
            get => _moneyEarned; 
            set
            {
                SetProperty(ref _moneyEarned, value);
            } 
        }

        public string BombsPlanted { 
            get => _bombsPlanted;
            set
            {
                SetProperty(ref _bombsPlanted, value);
            } 
        }

        public string BombsDefused { 
            get => _bombsDefused; 
            set
            {
                SetProperty(ref _bombsDefused, value);
            } 
        }

        public string WeaponsDonated { 
            get => _weaponsDonated; 
            set
            {
                SetProperty(ref _weaponsDonated, value);
            } 
        }

        public string Damage { 
            get => _damage; 
            set
            {
                SetProperty(ref _damage, value);
            } 
        }

        public string Headshots { 
            get => _headshots; 
            set
            {
                SetProperty(ref _headshots, value);
            } 
        }

        public string Shots { 
            get => _shots; 
            set
            {
                SetProperty(ref _shots, value);
            } 
        }

        public string Kills { 
            get => _kills; 
            set
            {
                SetProperty(ref _kills, value);
            } 
        }

        public string Hits { 
            get => _hits; 
            set
            {
                SetProperty(ref _hits, value);
            } 
        }

        public string Accuracy { 
            get => _accuracy; 
            set
            {
                SetProperty(ref _accuracy, value);
            } 
        }

        public string MatchesPlayed { 
            get => _matchesPlayed; 
            set
            {
                SetProperty(ref _matchesPlayed, value);
            } 
        }

        public string MatchesWon { 
            get => _matchesWon; 
            set
            {
                SetProperty(ref _matchesWon, value);
            } 
        }

        public string MatchesWinRate { 
            get => _matchesWinRate; 
            set
            {
                SetProperty(ref _matchesWinRate, value);
            } 
        }

        public string ContributionScore { 
            get => _contributionScore; 
            set
            {
                SetProperty(ref _contributionScore, value);
            } 
        }

        public SteamGameStatsResponse ResultStats
        {
            get => _resultStats;
            set
            {
                SetProperty(ref _resultStats, value);
                
                RoundsWon = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_wins"))?.value.ToString() ?? "0";
                MVP = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_mvps"))?.value.ToString() ?? "0";
                MoneyEarned = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_money_earned"))?.value.ToString() ?? "0";
                BombsPlanted = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_planted_bombs"))?.value.ToString() ?? "0";
                BombsDefused = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_defused_bombs"))?.value.ToString() ?? "0";
                WeaponsDonated = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_weapons_donated"))?.value.ToString() ?? "0";
                Damage = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_damage_done"))?.value.ToString() ?? "0";
                Headshots = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_kills_headshot"))?.value.ToString() ?? "0";
                Shots = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_shots_fired"))?.value.ToString() ?? "0";
                Kills = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_kills"))?.value.ToString() ?? "0";
                Hits = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_shots_hit"))?.value.ToString() ?? "0";
                Accuracy = Math.Round((double)_resultStats.playerstats.stats.Find(x => x.name.Equals("total_shots_hit"))?.value / (double)_resultStats.playerstats.stats.Find(x => x.name.Equals("total_shots_fired"))?.value * 100, 2).ToString() ?? "0";
                MatchesPlayed = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_matches_played"))?.value.ToString() ?? "0";
                MatchesWon = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_matches_won"))?.value.ToString() ?? "0";
                MatchesWinRate = Math.Round((double)_resultStats.playerstats.stats.Find(x => x.name.Equals("total_matches_won"))?.value / (double)_resultStats.playerstats.stats.Find(x => x.name.Equals("total_matches_played"))?.value * 100, 2).ToString() ?? "0";
                ContributionScore = _resultStats.playerstats.stats.Find(x => x.name.Equals("total_contribution_score"))?.value.ToString() ?? "0";

                OnPropertyChanged();
            }
        }

        public MainStatPageVm()
        {
            MessagingCenter.Subscribe<CarouselPageVm, SteamGameStatsResponse>(this, "resultStats", (sender, resultStats) =>
            {
                Debug.WriteLine("Received Stats Main");
                ResultStats = resultStats;
            });
        }
    }
}

