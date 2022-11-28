using Splorp.Core.Assets;
using Splorp.Core.Input;
using Splorp.Core.UI;
using Splorp.Core.UI.Text;

namespace Splorp.Core;

public abstract class Scene
{
    public MouseState Mouse = new();
    public KeyboardState Keyboard = new();
    private ICanvas canvas;
    private SceneManager sceneManager;

    public List<UIElement> UiElements { get; set; } = new();

    protected ICanvas _canvas { get; set; }
    protected List<GameObject> _gameObjects { get; set; } = new();

    protected TextArea _debugText { get; set; }
    
    protected readonly SceneManager _sceneManager;
    protected readonly IAssetManager _assetManager;

    public Scene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager)
    {
        _canvas = canvas;
        _sceneManager = sceneManager;
        _assetManager = assetManager;
    }

    public abstract void Draw();
}