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
    public class HomePageVm
    {
        public IPageServiceZero _pageService;
        public ICommand StatPageCommand { get; }


        public HomePageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;
            StatPageCommand = new CommandBuilder().SetExecuteAsync(StatPageCommandExecuteAsync).SetName("Stats Page").Build();
        }

        private async Task StatPageCommandExecuteAsync()
        {
            await _pageService.PushPageAsync<StatPage, StatPageVm>((vm) => { });
        }
    }
}
