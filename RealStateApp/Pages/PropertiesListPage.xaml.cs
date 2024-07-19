using RealStateApp.Models;
using RealStateApp.Services;
using System.Diagnostics;

namespace RealStateApp.Pages;

public partial class PropertiesListPage : ContentPage
{
	public PropertiesListPage(int id,string Name)
	{
		Title = Name;
		InitializeComponent();
		GetPropertiesList(id);
	}


	private async void GetPropertiesList(int id)
	{
		var properties = await ApiService.GetPropertyByCategory(id);

		CvProperties.ItemsSource = properties;

	}

    private void CvProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as PropertyByCategory;
        if (currentSelection != null)
        {
            Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));
            ((CollectionView)sender).SelectedItem = null;
        }
        else
        {
            return;
        }

    }
}