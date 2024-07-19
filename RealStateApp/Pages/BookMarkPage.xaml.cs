
using RealStateApp.Models;
using RealStateApp.Services;

namespace RealStateApp.Pages;

public partial class BookMark : ContentPage
{
	public BookMark()
	{
		InitializeComponent();
		GetPropertiesList();
	}

    private async void GetPropertiesList()
    {
		var properties = await ApiService.GetBookmarkList();
		CvProperties.ItemsSource = properties;

	}

    private void CvProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as BookmarkList;
        if (currentSelection != null)
        {
            Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.PropertyId));
            ((CollectionView)sender).SelectedItem = null;
        }
        else
        {
            return;
        }
    }
}