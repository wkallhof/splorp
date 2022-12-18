using MathNet.Numerics.LinearAlgebra;
using Splorp.Core;
using Splorp.Core.Assets;
using Splorp.Core.Input;
using Splorp.Core.Primitives;
using Splorp.Core.UI.Text;
using Splorp.Examples.Football;
using Splorp.Sdl2;

using var canvas = new Sdl2Canvas(480, 640);
using var assetManager = new Sdl2AssetManager(canvas);

var splorp = new SplorpRunner(
    canvas: canvas,
    assetManager: assetManager,
    timer: new Sdl2Timer(),
    inputManager: new Sdl2InputManager()
);

splorp.Run<FootballScene>();


public class FootballScene : Scene
{
    private TextArea _debugText;
    //private Image _player;

    private FootballPlayer Player1 = new(new Vector2(40, 40), rotation: 90);

    public FootballScene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager, ITimer timer) : base(canvas, assetManager, sceneManager, timer)
    {
        var font = _assetManager.LoadFont("lazy.ttf", 20);
        //_player = _assetManager.LoadImage("player.png");
        _debugText = new TextArea(10, 10, Color.Black, font);
        UiElements.Add(_debugText);
        _gameObjects.Add(Player1);

    }

    public override void Draw()
    {
        _canvas.Clear();

        _debugText.Text = $"{Player1.Transform.Rotation} {_timer.DeltaTime} {Player1.Transform.Right}";

        _gameObjects.ForEach(x => 
        {
            _canvas.Save();
            _canvas.Translate(x.Transform.Position);
            //_canvas.Scale(x.Transform.sc)
            _canvas.Rotate(x.Transform.Rotation.DegreesToRadians());
            x.Render(_canvas);
            _canvas.Restore();
        });

        HandleInput();
    }

    private void HandleInput()
    {
        if(Keyboard.R.Down)
        {
            _sceneManager.ResetCurrentScene();
        }

        if(Keyboard.Up.Down)
            Player1.Transform.Position += Player1.Transform.Forward * Player1.ForwardSpeed * _timer.DeltaTime;

        if(Keyboard.Down.Down)
            Player1.Transform.Position += Player1.Transform.Backwards * Player1.BackwardSpeed * _timer.DeltaTime;

        if(Keyboard.Right.Down && !Keyboard.LeftShift.Down)
            Player1.Transform.Rotation += Player1.TurnSpeed * _timer.DeltaTime;

        if(Keyboard.Left.Down && !Keyboard.LeftShift.Down)
            Player1.Transform.Rotation -= Player1.TurnSpeed * _timer.DeltaTime;

        if(Keyboard.Right.Down && Keyboard.LeftShift.Down)
            Player1.Transform.Position += Player1.Transform.Right * Player1.SideStepSpeed * _timer.DeltaTime;

        if(Keyboard.Left.Down && Keyboard.LeftShift.Down)
            Player1.Transform.Position += Player1.Transform.Left * Player1.SideStepSpeed * _timer.DeltaTime;
    }
}

