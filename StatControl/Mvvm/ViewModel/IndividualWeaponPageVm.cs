using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using StatControl.Mvvm.View;
using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using System.Windows.Input;
using System.Threading.Tasks;
using StatControl.Mvvm.Model.DisplayModel;

namespace StatControl.Mvvm.ViewModel
{
    internal class IndividualWeaponPageVm : MvvmZeroBaseVm
    {
        internal void Init(WeaponSelectDisplayModel Weapon)
        {
            Console.WriteLine(Weapon.WeaponName.ToString());
        }
    }
}
