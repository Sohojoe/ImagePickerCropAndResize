using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using FFImageLoading.Forms;

namespace ImagePickerSample
{
	public class StartPage : ContentPage
	{
		Label status = new Label();
		CachedImage ffImage = new CachedImage{
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			WidthRequest = 300,
			HeightRequest = 300,
			CacheDuration = TimeSpan.FromDays(30),
			DownsampleToViewSize = true,
			RetryCount = 0,
			RetryDelay = 250,
			TransparencyEnabled = false,
//			LoadingPlaceholder = "loading.png",
//			ErrorPlaceholder = "error.png",
			Source = "http://loremflickr.com/600/600/nature?filename=simple.jpg",
		};
		Image formsImage = new Image();

		public StartPage ()
		{
			var stack = new StackLayout {
				HorizontalOptions = LayoutOptions.Center,
			};
			stack.Children.Add (new Label { Text = " " }); // padding at top
			stack.Children.Add (status);
			stack.Children.Add (ffImage);
			stack.Children.Add (formsImage);
			status.Text = "init";
			this.Content = stack;
			Device.BeginInvokeOnMainThread (async() => await PickAsync ());
		}

		async Task PickAsync()
		{
			var mediaFile = await XamPluginMediaPicker.PickImageAsync ();
			if (mediaFile == null)
				return; // failed
			Debug.WriteLine (mediaFile);
			ffImage.Source = ImageSource.FromFile (mediaFile.Path);
			formsImage.Source = ImageSource.FromStream (() => mediaFile.GetStream());
//			ffImage.Source = ImageSource.FromStream (() => imageStream);
//			formsImage.Source = ImageSource.FromStream (() => imageStream);
		}


	}
}

