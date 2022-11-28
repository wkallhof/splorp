using Splorp.Core.Assets;
using Splorp.Core.UI.Text;
using static Splorp.Sdl2.Interops.SDL;
using static Splorp.Sdl2.Interops.SDL_image;
using static Splorp.Sdl2.Interops.SDL_ttf;


namespace Splorp.Sdl2;

public class Sdl2AssetManager : IAssetManager
{

    private Dictionary<string, Font> _fontMap = new();
    private Dictionary<string, Image> _imagePathTextureMap = new();

    private readonly nint _renderer;

    public Sdl2AssetManager(Sdl2Canvas canvas)
        => _renderer = canvas.Renderer;

    public Font LoadFont(string path, int size)
    {
        if(_fontMap.TryGetValue($"{path}-{size}", out Font? value))
            return value;

        var font = new Font(TTF_OpenFont(path, size), path, size);
        _fontMap.Add($"{path}-{size}", font);
        return font;
    }

    public Image LoadImage(string path)
    {
        if(_imagePathTextureMap.TryGetValue(path, out Image? value))
            return value;

        var image = new Image(IMG_LoadTexture(_renderer, path), path);
        _imagePathTextureMap[path] = image;

        return image;
    }

    public void Dispose()
    {
        _fontMap.Keys.ToList().ForEach(x => TTF_CloseFont(_fontMap[x].Pointer));
        _imagePathTextureMap.Keys.ToList().ForEach(x => SDL_DestroyTexture(_imagePathTextureMap[x].Pointer));
    }
}