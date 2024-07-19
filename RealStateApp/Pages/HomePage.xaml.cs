using RealStateApp.Models;
using RealStateApp.Services;

namespace RealStateApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        lblUserName.Text = "Hi " + Preferences.Get("username", string.Empty);
        GetCategories();
        GetTrendingProperties();
    }
    private async void GetTrendingProperties()
    {
        var properties = await ApiService.GetTrendingProperties();
        CvTopPicks.ItemsSource = properties;
    }

    private async void GetCategories()
    {
        var categories = await ApiService.GetCategories();
        CvCategories.ItemsSource = categories;
    }
    private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
        if (currentSelection != null)
        {
            Navigation.PushAsync(new PropertiesListPage(currentSelection.Id, currentSelection.Name));
            ((CollectionView)sender).SelectedItem = null;
        }
        else
        {
            return;
        }

    }

    private void OnCategoryTapped(object sender, TappedEventArgs e)
    {
        var frame = sender as Frame;
        var category = frame.BindingContext as Category;
        if (category != null)
        {
            Navigation.PushAsync(new PropertiesListPage(category.Id, category.Name));
        }
    }

    private void CvTopPicks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as TrendingProperty;
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

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var frame = sender as Frame;
        var property = frame.BindingContext as TrendingProperty;
        if (property != null)
        {
            Navigation.PushModalAsync(new PropertyDetailPage(property.Id));
        }
    }

    private void TapSearch_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PushModalAsync(new SearchPage());
    }
}