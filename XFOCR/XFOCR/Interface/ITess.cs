
using System.Threading.Tasks;

namespace XFOCR.Interface {
    public interface ITess {
		bool Initialized();
		Task<bool> Init(string dict);
		Task<string> SetImage(byte[] data);
		Task<string> SetImage(string path);
	}
}
