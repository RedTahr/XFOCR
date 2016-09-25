using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(TesseractService))]
namespace XFOCR.iOS.Service {
    public class TesseractService : ITesseractService {
        public TesseractService() {
            TesseractApi api = new TesseractApi();
            await api.Init("eng");
            await api.SetImage("image_path");
            string text = api.Text;
        }
    }
}