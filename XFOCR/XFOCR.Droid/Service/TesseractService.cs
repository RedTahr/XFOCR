using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(TesseractService))]
namespace XFOCR.Droid.Service {
    public class TesseractService : ITesseractService {
        
        public TesseractService() {
            TesseractApi api = new TesseractApi(context, AssetsDeployment.OncePerVersion);
        }
    }
}