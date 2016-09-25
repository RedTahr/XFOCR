using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XFOCR.Interface;

namespace XFOCR.View {
	// https://github.com/jamesmontemagno/MediaPlugin/blob/master/samples/MediaSample/MediaSample/MediaPage.xaml.cs
	public partial class HomePage : ContentPage {

		ITess tess;

		public HomePage() {
			InitializeComponent();

			prepOCR();

			takePhoto.Clicked += TakePhoto_Clicked;

			pickPhoto.Clicked += PickPhoto_Clicked;

			takeVideo.Clicked += TakeVideo_Clicked;

			pickVideo.Clicked += PickVideo_Clicked;
		}

		private async void prepOCR() {
			tess = DependencyService.Get<ITess>();
			if (!tess.Initialized()) {
				await tess.Init("eng");
			}
		}

		private async void PickVideo_Clicked(object sender, EventArgs e) {
			if (!CrossMedia.Current.IsPickVideoSupported) {
				DisplayAlert("Videos Not Supported", ":( Permission not granted to videos.", "OK");
				return;
			}
			var file = await CrossMedia.Current.PickVideoAsync();

			if (file == null)
				return;

			DisplayAlert("Video Selected", "Location: " + file.Path, "OK");
			file.Dispose();
		}

		private async void TakeVideo_Clicked(object sender, EventArgs e) {
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported) {
				DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
				return;
			}

			var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions {
				Name = "video.mp4",
				Directory = "DefaultVideos",
			});

			if (file == null)
				return;

			DisplayAlert("Video Recorded", "Location: " + file.Path, "OK");

			file.Dispose();
		}

		private async void PickPhoto_Clicked(object sender, EventArgs e) {
			if (!CrossMedia.Current.IsPickPhotoSupported) {
				DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
				return;
			}
			var file = await CrossMedia.Current.PickPhotoAsync();


			if (file == null)
				return;

			image.Source = ImageSource.FromStream(() => {
				var stream = file.GetStream();
				file.Dispose();
				return stream;
			});
		}

		private async void TakePhoto_Clicked(object sender, EventArgs e) {
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
				DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
				return;
			}

			var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions {

				Directory = "Sample",
				Name = "test.jpg"
			});

			if (file == null)
				return;

			DisplayAlert("File Location", file.Path, "OK");


			// double check
			if (!tess.Initialized()) {
				await tess.Init("eng");
			}

			byte[] imageBytes = new byte[0];
			System.IO.Stream stream;

			image.Source = ImageSource.FromStream(() => {
				stream = file.GetStream();
				return stream;
			});

			stream = file.GetStream();

			// get a byte[]
			imageBytes = new byte[stream.Length];
			stream.Position = 0;
			stream.Read(imageBytes, 0, (int)stream.Length);
			stream.Position = 0;

			var temp = await tess.SetImage(imageBytes);
			ocred.Text = temp;

			var result = await tess.SetImage(file.Path);
			ocred.Text = result;

			file.Dispose();
		}
	}
}
