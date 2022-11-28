using Splorp.Core.UI.Text;

namespace Splorp.Core.Assets
{
    public interface IAssetManager : IDisposable
    {
        Image LoadImage(string path);
        Font LoadFont(string path, int size);
    }
}