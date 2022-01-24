using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace StatControl.Mvvm.Model.DisplayModel
{
    internal class SocialProfileDisplayModel
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public ImageSource ProfilePicture { get; set; }
        public string Score { get; set; }
    }
}
