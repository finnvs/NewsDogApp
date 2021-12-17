using Newsdog.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Newsdog.Pages
{   
    public partial class TrendingPage : ContentPage
    {
        public TrendingPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            // Async løsning
            // LoadNewsAsync();
            // Sync løsning
            LoadNews();
            base.OnAppearing();
        }

        private async void LoadNewsAsync()
        {
            newsListView.IsRefreshing = true;            
            var news = await Helpers.NewsHelper.GetTrendingAsync();
            this.BindingContext = news;
            newsListView.IsRefreshing = false;
        }

        private void LoadNews()
        {
            newsListView.IsRefreshing = true;            
            var news = Helpers.NewsHelper.GetSimpleNews();
            this.BindingContext = news;
            newsListView.IsRefreshing = false;
        }
    }
}
