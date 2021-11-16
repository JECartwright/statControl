using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StatControl.BoilerPlate;

namespace StatControl
{
    public partial class App : Application
    {

        public Locator locator { get; private set; }
        public App()
        {
            InitializeComponent();

            locator = new Locator(this);

            _ = locator.SetFirstPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
