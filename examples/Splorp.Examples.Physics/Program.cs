using Splorp.Core;
using Splorp.Core.Assets;
using Splorp.Core.Primitives;
using Splorp.Core.UI.Text;
using Splorp.Sdl2;
using Physics = Splorp.Examples.Physics;

using var canvas = new Sdl2Canvas(600, 800);
using var assetManager = new Sdl2AssetManager(canvas);

var splorp = new SplorpRunner(
    canvas: canvas,
    assetManager: assetManager,
    timer: new Sdl2Timer(),
    inputManager: new Sdl2InputManager()
);

splorp.Run<PhysicsScene>();


public class PhysicsScene : Scene
{
    private TextArea _debugText;

    private Physics.Circle _playerCircle;
    private float _playerSpeed = 5;

    public PhysicsScene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager, ITimer timer) : base(canvas, assetManager, sceneManager, timer)
    {
        var font = _assetManager.LoadFont("lazy.ttf", 20);

        _debugText = new TextArea(10, 10, Color.White, font);
        UiElements.Add(_debugText);

        var random = new Random();

        for (var i = 0; i < 20; i++)
        {
            var randomX = random.NextFloat(50, canvas.Width - 50);
            var randomY = random.NextFloat(50, canvas.Height - 50);

            _gameObjects.Add(new Physics.Circle(
                position: new Vector2(randomX, randomY),
                mass: random.NextFloat(1, 5),
                font: font,
                color: Color.Random()));
        }

        _playerCircle = _gameObjects.OfType<Physics.Circle>().First();
    }

    public override void Draw()
    {
        _canvas.Clear(Color.Black);

         _debugText.Text = $"{_canvas.Width} {_canvas.Height}";

        var circles = _gameObjects.OfType<Physics.Circle>().ToList();

        for (var i = 0; i < circles.Count - 1 ; i++)
        {
            var circleA = circles[i];
            for (var j = i + 1; j < circles.Count; j++)
            {
                var circleB = circles[j];

                if(circleA.IsCollidingWith(circleB, out var normal, out var depth))
                {
                    circleA.RigidBody.Velocity += -normal! * (depth / 2f);
                    circleA.Transform.Position += -normal! * (depth / 2f);
                    circleB.Transform.Position += normal! * (depth / 2f);
                    circleB.RigidBody.Velocity += normal! * (depth / 2f);
                }
            }
        }

        circles.ForEach(x => x.RigidBody.Velocity *= 0.98f);

        _gameObjects.ForEach(x => 
        {
            _canvas.Save();
            _canvas.Translate(x.Transform.Position);
            _canvas.Rotate(x.Transform.Rotation.DegreesToRadians());
            x.Update();
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
        if(Keyboard.Right.Down)
            _playerCircle.RigidBody!.Velocity += Vector2.Right * _playerSpeed * _timer.DeltaTime;

        if(Keyboard.Left.Down)
            _playerCircle.RigidBody!.Velocity += Vector2.Left * _playerSpeed * _timer.DeltaTime;

        if(Keyboard.Up.Down)
            _playerCircle.RigidBody!.Velocity += Vector2.Up * _playerSpeed * _timer.DeltaTime;

        if(Keyboard.Down.Down)
            _playerCircle.RigidBody!.Velocity += Vector2.Down * _playerSpeed * _timer.DeltaTime;
    }
}

