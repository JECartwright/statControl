using System;
using System.Collections.Generic;
using System.Text;
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
using System.ComponentModel;
using System.Collections.ObjectModel;
using StatControl.Mvvm.Model.DisplayModel;

namespace StatControl.Mvvm.ViewModel
{
    internal class SocialPageVm : MvvmZeroBaseVm
    {
        public ObservableCollection<SocialFriendsDisplayModel> Friends { get; private set; }
        public ObservableCollection<SocialPointsDisplayModel> PointsUsers { get; private set; }

        public SocialPageVm()  
        {

        }

    }
}
