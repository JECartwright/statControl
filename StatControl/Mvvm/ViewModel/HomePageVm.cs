using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using StatControl.Mvvm.View;
using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using System.Windows.Input;
using System.Threading.Tasks;

namespace StatControl.Mvvm.ViewModel
{
    internal class HomePageVm : MvvmZeroBaseVm
    {
        public IPageServiceZero _pageService;
        public ICommand MainStatPageCommand { get; }


        public HomePageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
            MainStatPageCommand = new CommandBuilder().SetExecuteAsync(MainStatPageCommandExecuteAsync).SetName("Stats Page").Build();
        }

        private async Task MainStatPageCommandExecuteAsync()
        {
            await _pageService.PushPageAsync<MainStatPage, MainStatPageVm>((vm) => { });
        }
    }
}
