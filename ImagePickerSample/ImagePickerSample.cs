using System;

using Xamarin.Forms;
using FFImageLoading.Forms.Sample.ViewModels;
using DLToolkit.PageFactory;
using FFImageLoading.Forms.Sample.Pages;

namespace ImagePickerSample
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
			FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
//			MainPage = new XamarinFormsPageFactory().Init<HomeViewModel, PFNavigationPage>();
			MainPage = new PickerPage ();
//			MainPage = new CropTransformationPage ();
//			MainPage = new ContentPage {
//				Content = new StackLayout {
//					VerticalOptions = LayoutOptions.Center,
//					Children = {
//						new Label {
//							XAlign = TextAlignment.Center,
//							Text = "Welcome to Xamarin Forms!"
//						}
//					}
//				}
//			};
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

