using System.Threading.Tasks;
using Tesseract.iOS;
using XFOCR.Interface;
using XFOCR.iOS.Service;

[assembly: Xamarin.Forms.Dependency(typeof(Tess))]
namespace XFOCR.iOS.Service {
	public class Tess : ITess {

		static TesseractApi api;

        public Tess() {
            api = new TesseractApi();
			Init();
        }

		private async void Init() {
			await api.Init("eng");
			await api.SetImage("image_path");
			string text = api.Text;
		}

		public bool Initialized() {
			return api.Initialized;
		}

		public async Task<bool> Init(string dict) {
			return await api.Init(dict);
		}

		public async Task<string> SetImage(byte[] data) {
			var result = await api.SetImage(data);
			if (result) {
				return api.Text;
			}
			return "fail";
		}

		public async Task<string> SetImage(string path) {
			var result = await api.SetImage(path);
			if (result) {
				return api.Text;
			}
			return "fail";
		}
	}
}