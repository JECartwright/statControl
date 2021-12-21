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
using System;
using System.Collections.Generic;

namespace StatControl.Mvvm.ViewModel
{
    internal class LastMatchPageVm : MvvmZeroBaseVm
    {
        private string _textMatchResults;
        private string _textMVP;
        private string _textKills;
        private string _textDeaths;
        private string _textKD;
        private string _textDamage;
        private string _textADR;
        private string _textMoneySpent;
        private string _textScore;
        private string _textFavShots;
        private string _textFavHits;
        private string _textFavKills;
        private string _textFavAccuracy;
        private string _FavWeaponID;
        private SteamGameStatsResponse _resultStats;

        private Dictionary<int, String> _favWeaponDictionary;

        public string FavWeaponID
        {
            get => _FavWeaponID;
            set => SetProperty(ref _FavWeaponID, value);
        }

        public string TextFavShots
        {
            get => _textFavShots;
            set => SetProperty(ref _textFavShots, value);
        }

        public string TextFavHits
        {
            get => _textFavHits;
            set => SetProperty(ref _textFavHits, value);
        }

        public string TextFavKills
        {
            get => _textFavKills;
            set => SetProperty(ref _textFavKills, value);
        }

        public string TextFavAccuracy
        {
            get => _textFavAccuracy;
            set => SetProperty(ref _textFavAccuracy, value);
        }

        public string TextMatchResults
        {
            get => _textMatchResults;
            set => SetProperty(ref _textMatchResults, value);
        }

        public string TextMVP
        {
            get => _textMVP;
            set => SetProperty(ref _textMVP, value);
        }

        public string TextKills
        {
            get => _textKills;
            set => SetProperty(ref _textKills, value);
        }

        public string TextDeaths
        {
            get => _textDeaths;
            set => SetProperty(ref _textDeaths, value);
        }

        public string TextKD
        {
            get => _textKD;
            set => SetProperty(ref _textKD, value);
        }

        public string TextDamage
        {
            get => _textDamage;
            set => SetProperty(ref _textDamage, value);
        }

        public string TextADR
        {
            get => _textADR;
            set => SetProperty(ref _textADR, value);
        }

        public string TextScore
        {
            get => _textScore;
            set => SetProperty(ref _textScore, value);
        }

        public string TextMoneySpent
        {
            get => _textMoneySpent;
            set => SetProperty(ref _textMoneySpent, value);
        }

        public SteamGameStatsResponse ResultStats
        {
            get => _resultStats;
            set
            {
                SetProperty(ref _resultStats, value);

                TextMatchResults = _resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_wins")).value.ToString() + " / " + (_resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_rounds")).value - _resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_wins")).value).ToString();
                TextMVP = _resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_mvps")).value.ToString();
                TextKills = _resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_kills")).value.ToString();
                TextDeaths =  _resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_deaths")).value.ToString();
                
