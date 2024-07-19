
using RealStateApp.Models;
using RealStateApp.Services;

namespace RealStateApp.Pages;

public partial class PropertyDetailPage : ContentPage
{
	private string PhoneNumber;
	private bool IsBookMarkEnabled;
	private int propertyId;
	private int bookmarkId;
	public PropertyDetailPage(int PropertyId)
	{
		InitializeComponent();
		GetPropertyDetail(PropertyId);
		this.propertyId = PropertyId;
	}

    private async void GetPropertyDetail(int propertyId)
    {
		var property = await ApiService.GetPropertyDetail(propertyId);

		LblPrice.Text = property.Price.ToString();
		LblDescription.Text = property.Detail;
		LblAddress.Text = property.Address;
		ImgProperty.Source = property.FullImageUrl;
		PhoneNumber = property.Phone.ToString();

		if(property.Bookmarks != null)
		{
			ImgbookmarkBtn.Source = "bookmark_empty_icon";
			bookmarkId =property.Bookmarks.Id;
			IsBookMarkEnabled = false;
		}
		else
		{
			ImgbookmarkBtn.Source =  property.Bookmarks.Status ? "bookmark_fill_icon" : "bookmark_empty_icon";
			IsBookMarkEnabled= true;
        }
	}

    private void TapMessage_Tapped(object sender, TappedEventArgs e)
    {
		var message = new SmsMessage("Hi Im Intrested In you Property",PhoneNumber);
		Sms.ComposeAsync(message);
    }

    private void TapCall_Tapped(object sender, TappedEventArgs e)
    {
		PhoneDialer.Open(PhoneNumber);
    }

    private void ImgbackBtn_Clicked(object sender, EventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private async void ImgbookmarkBtn_Clicked(object sender, EventArgs e)
    {
		if (IsBookMarkEnabled == false)
		{
			//Add A bookMark
			var AddBookMark = new AddBookmark()
			{
				Id = Preferences.Get("userid", 0),
				PropertyId = propertyId
			};

			var response = await ApiService.AddBookMard(addbookmark: AddBookMark);
			if (response)
			{
				ImgbookmarkBtn.Source = "bookmark_fill_icon";
				IsBookMarkEnabled = true;

            }
		
		}
		else
		{
			var response = await ApiService.DeleteBookMard(bookmarkId);
			if (response)
			{
				ImgbookmarkBtn.Source = "bookmark_empty_icon";
				IsBookMarkEnabled = false;
			}
			//Remove A bookMark
        }
    }
}