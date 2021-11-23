using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using StatControl.Mvvm.View;
using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using System.Windows.Input;
using System.Threading.Tasks;
using StatControl.Services;
using System.Diagnostics;

namespace StatControl.Mvvm.ViewModel
{
    internal class HomePageVm : MvvmZeroBaseVm
    {
        private readonly SteamService _steamService;
        public IPageServiceZero _pageService;

        public ICommand MainStatPageCommand { get; }


        public HomePageVm(IPageServiceZero pageService, SteamService steamService)
        {
            _pageService = pageService;
            _steamService = steamService;
            MainStatPageCommand = new CommandBuilder().SetExecuteAsync(MainStatPageCommandExecuteAsync).SetName("Stats Page").Build();
        }

        private async Task MainStatPageCommandExecuteAsync()
        {
            var result = await _steamService.GetStatsAsync("76561198045733101");
            //Debug.WriteLine(result.payload.playerstats.gameName);

            await _pageService.PushPageAsync<MainStatPage, MainStatPageVm>((vm) => { });
        }
    }
}
