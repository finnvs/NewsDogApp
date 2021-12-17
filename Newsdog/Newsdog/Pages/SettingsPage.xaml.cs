using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Newsdog.Pages
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            InitializeSettings();

            base.OnAppearing();
        }

        private void InitializeSettings()
        {            
            //articleCountSlider.Value = 10;
            //categoryPicker.SelectedIndex = 1;
        }

    }
}
