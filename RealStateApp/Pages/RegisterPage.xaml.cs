using RealStateApp.Services;

namespace RealStateApp.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private void OnTogglePasswordVisibility(object sender, System.EventArgs e)
    {
        EntPassword.IsPassword = !EntPassword.IsPassword;
        BtnTogglePassword.Source = EntPassword.IsPassword ? "view.png" : "hide.png";
    }

    async void BtnRegister_Clicked(object sender, EventArgs e)
    {
        var response = await ApiService.RegisterUser(EntFullName.Text,EntEmail.Text,EntPassword.Text,EntPhone.Text);

        if (response)
        {
            await DisplayAlert("", "You Account Has Been Created", "Alright");
            await Navigation.PushModalAsync(new LoginPage());
        }
        else
        {
            await DisplayAlert("", "Oppps SomeThing Went Wrong", "Alright");
        }
    }

    private async void TapLogin_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new LoginPage());
    }
}