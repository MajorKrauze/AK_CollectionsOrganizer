using AK_CollectionsOrganizer.Models;

namespace AK_CollectionsOrganizer.Views
{
    [QueryProperty(nameof(targetName), nameof(targetName))]

    public partial class CertainCollectionPage : ContentPage
    {

        Collection chosenCollection;

        string collectionName;

        public string targetName
        {
            get { return collectionName; }
            set { 
                collectionName = value;

                chosenCollection = FilesAccess.Instance.FindCollectionByName(collectionName); 
                
                if (chosenCollection != null)
                {
                    //CertainCollection.ItemsSource = FilesAccess.Instance.AllCollections;
                    CertainCollection.ItemsSource = chosenCollection.Collectables;

                    chosenCollection.LoadCollectablesToCollection();


                    CollectionName.Text = chosenCollection.Name;
                    BindingContext = this;
                }
            }
        }


        public CertainCollectionPage()
        {
            InitializeComponent();
            //CertainCollection.ItemsSource = chosenCollection.Collectables;
            BindingContext = this;

        }


        private async void OnAddCollectableClicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync($"///{nameof(AddCollectablePage)}?{nameof(AddCollectablePage.targetName)}={collectionName}");
            
            //await Shell.Current.GoToAsync($"///{nameof(AddCollectablePage)}?{nameof(AddCollectablePage.targetName)}={string.Empty}");

            //await Shell.Current.GoToAsync("///AddCollectablePage");
        }

        private async void CertainCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (e.SelectedItem != null)
            //{
            //    string selectedSchoolClass = (string)e.SelectedItem;
            //    // Do something with the selected SchoolClass
            //}
            if (e.CurrentSelection.Count != 0)
            {
                var aClass = (Collectable)e.CurrentSelection[0];
                //ClassesCollectionView.SelectedItem = null;

                await Shell.Current.GoToAsync($"///{nameof(AddCollectablePage)}?{nameof(AddCollectablePage.targetName)}={aClass.Name}");
                CertainCollection.SelectedItem = null;

            }

        }

        private async void OnReturnCollectableClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///AllCollectionsPage");
        }

        //private async void OnAddCollectableClicked(object sender, EventArgs e)
        //{
        //    await Shell.Current.GoToAsync($"{nameof(AddCollectablePage)}?{nameof(AddCollectablePage.targetName)}={string.Empty}");

        //    //await Shell.Current.GoToAsync("///AddCollectionPage");

        //}
    }
}