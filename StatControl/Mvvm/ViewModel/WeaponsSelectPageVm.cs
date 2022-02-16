using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using StatControl.Mvvm.View;
using FunctionZero.MvvmZero;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using StatControl.Mvvm.Model.DisplayModel;
using StatControl.Mvvm.Model.SteamGameStats;
using System.Diagnostics;
using StatControl.Mvvm.Model.ApplicationAPIData;


namespace StatControl.Mvvm.ViewModel
{
    internal class WeaponsSelectPageVm : MvvmZeroBaseVm
    {
        private readonly IPageServiceZero _pageService;
        private SteamGameStatsResponse _resultGameStats;
        private int _progressBarSize;
        public ObservableCollection<WeaponSelectDisplayModel> WeaponsDisplay { get; private set; }

        private async void StartWeaponPage(WeaponSelectDisplayModel weapon)
        {
            //--Temporary Fix For Deselecting, until Xamarin gets updated.
            await Task.Delay(10);
            SelectedWeapon = null;
            //--

            await _pageService.PushPageAsync<IndividualWeaponPage, IndividualWeaponPageVm>((vm) => vm.Init(_resultGameStats, weapon.APIName));
        }

        WeaponSelectDisplayModel _selectedWeapon;
        public WeaponSelectDisplayModel SelectedWeapon
        {
            get => _selectedWeapon;
            set
            {

                if (value != null)
                {
                    StartWeaponPage(value);
                }
                SetProperty(ref _selectedWeapon, value);
                OnPropertyChanged();
            }
        }

        public SteamGameStatsResponse ResultStats
        {
            get => _resultGameStats;
            set => SetProperty(ref _resultGameStats, value);
        }
        public int ProgressBarSize
        {
            get => _progressBarSize;
            set => SetProperty(ref _progressBarSize, value);
        }
        //for push
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

        public void OnStarted()
        {
            List<WeaponSelectDisplayModel> weapons = new List<WeaponSelectDisplayModel>
            {
                new WeaponSelectDisplayModel("deagle", "Deagle/R8"),
                new WeaponSelectDisplayModel("elite", "Dual Elites"),
                new WeaponSelectDisplayModel("fiveseven", "Five Seven"),
                new WeaponSelectDisplayModel("glock", "Glock"),
                new WeaponSelectDisplayModel("ak47", "AK47"),
                new WeaponSelectDisplayModel("aug", "AUG"),
                new WeaponSelectDisplayModel("awp", "AWP"),
                new WeaponSelectDisplayModel("famas", "Famas"),
                new WeaponSelectDisplayModel("g3sg1", "G3SG1"),
                new WeaponSelectDisplayModel("galilar", "Galil AR"),
                new WeaponSelectDisplayModel("m249", "M249"),
                new WeaponSelectDisplayModel("m4a1", "M4A1-S/M4A4"),
                new WeaponSelectDisplayModel("mac10", "MAC10"),
                new WeaponSelectDisplayModel("p90", "P90"),
                new WeaponSelectDisplayModel("ump45", "UMP 45"),
                new WeaponSelectDisplayModel("xm1014", "XM1014"),
                new WeaponSelectDisplayModel("bizon", "Bizon"),
                new WeaponSelectDisplayModel("mag7", "MAG7"),
                new WeaponSelectDisplayModel("negev", "Negev"),
                new WeaponSelectDisplayModel("sawedoff", "Sawed Off"),
                new WeaponSelectDisplayModel("tec9", "Tec9"),
                new WeaponSelectDisplayModel("taser", "Zeus"),
                new WeaponSelectDisplayModel("hkp2000", "P2000/USP-S"),
                new WeaponSelectDisplayModel("mp7", "MP7/MP5-SD"),
                new WeaponSelectDisplayModel("mp9", "MP9"),
                new WeaponSelectDisplayModel("nova", "Nova"),
                new WeaponSelectDisplayModel("p250", "P250"),
                new WeaponSelectDisplayModel("scar20", "Scar20"),
                new WeaponSelectDisplayModel("sg556", "SG556"),
                new WeaponSelectDisplayModel("ssg08", "SSG08"),
                new WeaponSelectDisplayModel("knife", "Knife"),
                new WeaponSelectDisplayModel("hegrenade", "Grenade")
            };

            for (int i = 0; i < ResultStats.playerstats.stats.Count; i++)
            {
                for (int b = 0; b < weapons.Count; b++)
                {
                    if (ResultStats.playerstats.stats[i].name == $"total_kills_{weapons[b].APIName}")
                    {
                        weapons[b].Kills = ResultStats.playerstats.stats[i].value;
                    }
                    else if (ResultStats.playerstats.stats[i].name == $"total_shots_{weapons[b].APIName}")
                    {
                        weapons[b].shots = ResultStats.playerstats.stats[i].value;
                    }
                    else if (ResultStats.playerstats.stats[i].name == $"total_hits_{weapons[b].APIName}")
                    {
                        weapons[b].hits = ResultStats.playerstats.stats[i].value;
                    }
                }
            }
            for (int c = 0; c < weapons.Count; c++)
            {
                weapons[c].WeaponImage = _favWeaponDictionary[weapons[c].APIName];
                weapons[c].setAccuracy();
                WeaponsDisplay.Add(weapons[c]);
            }
            Debug.WriteLine("Finished Setting Up Weapon Data");

        }

        public void PlatformHelper()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:                    
                    var screen = DeviceDisplay.MainDisplayInfo;
                    var x = (int)screen.Width;
                    
                    // if (x == 1080)
                    // {
                    //     ProgressBarSize = 24;
                    // }
                    // else if (x == 1440)
                    // {
                    //     ProgressBarSize = 30;
                    // }
                    // else if (x == 720)
                    // {
                    //     ProgressBarSize = 18;
                    // }
                    // else
                    // {
                    //     ProgressBarSize = 24;
                    // }
                    ProgressBarSize = (int)Math.Floor(0.0166666666666666667 * x + 6);
                    
                    break;
                case Device.UWP:
                    ProgressBarSize = 9;
                    break;
                default:
                    ProgressBarSize = 9;
                    break;
            }
        }

        public void DataRefresh()
        {
            if (ApplicatationDataHandler.CheckAPI)
            {
                ResultStats = ApplicatationDataHandler.resultStats;
            }
            OnPropertyChanged();
        }

        public WeaponsSelectPageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
            WeaponsDisplay = new ObservableCollection<WeaponSelectDisplayModel>();
            WeaponsDisplay.Clear();
        }
    }
}
