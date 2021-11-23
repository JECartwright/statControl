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
        private string _steamProfileIdText;
        private readonly SteamService _steamService;
        private readonly IPageServiceZero _pageService;
        
        public ICommand MainStatPageCommand { get; }

        public string SteamProfileIdText
        {
            get => _steamProfileIdText;
            set => SetProperty(ref _steamProfileIdText, value);
        }
        

        private async Task MainStatPageCommandExecuteAsync()
        {
            //Testing connection
            //var result = await _steamService.GetStatsAsync(_steamProfileIdText);
            //Debug.WriteLine(result.payload.playerstats.steamID);
            //

            await _pageService.PushPageAsync<MainStatPage, MainStatPageVm>((vm) => { });
        }

        public HomePageVm(IPageServiceZero pageService, SteamService steamService)
        {
            _pageService = pageService;
            _steamService = steamService;
            MainStatPageCommand = new CommandBuilder().SetExecuteAsync(MainStatPageCommandExecuteAsync).SetName("Get Stats").Build();
        }
    }
}
