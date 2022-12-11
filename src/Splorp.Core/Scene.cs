using Splorp.Core.Assets;
using Splorp.Core.Input;
using Splorp.Core.UI;
using Splorp.Core.UI.Text;

namespace Splorp.Core;

public abstract class Scene
{
    public MouseState Mouse = new();
    public KeyboardState Keyboard = new();

    public List<UIElement> UiElements { get; set; } = new();

    
    protected List<GameObject> _gameObjects { get; set; } = new();

    protected readonly ICanvas _canvas;
    protected readonly SceneManager _sceneManager;
    protected readonly IAssetManager _assetManager;
    protected readonly ITimer _timer;

    public Scene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager, ITimer timer)
    {
        _canvas = canvas;
        _sceneManager = sceneManager;
        _assetManager = assetManager;
        _timer = timer;
    }

    public abstract void Draw();
}