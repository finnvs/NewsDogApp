using Newsdog.Helpers;
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
            // Sync løsning - DR DK Feed
            LoadNewsDR();
            // Sync løsning - BBC Feed
            // LoadNewsBBC();
            // Sync løsning - Ny Times Feed
            // LoadNewsNyTimes();
            // Sync løsning - Buzzfeed
            // LoadNewsBuzzfeed();
            // Sync løsning - CNN Feed (har errors)
            // LoadNewsCnn();
            base.OnAppearing();
        }

        private async void LoadNewsAsync()
        {
            newsListView.IsRefreshing = true;            
            var news = await NewsHelper.GetTrendingAsync();
            this.BindingContext = news;
            newsListView.IsRefreshing = false;
        }

        private void LoadNewsDR()
        {            
            newsListView.IsRefreshing = true;            
            var news = NewsHelper.GetSimpleNewsDR();
            this.BindingContext = news;
            newsListView.IsRefreshing = false;
        }

        private void LoadNewsBBC()
        {
            newsListView.IsRefreshing = true;
            var news = NewsHelper.GetSimpleNewsBBC();
            this.BindingContext = news;
            newsListView.IsRefreshing = false;
        }

        private void LoadNewsNyTimes()
        {
            newsListView.IsRefreshing = true;
            var news = NewsHelper.GetSimpleNewsNyTimes();
            this.BindingContext = news;
            newsListView.IsRefreshing = false;
        }

        private void LoadNewsBuzzfeed()
        {
            newsListView.IsRefreshing = true;
            var news = NewsHelper.GetSimpleNewsBuzzfeed();
            this.BindingContext = news;
            newsListView.IsRefreshing = false;
        }

        private void LoadNewsCnn()
        {
            newsListView.IsRefreshing = true;
            var news = NewsHelper.GetSimpleNewsCnn();
            this.BindingContext = news;
            newsListView.IsRefreshing = false;
        }
    }
}
