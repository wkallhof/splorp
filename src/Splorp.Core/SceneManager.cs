using Splorp.Core.Assets;

namespace Splorp.Core;

public class SceneManager
{
    public Scene? CurrentScene { get; internal set; }

    private readonly ICanvas _canvas;
    private readonly IAssetManager _assetManager;

    public SceneManager(ICanvas canvas, IAssetManager assetManager)
    {
        _canvas = canvas;
        _assetManager = assetManager;
    }

    public void LoadScene<T>() where T : Scene
    {
        CurrentScene = (Scene?)Activator.CreateInstance(typeof(T), _canvas, _assetManager, this);
    }
}