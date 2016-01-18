using System;

using Xamarin.Forms;
using FFImageLoading.Forms.Sample.ViewModels;
using DLToolkit.PageFactory;

namespace FFImageLoading.Forms.Sample.Pages
{
	public class CropTransformationPage : ContentPage
	{
		CropTransformationViewModel ViewModel;
		string GetRandomImageUrl(int width = 600, int height = 600)
		{
			return string.Format("http://loremflickr.com/{1}/{2}/nature?filename={0}.jpg", 
				Guid.NewGuid().ToString("N"), width, height);
		}
		public CropTransformationPage()
		{
			Title = "CropTransformation Demo";
			ViewModel = new CropTransformationViewModel ();
			ViewModel.ImagePath = GetRandomImageUrl ();

			var cachedImage = new CachedImage() {
				WidthRequest = 300f,
				HeightRequest = 300f,
				DownsampleToViewSize = true,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				CacheDuration = TimeSpan.FromDays(30),
				FadeAnimationEnabled = false,
				Source = GetRandomImageUrl()
			};

			cachedImage.SetBinding<CropTransformationViewModel>(CachedImage.TransformationsProperty, v => v.Transformations);
			cachedImage.SetBinding<CropTransformationViewModel>(CachedImage.LoadingPlaceholderProperty, v => v.LoadingImagePath);
			cachedImage.SetBinding<CropTransformationViewModel>(CachedImage.ErrorPlaceholderProperty, v => v.ErrorImagePath);
			cachedImage.SetBinding<CropTransformationViewModel>(CachedImage.SourceProperty, v => v.ImagePath);

			var imagePath = new Label() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				XAlign = TextAlignment.Center,
				FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
			};
			imagePath.SetBinding<TransformationExampleViewModel>(Label.TextProperty, v => v.ImagePath);

			var cropAddXButton = new Button() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Text = "X+",
				Command = new Command((o) => ViewModel.AddCurrentXOffsetCommad.Execute (o)),
			};

			var cropSubXButton = new Button() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Text = "X-",
				Command = new Command((o)=> ViewModel.SubCurrentXOffsetCommad.Execute(o)),
			};

			var cropAddYButton = new Button() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Text = "Y+",
				Command = new Command((o)=> ViewModel.AddCurrentYOffsetCommad.Execute(o)),
			};

			var cropSubYButton = new Button() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Text = "Y-",
				Command = new Command((o)=> ViewModel.SubCurrentYOffsetCommad.Execute(o)),
			};

			var cropAddZoomButton = new Button() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Text = "+",
				Command = new Command((o)=> ViewModel.AddCurrentZoomFactorCommad.Execute(o)),
			};

			var cropSubZoomButton = new Button() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Text = "-",
				Command = new Command((o)=> ViewModel.SubCurrentZoomFactorCommad.Execute(o)),
			};

			var buttonsLayout1 = new StackLayout() {
				Orientation = StackOrientation.Horizontal,
				Children = {
					cropAddXButton, 
					cropSubXButton,
					cropAddYButton,
					cropSubYButton,
				}
			};

			var buttonsLayout2 = new StackLayout() {
				Orientation = StackOrientation.Horizontal,
				Children = {
					cropAddZoomButton,
					cropSubZoomButton
				}
			};

			Content = new ScrollView() {
				Content = new StackLayout { 
					Children = {
						imagePath,
						cachedImage,
						buttonsLayout1,
						buttonsLayout2,
					}
				}
			};
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			ViewModel.ImagePath = null;
		}
	}
}


