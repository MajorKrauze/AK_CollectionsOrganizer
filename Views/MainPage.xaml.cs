using AK_CollectionsOrganizer.Models;
using AK_CollectionsOrganizer.Views;

namespace AK_CollectionsOrganizer
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            FilesAccess.Instance.LoadAllCollections();

        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}

        private async void OnNaviClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AllCollectionsPage());
        }
    }
}