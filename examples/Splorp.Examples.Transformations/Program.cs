using Splorp.Core;
using Splorp.Core.Assets;
using Splorp.Core.Primitives;
using Splorp.Core.UI.Text;
using Splorp.Sdl2;

using var canvas = new Sdl2Canvas(480, 640);
using var assetManager = new Sdl2AssetManager(canvas);

var splorp = new SplorpRunner(
    canvas: canvas,
    assetManager: assetManager,
    timer: new Sdl2Timer(),
    inputManager: new Sdl2InputManager()
);

splorp.Run<TransformationScene>();


public class TransformationScene : Scene
{
    private TextArea _debugText;

    private float _zoom = 1;
    
    private Rectangle _camera;

    private float _rotation = 0;

    private Vector2 _rightVector;

    private List<Rectangle> _rectangles = new();

    public TransformationScene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager) : base(canvas, assetManager, sceneManager)
    {

        _camera = new Rectangle(0, 0, _canvas.Width, _canvas.Height);
        _rightVector = new Vector2(_camera.Position.X + _camera.Width, _camera.Center.Y);

        var font = _assetManager.LoadFont("lazy.ttf", 20);
        _debugText = new TextArea(10, 10, Color.Black, font);
        UiElements.Add(_debugText);
    }

    public override void Draw()
    {
        _canvas.Clear();
        _canvas.SetDrawColor(Color.Black);

        _canvas.Save();

        _canvas.Translate(_camera.Position * -1);

        _canvas.Translate(_camera.Center);
        //_canvas.Scale(_zoom, _zoom);
        _canvas.Rotate(-_camera.Rotation);
        _canvas.Translate(_camera.Center * -1);

        _canvas.DrawRect(_camera);

        _rectangles.ForEach(x => RenderRectangle(x));

        _canvas.Restore();

        HandleInput();
    }

    private void RenderRectangle(Rectangle rectangle)
    {
        _canvas.Save();
        _canvas.Translate(rectangle.Center);
        _canvas.Rotate(rectangle.Rotation);
        _canvas.FillRect(-(rectangle.Width / 2), -(rectangle.Height / 2), rectangle.Width, rectangle.Height);
        _canvas.Restore();
    }

    private Vector2 GetWorldCoordinates(Vector2 input)
    {
        _canvas.Save();

        _canvas.Translate(_camera.Position);

        //_canvas.Translate(_camera.Center);
        //_canvas.Scale(_zoom, _zoom);
        //_canvas.Rotate(-_camera.Rotation);
       // _canvas.Translate(_camera.Center * -1);
        var newCoordinates = _canvas.Transform.ApplyTo(input);
        _canvas.Restore();
        return newCoordinates;
    }

    private void HandleInput()
    {
        if(Keyboard.Right.Down)
            _camera.Position += new Vector2(1, 0);

        if(Keyboard.Left.Down)
            _camera.Position -= new Vector2(1, 0);

        if(Keyboard.Up.Down)
            _camera.Position -= new Vector2(0, 1);

        if(Keyboard.Down.Down)
            _camera.Position += new Vector2(0, 1);

        if(Keyboard.A.Down)
            _camera.Rotation -= 0.02f;

        if(Keyboard.D.Down)
            _camera.Rotation += 0.02f;

        if(Keyboard.W.Down)
            _zoom *= 1.02f;
            
        if(Keyboard.S.Down)
            _zoom /= 1.02f;

        if(Keyboard.R.Down)
        {
            _zoom = 1;
            _camera.Rotation = 0;
            _camera.Position = new Vector2(0, 0);
            _rectangles.Clear();
        }

        if(Mouse.LeftButtonDown)
        {
            var position = GetWorldCoordinates(Mouse.Position);
            _rectangles.Add(new Rectangle(position, 10, 10));
        }
    }
}

