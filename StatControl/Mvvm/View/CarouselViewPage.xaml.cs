using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatControl.Mvvm.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselViewPage : CarouselPage
    {
        public CarouselViewPage()
        {
            InitializeComponent();
            this.CurrentPage = this.Children[1];
        }
    }
}