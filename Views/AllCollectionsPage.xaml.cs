
using AK_CollectionsOrganizer.Models;

namespace AK_CollectionsOrganizer.Views;

public partial class AllCollectionsPage : ContentPage
{
    public AllCollectionsPage()
    {
        InitializeComponent();
        //FilesAccess.Instance.LoadAllCollections();
        AllCollectionsCollectionView.ItemsSource = FilesAccess.Instance.AllCollections;
        BindingContext = this;

    }

    private async void AllCollectionsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        if (e.CurrentSelection.Count != 0)
        {
            Collection aClass = (Collection)e.CurrentSelection[0];
            //ClassesCollectionView.SelectedItem = null;

            await Shell.Current.GoToAsync($"///{nameof(CertainCollectionPage)}?{nameof(CertainCollectionPage.targetName)}={aClass.Name}");
            AllCollectionsCollectionView.SelectedItem = null;

        }

    }

    private async void OnAddCollectionClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AddCollectionPage");
    }
}

//if (e.SelectedItem != null)
//{
//    string selectedSchoolClass = (string)e.SelectedItem;
//    // Do something with the selected SchoolClass
//}