using Tesseract.Droid;
using XFOCR.Droid.Service;
using XFOCR.Interface;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(Tess))]
namespace XFOCR.Droid.Service {
    public class Tess : ITess {

		static TesseractApi api;

		public Tess() {
            api = new TesseractApi(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.ApplicationContext, AssetsDeployment.OncePerVersion);
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