                if (_resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_deaths")).value == 0) {
                    TextKD = _resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_kills")).value.ToString();
                } else
                {
                    TextKD = Math.Round((double)_resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_kills")).value / (double)_resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_deaths")).value, 2).ToString();
                }

                TextDamage = _resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_damage")).value.ToString();
                TextADR = Math.Round((double)_resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_damage")).value / (double)_resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_rounds")).value, 2).ToString();
                TextScore =  _resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_contribution_score")).value.ToString();
                TextMoneySpent =  _resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_money_spent")).value.ToString();

                //Favourite Weapon
                FavWeaponID = _favWeaponDictionary[_resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_favweapon_id")).value];

                TextFavShots = "Shots: " + _resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_favweapon_shots")).value.ToString();
                TextFavHits = "Hits: " + _resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_favweapon_hits")).value.ToString();
                TextFavKills = "Kills: " + _resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_favweapon_kills")).value.ToString();
                TextFavAccuracy = "Accuracy: " + Math.Round((double)_resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_favweapon_hits")).value / (double)_resultStats.playerstats.stats.Find(x => x.name.Equals("last_match_favweapon_shots")).value * 100, 2).ToString() + "%";


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

            _favWeaponDictionary = new Dictionary<int, String>()
            {
                { 1, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_deagle.29e8f0d7d0be5e737d4f663ee8b394b5c9e00bdd.png" },
                { 2, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_elite.6563e9d274c6e799d71a7809021624f213d5e080.png" },
                { 3, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_fiveseven.7c33b4a78ae94a3d14e7cd0f71b295cf61717d75.png" },
                { 4, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_glock.8430afea5349054d0923cefa7d2e7bf3950ce3d7.png" },
                { 7, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_ak47.a320f13fea4f21d1eb3b46678d6b12e97cbd1052.png" },
                { 8, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_aug.6b97a75aa4c0dbb61d81efb6d5497b079b67d0da.png" },
                { 9, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_awp.2899e1c6345ed05d62bdbe112db1b117d022e477.png" },
                { 10, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_famas.c897878873beb9e9ca4c68ef3a666869c6e78031.png" },
                { 11, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_g3sg1.986d0e07f58c81c99aa5a47d86340f4c3d400339.png" },
                { 13, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_galilar.b84153658afdb7dc26a9854e566fde3fc42c22ef.png" },
                { 14, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_m249.02d1cf8fa8c41af5a43749bf780c4c4a2e50ea8e.png" },
                { 16, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_m4a1.39b3bd8d556e5cdebb79d60902442986eb9aedff.png" },
                { 17, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_mac10.41e40474aa21a9ed90d9b21dd5adf0910f766426.png" },
                { 19, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_p90.15fedd7fc90f003b8de0ded36245b438d54bc3d2.png" },
                { 23, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_mp5sd.2e92234c951819f3ae44742e96c488ef97f26c7c.png" },
                { 24, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_ump45.55669e2321f28efed775be27f7e3c7e71b501520.png" },
                { 25, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_xm1014.7bd7f3985d680db2fcb7cad32b07c90b758c234b.png" },
                { 26, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_bizon.58523d37ee43b9a4ef42a67b65a28e5967743a56.png" },
                { 27, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_mag7.5480ba05c61153309163c46e7d646d6958af9bf7.png" },
                { 28, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_negev.1cf512eb01bd62bcae5c54feec694f418ab71d30.png" },
                { 29, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_sawedoff.4c4df9c84e1edc20488c45061ad88cfd2460c4a5.png" },
                { 30, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_tec9.74538566492b4af122be9b996bdd7d08585db3c0.png" },
                { 31, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_taser.3c80d155bf0547c377217920f2c7329c8b00d472.png" },
                { 32, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_hkp2000.c2221f8c2ef3df6c2fcdafd1bea9faae01f64054.png" },
                { 33, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_mp7.0afc09868c38a00fde50c3e4943637c714e8981e.png" },
                { 34, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_mp9.c9103efde0845eb715cdcb67bf74bad646b1c5bc.png" },
                { 35, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_nova.d9063351d4233101d02def18aa7e901d02f9b4c1.png" },
                { 36, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_p250.0bc9109121fb318a3bb18f6fa92692c7aa433205.png" },
                { 38, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_scar20.1552c7b64dfe9e542a3b730edb80e21dcc6d243d.png" },
                { 39, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_sg556.74040869391ea2ab25777f3670a6015191a73e6c.png" },
                { 40, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_ssg08.271a856f50fd6ac1014334098b1a43d61bddb892.png" },
                { 60, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_m4a1_silencer.a8d2a028fa33eb117d6d7665303c3316169c33f7.png" },
                { 61, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_usp_silencer.608e10862885084bb1cec55d87ba5e694bfd621d.png" },
                { 63, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_cz75a.057939990f5f295fc5eaf8f758cdef21a7cfeb8a.png" },
                { 64, "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_revolver.a7c0ab2973cdc0bdb53ebbef960ecbae8842f719.png" }
            };
        }
    }
}
