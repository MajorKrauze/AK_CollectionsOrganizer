using AK_CollectionsOrganizer.Models;

namespace AK_CollectionsOrganizer.Views;

public partial class AddCollectionPage : ContentPage
{
    Collection collectionModel;
    public AddCollectionPage()
    {
        InitializeComponent();
        CollectionName.Text = string.Empty;
        BindingContext = this;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        collectionModel = new Collection(CollectionName.Text);
        FilesAccess.Instance.AddCollectionToCollections(collectionModel);

        //FilesAccess.Instance.AllCollections.Add(collectionModel);
        //FilesAccess.Instance.SaveCollections();


        SaveButton.Text = "Saved";

        //await Navigation.PopAsync();

        await Shell.Current.GoToAsync("///AllCollectionsPage");

        SaveButton.Text = "Save";
        CollectionName.Text = null;

    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

        await Shell.Current.GoToAsync("///AllCollectionsPage");
    }
}