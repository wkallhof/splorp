using Splorp.Core.Assets;

namespace Splorp.Core;

public class SceneManager
{
    public Scene? CurrentScene { get; internal set; }

    private readonly ICanvas _canvas;
    private readonly IAssetManager _assetManager;
    private readonly ITimer _timer;

    public SceneManager(ICanvas canvas, IAssetManager assetManager, ITimer timer)
    {
        _canvas = canvas;
        _assetManager = assetManager;
        _timer = timer;
    }

    public void LoadScene<T>() where T : Scene
        => LoadScene(typeof(T));

    public void LoadScene(Type sceneType)
    {
        CurrentScene = (Scene?)Activator.CreateInstance(sceneType, _canvas, _assetManager, this, _timer);
    }

    public void ResetCurrentScene()
        => LoadScene(CurrentScene!.GetType());
}