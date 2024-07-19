using RealStateApp.Services;

namespace RealStateApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void TapJoinNow(object sender, TappedEventArgs e)
    {
		await Navigation.PushModalAsync(new RegisterPage());
    }

    private async void BtnLogin_Clicked(object sender, EventArgs e)
    {
        var response = await ApiService.Login(EntEmail.Text, EntPassword.Text);

        if (response)
        {
            // Create a new instance of the page
            var customTabPage = new CustomTabPage();

            // Set the MainPage to the new page
            Application.Current.MainPage = customTabPage;
            await Navigation.PushModalAsync(new CustomTabPage());
            // If you want to show an alert, you can do it after setting the MainPage
            // Make sure to call it on the main thread if necessary
            await MainThread.InvokeOnMainThreadAsync(async () => {
                await DisplayAlert("", "Your Account Has Been Created", "Alright");
            });
        }
        else
        {
            await DisplayAlert("", "Oppps SomeThing Went Wrong", "Alright");
        }
    }

    private void OnTogglePasswordVisibility(object sender, EventArgs e)
    {
        EntPassword.IsPassword = !EntPassword.IsPassword;
        BtnTogglePassword.Source = EntPassword.IsPassword ? "view.png" : "hide.png";
    }
}