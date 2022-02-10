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
using StatControl.Mvvm.Model.DisplayModel;
using StatControl.Mvvm.Model.SteamGameStats;
using StatControl.Mvvm.Model.SQLWeaponModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Diagnostics;
using StatControl.Services;
using StatControl.Mvvm.Model.ApplicationAPIData;
using Microcharts;

namespace StatControl.Mvvm.ViewModel
{
    internal class IndividualWeaponPageVm : MvvmZeroBaseVm
    {
        private readonly Dictionary<String, String> _favWeaponDictionary = new Dictionary<String, String>()
            {
                { "deagle", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_deagle.29e8f0d7d0be5e737d4f663ee8b394b5c9e00bdd.png" },
                { "elite", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_elite.6563e9d274c6e799d71a7809021624f213d5e080.png" },
                { "fiveseven", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_fiveseven.7c33b4a78ae94a3d14e7cd0f71b295cf61717d75.png" },
                { "glock", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_glock.8430afea5349054d0923cefa7d2e7bf3950ce3d7.png" },
                { "ak47", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_ak47.a320f13fea4f21d1eb3b46678d6b12e97cbd1052.png" },
                { "aug", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_aug.6b97a75aa4c0dbb61d81efb6d5497b079b67d0da.png" },
                { "awp", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_awp.2899e1c6345ed05d62bdbe112db1b117d022e477.png" },
                { "famas", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_famas.c897878873beb9e9ca4c68ef3a666869c6e78031.png" },
                { "g3sg1", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_g3sg1.986d0e07f58c81c99aa5a47d86340f4c3d400339.png" },
                { "galilar", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_galilar.b84153658afdb7dc26a9854e566fde3fc42c22ef.png" },
                { "m249", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_m249.02d1cf8fa8c41af5a43749bf780c4c4a2e50ea8e.png" },
                { "m4a1", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_m4a1.39b3bd8d556e5cdebb79d60902442986eb9aedff.png" },
                { "mac10", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_mac10.41e40474aa21a9ed90d9b21dd5adf0910f766426.png" },
                { "p90", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_p90.15fedd7fc90f003b8de0ded36245b438d54bc3d2.png" },
                { "ump45", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_ump45.55669e2321f28efed775be27f7e3c7e71b501520.png" },
                { "xm1014", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_xm1014.7bd7f3985d680db2fcb7cad32b07c90b758c234b.png" },
                { "bizon", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_bizon.58523d37ee43b9a4ef42a67b65a28e5967743a56.png" },
                { "mag7", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_mag7.5480ba05c61153309163c46e7d646d6958af9bf7.png" },
                { "negev", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_negev.1cf512eb01bd62bcae5c54feec694f418ab71d30.png" },
                { "sawedoff", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_sawedoff.4c4df9c84e1edc20488c45061ad88cfd2460c4a5.png" },
                { "tec9", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_tec9.74538566492b4af122be9b996bdd7d08585db3c0.png" },
                { "taser", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_taser.3c80d155bf0547c377217920f2c7329c8b00d472.png" },
                { "hkp2000", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_hkp2000.c2221f8c2ef3df6c2fcdafd1bea9faae01f64054.png" },
                { "mp7", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_mp7.0afc09868c38a00fde50c3e4943637c714e8981e.png" },
                { "mp9", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_mp9.c9103efde0845eb715cdcb67bf74bad646b1c5bc.png" },
                { "nova", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_nova.d9063351d4233101d02def18aa7e901d02f9b4c1.png" },
                { "p250", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_p250.0bc9109121fb318a3bb18f6fa92692c7aa433205.png" },
                { "scar20", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_scar20.1552c7b64dfe9e542a3b730edb80e21dcc6d243d.png" },
                { "sg556", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_sg556.74040869391ea2ab25777f3670a6015191a73e6c.png" },
                { "ssg08", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_ssg08.271a856f50fd6ac1014334098b1a43d61bddb892.png" },
                { "knife", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_knife.a07b900d79ea768eae1a217a2839c5727f760396.png" },
                { "hegrenade", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapons/base_weapons/weapon_hegrenade.7b344756d5dbdda4fd2e583a227a670599889f59.png" }
            };
        private List<SQLWeaponDataModel> PreviousSQLWeapons;
        private List<SQLWeaponDataModel> SQLWeapons;
        private Chart _weaponChart;
        public Chart WeaponChart
        {
            get => _weaponChart;
            set
            {
                SetProperty(ref _weaponChart, value);
                OnPropertyChanged();
            }
        }

        private List<ChartEntry> _weaponChartEntrys;

        //geter seters
        private SteamGameStatsResponse _resultGameStats;
        private string _currentWeapon;
        public string CurrentWeapon
        {
            get => _currentWeapon;
            set => SetProperty(ref _currentWeapon, value);
        }
        private string _weapon;
        public string Weapon
        {
            get => _weapon;
            set => SetProperty(ref _weapon, value);
        }
        private ImageSource _weaponImage;
        public ImageSource WeaponImage
        {
            get => _weaponImage;
            set => SetProperty(ref _weaponImage, value);
        }

        private string _graphModeDropper;
        public string GraphModeDropper
        {
            get => _graphModeDropper;
            set
            {
                SetProperty(ref _graphModeDropper, value);
                OnGraphChange();
            }
        }

        private string _timeframeDropper;
        public string TimeframeDropper
        {
            get => _timeframeDropper;
            set
            {                
                SetProperty(ref _timeframeDropper, value);
                OnTimePeriodSelected();
                SetTrueValues();
            }
        }

        private Color _customRed = new Color(255, 0, 0);
        private Color _customGreen = new Color(0, 255, 0);
        private Color _customWhite = new Color(255, 255, 255);

        private string _rangeTimeframe;
        public string RangeTimeframe
        {
            get => _rangeTimeframe;
            set
            {
                value = "Changes Over " + value;
                SetProperty(ref _rangeTimeframe, value);
            }
        }

        private string _rangeAccuracy;
        public string RangeAccuracy
        {
            get => _rangeAccuracy;
            set
            {
                value = "Accuracy: " + value + "%";
                SetProperty(ref _rangeAccuracy, value);
            }
        }

        private string _rangeKills;
        public string RangeKills
        {
            get => _rangeKills;
            set
            {
                value = "Kills: " + value;
                SetProperty(ref _rangeKills, value);
            }
        }

        private string _rangeHits;
        public string RangeHits
        {
            get => _rangeHits;
            set
            {
                value = "Hits: " + value;
                SetProperty(ref _rangeHits, value);
            }
        }

        private string _rangeShots;
        public string RangeShots
        {
            get => _rangeShots;
            set
            {
                value = "Shots: " + value;
                SetProperty(ref _rangeShots, value);
            }
        }

        private string _rangeMisses;
        public string RangeMisses
        {
            get => _rangeMisses;
            set
            {
                value = "Misses: " + value;
                SetProperty(ref _rangeMisses, value);
            }
        }

        private string _globalAccuracy;
        public string GlobalAccuracy
        {
            get => _globalAccuracy;
            set
            {
                value = "Accuracy: " + value + "%";
                SetProperty(ref _globalAccuracy, value);
            }
        }

        private string _globalKills;
        public string GlobalKills
        {
            get => _globalKills;
            set
            {
                value = "Kills: " + value;
                SetProperty(ref _globalKills, value);
            }
        }

        private string _globalHits;
        public string GlobalHits
        {
            get => _globalHits;
            set
            {
                value = "Hits: " + value;
                SetProperty(ref _globalHits, value);
            }
        }

        private string _globalShots;
        public string GlobalShots
        {
            get => _globalShots;
            set
            {
                value = "Shots: " + value;
                SetProperty(ref _globalShots, value);
            }
        }

        private string _globalMisses;
        public string GlobalMisses
        {
            get => _globalMisses;
            set
            {
                value = "Misses: " + value;
                SetProperty(ref _globalMisses, value);
            }
        }

        private string _rangeAccuracyChange;
        public string RangeAccuracyChange
        {
            get => _rangeAccuracyChange;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Convert.ToSingle(value) > 0)
                    {
                        value = "↑" + value + "%";
                        _rangeAccuracyChangeColor = _customRed;
                    }
                    else if (Convert.ToSingle(value) == 0)
                    {
                        value = value + "%";
                        _rangeAccuracyChangeColor = _customWhite;
                    }
                    else
                    {
                        value = "↓" + value + "%";
                        _rangeAccuracyChangeColor = _customGreen;
                    }
                }
                SetProperty(ref _rangeAccuracyChange, value);
            }
        }

        private string _rangeKillsChange;
        public string RangeKillsChange
        {
            get => _rangeKillsChange;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Convert.ToSingle(value) > 0)
                    {
                        value = "↑" + value + "%";
                        _rangeKillsChangeColor = _customGreen;
                    }
                    else if (Convert.ToSingle(value) == 0)
                    {
                        value = value + "%";
                        _rangeKillsChangeColor = _customWhite;
                    }
                    else
                    {
                        value = "↓" + value + "%";
                        _rangeKillsChangeColor = _customRed;
                    }
                }
                SetProperty(ref _rangeKillsChange, value);
            }
        }

        private string _rangeHitsChange;
        public string RangeHitsChange
        {
            get => _rangeHitsChange;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Convert.ToSingle(value) > 0)
                    {
                        value = "↑" + value + "%";
                        _rangeHitsChangeColor = _customGreen;
                    }
                    else if (Convert.ToSingle(value) == 0)
                    {
                        value = value + "%";
                        _rangeHitsChangeColor = _customWhite;
                    }
                    else
                    {
                        value = "↓" + value + "%";
                        _rangeHitsChangeColor = _customRed;
                    }
                }
                SetProperty(ref _rangeHitsChange, value);
            }
        }

        private string _rangeShotsChange;
        public string RangeShotsChange
        {
            get => _rangeShotsChange;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Convert.ToSingle(value) > 0)
                    {
                        value = "↑" + value + "%";
                        _rangeShotsChangeColor = _customGreen;
                    }
                    else if (Convert.ToSingle(value) == 0)
                    {
                        value = value + "%";
                        _rangeShotsChangeColor = _customWhite;
                    }
                    else
                    {
                        value = "↓" + value + "%";
                        _rangeShotsChangeColor = _customRed;
                    }
                }
                SetProperty(ref _rangeShotsChange, value);
            }
        }

        private string _rangeMissesChange;
        public string RangeMissesChange
        {
            get => _rangeMissesChange;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Convert.ToSingle(value) > 0)
                    {
                        value = "↑" + value + "%";
                        _rangeMissesChangeColor = _customGreen;
                    }
                    else if (Convert.ToSingle(value) == 0)
                    {
                        value = value + "%";
                        _rangeMissesChangeColor = _customWhite;
                    }
                    else
                    {
                        value = "↓" + value + "%";
                        _rangeMissesChangeColor = _customRed;
                    }
                }
                SetProperty(ref _rangeMissesChange, value);
            }
        }

        private string _globalAccuracyChange;
        public string GlobalAccuracyChange
        {
            get => _globalAccuracyChange;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Convert.ToSingle(value) > 0)
                    {
                        value = "↑" + value + "%";
                        _globalAccuracyChangeColor = _customGreen;
                    }
                    else if (Convert.ToSingle(value) == 0)
                    {
                        value = value + "%";
                        _globalAccuracyChangeColor = _customWhite;
                    }
                    else
                    {
                        value = "↓" + value + "%";
                        _globalAccuracyChangeColor = _customRed;
                    }
                }
                SetProperty(ref _globalAccuracyChange, value);
            }
        }

        private string _globalKillsChange;
        public string GlobalKillsChange
        {
            get => _globalKillsChange;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Convert.ToSingle(value) > 0)
                    {
                        value = "↑" + value + "%";
                        _globalKillsChangeColor = _customGreen;
                    }
                    else if (Convert.ToSingle(value) == 0)
                    {
                        value = value + "%";
                        _globalKillsChangeColor = _customWhite;
                    }
                    else
                    {
                        value = "↓" + value + "%";
                        _globalKillsChangeColor = _customRed;
                    }
                }
                SetProperty(ref _globalKillsChange, value);
            }
        }

        private string _globalHitsChange;
        public string GlobalHitsChange
        {
            get => _globalHitsChange;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Convert.ToSingle(value) > 0)
                    {
                        value = "↑" + value + "%";
                        _globalHitsChangeColor = _customGreen;
                    }
                    else if (Convert.ToSingle(value) == 0)
                    {
                        value = value + "%";
                        _globalHitsChangeColor = _customWhite;
                    }
                    else
                    {
                        value = "↓" + value + "%";
                        _globalHitsChangeColor = _customRed;
                    }
                }
                SetProperty(ref _globalHitsChange, value);
            }
        }

        private string _globalShotsChange;
        public string GlobalShotsChange
        {
            get => _globalShotsChange;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Convert.ToSingle(value) > 0)
                    {
                        value = "↑" + value + "%";
                        _globalShotsChangeColor = _customGreen;
                    }
                    else if (Convert.ToSingle(value) == 0)
                    {
                        value = value + "%";
                        _globalShotsChangeColor = _customWhite;
                    }
                    else
                    {
                        value = "↓" + value + "%";
                        _globalShotsChangeColor = _customRed;
                    }
                }
                SetProperty(ref _globalShotsChange, value);
            }
        }

        private string _globalMissesChange;
        public string GlobalMissesChange
        {
            get => _globalMissesChange;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Convert.ToSingle(value) > 0)
                    {
                        value = "↑" + value + "%";
                        _globalMissesChangeColor = _customRed;
                    }
                    else if (Convert.ToSingle(value) == 0)
                    {
                        value = value + "%";
                        _globalMissesChangeColor = _customWhite;
                    }
                    else
                    {
                        value = "↓" + value + "%";
                        _globalMissesChangeColor = _customGreen;
                    }
                }
                SetProperty(ref _globalMissesChange, value);
            }
        }

        private Color _rangeAccuracyChangeColor;
        public Color RangeAccuracyChangeColor
        {
            get => _rangeAccuracyChangeColor;
            set
            {
                SetProperty(ref _rangeAccuracyChangeColor, value);
            }
        }

        private Color _rangeKillsChangeColor;
        public Color RangeKillsChangeColor
        {
            get => _rangeKillsChangeColor;
            set
            {
                SetProperty(ref _rangeKillsChangeColor, value);
            }
        }

        private Color _rangeHitsChangeColor;
        public Color RangeHitsChangeColor
        {
            get => _rangeHitsChangeColor;
            set
            {
                SetProperty(ref _rangeHitsChangeColor, value);
            }
        }

        private Color _rangeShotsChangeColor;
        public Color RangeShotsChangeColor
        {
            get => _rangeShotsChangeColor;
            set
            {
                SetProperty(ref _rangeShotsChangeColor, value);
            }
        }

        private Color _rangeMissesChangeColor;
        public Color RangeMissesChangeColor
        {
            get => _rangeMissesChangeColor;
            set
            {
                SetProperty(ref _rangeMissesChangeColor, value);
            }
        }

        private Color _globalAccuracyChangeColor;
        public Color GlobalAccuracyChangeColor
        {
            get => _globalAccuracyChangeColor;
            set
            {
                SetProperty(ref _globalAccuracyChangeColor, value);
            }
        }

        private Color _globalKillsChangeColor;
        public Color GlobalKillsChangeColor
        {
            get => _globalKillsChangeColor;
            set
            {
                SetProperty(ref _globalKillsChangeColor, value);
            }
        }

        private Color _globalHitsChangeColor;
        public Color GlobalHitsChangeColor
        {
            get => _globalHitsChangeColor;
            set
            {
                SetProperty(ref _globalHitsChangeColor, value);
            }
        }

        private Color _globalShotsChangeColor;
        public Color GlobalShotsChangeColor
        {
            get => _globalShotsChangeColor;
            set
            {
                SetProperty(ref _globalShotsChangeColor, value);
            }
        }

        private Color _globalMissesChangeColor;
        public Color GlobalMissesChangeColor
        {
            get => _globalMissesChangeColor;
            set
            {
                SetProperty(ref _globalMissesChangeColor, value);
            }
        }

        private string _score;
        public string Score
        {
            get => _score;
            set
            {
                value = "Score: " + value;
                SetProperty(ref _score, value);
            }
        }

        public string WeaponDataShots;
        public string WeaponDataHits;
        public string WeaponDataKills;

        public SteamGameStatsResponse ResultGameStats
        {
            get => _resultGameStats;
            set
            {
                //_resultStats = value;
                SetProperty(ref _resultGameStats, value);
            }
        }

        //fuctions

        public void PageSetup(SteamGameStatsResponse data)
        {
            WeaponImage = _favWeaponDictionary[Weapon];
            WeaponDataShots = data.playerstats.stats.Find(x => x.name.Equals($"total_shots_{Weapon}"))?.value.ToString() ?? "0";
            WeaponDataHits = data.playerstats.stats.Find(x => x.name.Equals($"total_hits_{Weapon}"))?.value.ToString() ?? "0";
            WeaponDataKills = data.playerstats.stats.Find(x => x.name.Equals($"total_kills_{Weapon}"))?.value.ToString() ?? "0";
            SetTrueValues();
            OnPropertyChanged();
        }

        public void SetDefaultValues()
        {
            RangeTimeframe = "1 Week";
            RangeKills = "0";
            RangeKillsChange = "0";
            RangeHits = "0";
            RangeHitsChange = "0";
            RangeShots = "0";
            RangeShotsChange = "0";
            RangeAccuracy = "0";
            RangeAccuracyChange = "0";
            RangeMisses = "0";
            RangeMissesChange = "0";
            GlobalKills = "0";
            GlobalKillsChange = "0";
            GlobalHits = "0";
            GlobalHitsChange = "0";
            GlobalShots = "0";
            GlobalShotsChange = "0";
            GlobalAccuracy = "0";
            GlobalAccuracyChange = "0";
            GlobalMisses = "0";
            GlobalMissesChange = "0";
            Score = "0";
            OnPropertyChanged();
        }

        public void SetTrueValues()
        {
            int weaponshots = 0;
            int weaponhits = 0;
            int weaponkills = 0;
            int weaponmisses = 0;
            float weaponaccuracy = 0f;
            if (!string.IsNullOrEmpty(WeaponDataShots))
            {
                weaponshots = Convert.ToInt32(WeaponDataShots);
            }
            if (!string.IsNullOrEmpty(WeaponDataHits))
            {
                weaponhits = Convert.ToInt32(WeaponDataHits);
            }
            if (!string.IsNullOrEmpty(WeaponDataKills))
            {
                weaponkills = Convert.ToInt32(WeaponDataKills);
            }
            weaponmisses = weaponshots - weaponhits;
            weaponaccuracy = Convert.ToSingle(weaponhits) / Convert.ToSingle(weaponshots);
            string weaponaccuracypercent = (Math.Round(weaponaccuracy, 2) * 100).ToString();
            GlobalAccuracy = weaponaccuracypercent;
            GlobalMisses = weaponmisses.ToString();
            GlobalHits = weaponhits.ToString();
            GlobalKills = weaponkills.ToString();
            GlobalShots = weaponshots.ToString();
            //LOCAL-------------------------------------------------------------------------------
            int localweaponshots = 0;
            int localweaponhits = 0;
            int localweaponkills = 0;
            int localweaponmisses = 0;
            float localweaponaccuracy = 0f;
            string localweaponaccuracypercent = "0";
            int globalweaponshotschange = 0;
            int globalweaponhitschange = 0;
            int globalweaponkillschange = 0;
            int globalweaponmisseschange = 0;
            float globalaccuracychange = 0f;
            string globalaccuracychangepercent = "0";
            if (SQLWeapons.Count > 0)
            {
                SQLWeaponDataModel oldentry = SQLWeapons[0];
                SQLWeaponDataModel recententry = SQLWeapons[SQLWeapons.Count-1];
                localweaponkills = Convert.ToInt32(recententry.weapon_kills) - Convert.ToInt32(oldentry.weapon_kills);
                localweaponhits = Convert.ToInt32(recententry.weapon_hits) - Convert.ToInt32(oldentry.weapon_hits);
                localweaponshots = Convert.ToInt32(recententry.weapon_shots) - Convert.ToInt32(oldentry.weapon_shots);
                localweaponmisses = localweaponshots - localweaponhits;
                localweaponaccuracy = Convert.ToSingle(localweaponhits) / Convert.ToSingle(localweaponshots);
                localweaponaccuracypercent = (Math.Round(localweaponaccuracy, 2) * 100).ToString();
                globalaccuracychange = (Convert.ToSingle(recententry.weapon_hits) / Convert.ToSingle(recententry.weapon_shots)) - (Convert.ToSingle(oldentry.weapon_hits) / Convert.ToSingle(oldentry.weapon_shots));
                globalaccuracychangepercent = (Math.Round(globalaccuracychange, 2) * 100).ToString();
                globalweaponshotschange = weaponshots - Convert.ToInt32(oldentry.weapon_shots);
                globalweaponhitschange = weaponhits - Convert.ToInt32(oldentry.weapon_hits);
                globalweaponkillschange = weaponkills - Convert.ToInt32(oldentry.weapon_kills);
                globalweaponmisseschange = weaponmisses - Convert.ToInt32(oldentry.weapon_shots - oldentry.weapon_hits);
            }
            RangeAccuracy = localweaponaccuracypercent;
            RangeMisses = localweaponmisses.ToString();
            RangeHits = localweaponhits.ToString();
            RangeKills = localweaponkills.ToString();
            RangeShots = localweaponshots.ToString();
            GlobalAccuracyChange = globalaccuracychangepercent;
            GlobalHitsChange = globalweaponhitschange.ToString();
            GlobalKillsChange = globalweaponkillschange.ToString();
            GlobalMissesChange = globalweaponmisseschange.ToString();
            GlobalShotsChange = globalweaponshotschange.ToString();
            //CHANGES-------------------------------------------------------------------------------
            int oldweaponshots = 0;
            int oldweaponhits = 0;
            int oldweaponkills = 0;
            int oldweaponmisses = 0;
            float oldweaponaccuracy = 0f;
            string changeweaponaccuracypercent = "0";
            int changeweaponshots = 0;
            int changeweaponhits = 0;
            int changeweaponkills = 0;
            int changeweaponmisses = 0;
            float changeweaponaccuracy = 0f;
            if (PreviousSQLWeapons.Count > 0)
            {
                SQLWeaponDataModel oldoldentry = PreviousSQLWeapons[0];
                SQLWeaponDataModel oldrecententry = PreviousSQLWeapons[SQLWeapons.Count - 1];
                oldweaponkills = Convert.ToInt32(oldrecententry.weapon_kills) - Convert.ToInt32(oldoldentry.weapon_kills);
                oldweaponhits = Convert.ToInt32(oldrecententry.weapon_hits) - Convert.ToInt32(oldoldentry.weapon_hits);
                oldweaponshots = Convert.ToInt32(oldrecententry.weapon_shots) - Convert.ToInt32(oldoldentry.weapon_shots);
                oldweaponmisses = oldweaponshots - oldweaponhits;
                oldweaponaccuracy = Convert.ToSingle(oldweaponhits) / Convert.ToSingle(oldweaponshots);

                changeweaponshots = weaponshots - oldweaponshots;
                changeweaponhits = weaponhits - oldweaponhits;
                changeweaponkills = weaponkills - oldweaponkills;
                changeweaponmisses = weaponmisses - oldweaponmisses;
                changeweaponaccuracy = weaponaccuracy - oldweaponaccuracy;
                changeweaponaccuracypercent = (Math.Round(changeweaponaccuracy, 2) * 100).ToString();
            }
            RangeAccuracyChange = changeweaponaccuracypercent;
            RangeHitsChange = changeweaponhits.ToString();
            RangeKillsChange = changeweaponkills.ToString();
            RangeMissesChange = changeweaponmisses.ToString();
            RangeShotsChange = changeweaponshots.ToString();

            int DaysToDisplay = 0;
            switch (TimeframeDropper)
            {
                case "1 Week":
                    DaysToDisplay = 7;
                    break;
                case "1 Month":
                    DaysToDisplay = 31;
                    break;
                case "3 Months":
                    DaysToDisplay = 63;
                    break;
                case "1 Year":
                    DaysToDisplay = 365;
                    break;
                case "All Time":
                    DaysToDisplay = SQLWeapons.Count - 1;
                    break;
            }
            if (DaysToDisplay > SQLWeapons.Count - 1)
            {
                DaysToDisplay = SQLWeapons.Count - 1;
            }
            List<float> Values = new List<float>();
            for (int i = 0; i < DaysToDisplay; i++)
            {
                (int, int, int, int, float) toaddtovalues = WorkOutDay(i);                
                switch (GraphModeDropper)
                {
                    case "Accuracy":
                        Values.Add(toaddtovalues.Item5);
                        break;
                    case "Kills":
                        Values.Add(Convert.ToSingle(toaddtovalues.Item1));
                        break;
                    case "Shots":
                        Values.Add(Convert.ToSingle(toaddtovalues.Item2));
                        break;
                    case "Hits":
                        Values.Add(Convert.ToSingle(toaddtovalues.Item3));
                        break;
                    case "Misses":
                        Values.Add(Convert.ToSingle(toaddtovalues.Item4));
                        break;
                }
            }
            for (int b = 1; b < Values.Count-1;b++)
            {
                ChartEntry chartEntry = new ChartEntry(Values[b]);
                chartEntry.Label = b.ToString();
                chartEntry.ValueLabel = Values[b].ToString();
                chartEntry.Color = SkiaSharp.SKColor.Parse("#000000");
                _weaponChartEntrys.Add(chartEntry);
            }
            var chart = new LineChart() { Entries = _weaponChartEntrys };
            WeaponChart = chart;

        }

        private (int,int,int,int,float) WorkOutDay(int day)
        {
            int oldweaponshots = 0;
            int oldweaponhits = 0;
            int oldweaponkills = 0;
            int oldweaponmisses = 0;
            float oldweaponaccuracy = 0f;
            if (SQLWeapons[day] != null && SQLWeapons[day-1] != null)
            {
                SQLWeaponDataModel oldentry = SQLWeapons[day];
                SQLWeaponDataModel recententry = SQLWeapons[day-1];
                oldweaponkills = Convert.ToInt32(recententry.weapon_kills) - Convert.ToInt32(oldentry.weapon_kills);
                oldweaponhits = Convert.ToInt32(recententry.weapon_hits) - Convert.ToInt32(oldentry.weapon_hits);
                oldweaponshots = Convert.ToInt32(recententry.weapon_shots) - Convert.ToInt32(oldentry.weapon_shots);
                oldweaponmisses = oldweaponshots - oldweaponhits;
                oldweaponaccuracy = Convert.ToSingle(oldweaponhits) / Convert.ToSingle(oldweaponshots);
            }
            (int,int,int,int,float) toret = (oldweaponkills, oldweaponshots, oldweaponhits, oldweaponmisses, oldweaponaccuracy);
            return toret;
        }

        public void OnGraphChange()
        {
            SetTrueValues();
        }



        public void OnTimePeriodSelected(bool def = false)
        {
            SQLWeapons.Clear();
            DateTime previoustoday = DateTime.Today;
            DateTime previouslowerbracket = previoustoday;
            DateTime today = DateTime.Today;
            DateTime lowerbracket = today;
            if (def)
            {
                lowerbracket = today.AddDays(-7);
                previoustoday = today.AddDays(-7);
                previouslowerbracket = today.AddDays(-14);
            }
            else
            {
                if (TimeframeDropper == "1 Week")
                {
                    lowerbracket = today.AddDays(-7);
                    previoustoday = today.AddDays(-7);
                    previouslowerbracket = today.AddDays(-14);
                    RangeTimeframe = "1 Week";
                }
                else if (TimeframeDropper == "1 Month")
                {
                    lowerbracket = today.AddMonths(-1);
                    previoustoday = today.AddMonths(-1);
                    previouslowerbracket = today.AddMonths(-2);
                    RangeTimeframe = "1 Month";
                }
                else if (TimeframeDropper == "3 Months")
                {
                    lowerbracket = today.AddMonths(-3);
                    previoustoday = today.AddMonths(-3);
                    previouslowerbracket = today.AddMonths(-6);
                    RangeTimeframe = "3 Months";
                }
                else if (TimeframeDropper == "1 Year")
                {
                    lowerbracket = today.AddYears(-1);
                    previoustoday = today.AddYears(-1);
                    previouslowerbracket = today.AddYears(-2);
                    RangeTimeframe = "1 Year";
                }
                else if (TimeframeDropper == "All Time")
                {
                    RangeTimeframe = "All Time";
                    lowerbracket = today.AddYears(-30);//WILL NOT WORK IN 2052
                }
            }
            List<SQLWeaponDataModel> sQLs = SQLDataService.GetSQLWeaponData(ApplicatationDataHandler.currentID, DateToStringForSQL(lowerbracket), DateToStringForSQL(today));
            SQLWeapons = GetUsefullWeapons(sQLs);
            List<SQLWeaponDataModel> sQLsprev = SQLDataService.GetSQLWeaponData(ApplicatationDataHandler.currentID, DateToStringForSQL(previouslowerbracket), DateToStringForSQL(previoustoday));
            PreviousSQLWeapons = GetUsefullWeapons(sQLsprev);
        }

        public IndividualWeaponPageVm()
        {
            SQLWeapons = new List<SQLWeaponDataModel>();
            PreviousSQLWeapons = new List<SQLWeaponDataModel>();
            _weaponChartEntrys = new List<ChartEntry>();
            SetDefaultValues();
            OnTimePeriodSelected(true);
        }

        private string DateToStringForSQL(DateTime indate)
        {
            string stringcon = "";
            stringcon += indate.Year.ToString();
            stringcon += "-";
            stringcon += indate.Month.ToString();
            stringcon += "-";
            stringcon += indate.Day.ToString();
            stringcon += "-";
            return stringcon;
        }

        private List<SQLWeaponDataModel> GetUsefullWeapons(List<SQLWeaponDataModel> sQLs)
        {
            List<SQLWeaponDataModel> temp = new List<SQLWeaponDataModel>();            
            for (int i = 0; i < sQLs.Count; i++)
            {
                if (sQLs[i].weapon_name == Weapon)
                {
                    temp.Add(sQLs[i]);
                }
            }
            return temp;
        }

        internal void Init(SteamGameStatsResponse resultGameStats, string currentWeapon)
        {
            ResultGameStats = resultGameStats;
            Weapon = currentWeapon;
            PageSetup(_resultGameStats);
        }
    }
}
