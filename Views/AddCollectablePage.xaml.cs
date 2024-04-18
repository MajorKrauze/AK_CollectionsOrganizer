using AK_CollectionsOrganizer.Models;

namespace AK_CollectionsOrganizer.Views
{


    [QueryProperty(nameof(targetName), nameof(targetName))]

    public partial class AddCollectablePage : ContentPage
    {
        Collectable collectableModel {  get; set; }
        

        string _targetName;
        public string targetName //Collection name
        {
            get { return _targetName; }
            set { _targetName = value; } // if (value != string.Empty) { CollectableName.Text = value; } }
        }
        //public Collection target { get; set; }

        public AddCollectablePage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            

            string name = CollectableName.Text;
            string description = CollectableDescription.Text;

            collectableModel = new Collectable(name, description);

            //if (targetName != string.Empty)
            //{
                //FilesAccess.Instance.RemoveCollectableToCollection(collectableModel, targetName);
            //}

            FilesAccess.Instance.AddCollectableToCollection(collectableModel, targetName);
            Collection collection = FilesAccess.Instance.FindCollectionByName(targetName);
            collection.SaveCollectablesToFile();
            //if (collection != null)
            //{
            //    collection.Collectables.Add(collectableModel);
            //}


            SaveButton.Text = "Saved";

            await Navigation.PopAsync();
            await Shell.Current.GoToAsync($"///{nameof(CertainCollectionPage)}?{nameof(CertainCollectionPage.targetName)}={targetName}");

            //await Shell.Current.GoToAsync("/..");
            //await Shell.Current.GoToAsync("///AllClassesPage");

            SaveButton.Text = "Save";
            CollectableName.Text = null;

        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            await Shell.Current.GoToAsync("///CertainCollectionPage");
        }
    }
}