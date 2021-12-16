using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StatControl.Mvvm.Model.DisplayModel
{
    class AchievementDisplayModel
    {
        public string APIName {get;set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public ImageSource ImageAdress { get; set; }
        public int Achieved { get; set; }
        public string AchievedText { get; set; }
        public Color AchievedColor { get; set; }
    }
}
