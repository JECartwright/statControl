using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StatControl.Mvvm.Model.DisplayModel
{
    class WeaponSelectDisplayModel
    {
        public string WeaponName { get; set; }
        public string APIName { get; private set; }
        public ImageSource WeaponImage { get; set; }
        public int Kills { get; set; }
        public string KillsText { get; set; }
        public string AccuracyText { get; set; }
        public double Accuracy { get;  set; }
        public double ArcAngle { get;  set; }
        public int shots { get; set; }
        public int hits { get; set; }

        public void setAccuracy()
        {
            try
            {
                Accuracy = Convert.ToDouble(hits) / Convert.ToDouble(shots);
                Accuracy = Math.Round(Accuracy,2);
                ArcAngle = Accuracy;
                Accuracy = Accuracy * 100;
            }
            catch (DivideByZeroException)
            {
                Accuracy = 0;
            }
            if (double.IsNaN(Accuracy))
            {
                Accuracy = 0;
                ArcAngle = 0;
            }
            KillsText = "K:" + Kills.ToString();
            AccuracyText = "A:" + Accuracy.ToString();
        }

        public WeaponSelectDisplayModel(string Name, string DisplayName)
        {
            APIName = Name;
            WeaponName = DisplayName;
        }
    }
}
