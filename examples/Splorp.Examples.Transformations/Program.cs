using MathNet.Numerics.LinearAlgebra;
using Splorp.Core;
using Splorp.Core.Assets;
using Splorp.Core.Primitives;
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
    private Polygon _polygon;
    private Polygon _polygon2;

    public TransformationScene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager) : base(canvas, assetManager, sceneManager)
    {
        _polygon = new Polygon(new()
        {
            new(100, 100),
            new(300, 100),
            new(300, 300),
            new(100, 300)
        });

        _polygon2 = new Polygon(new()
        {
            new(150, 50),
            new(200, 50),
            new(200, 100),
            new(150, 100)
        });
    }

    public override void Draw()
    {
        _canvas.Clear();
        _canvas.SetDrawColor(Color.Black);

        var transformation = new Transformation();
        var box = GetBoundingBox(_polygon);

        if(Keyboard.Right.Down)
            transformation.Translate(5, 0);

        if(Keyboard.Left.Down)
            transformation.Translate(-5, 0);

        if(Keyboard.Up.Down)
            transformation.Translate(0, -5);

        if(Keyboard.Down.Down)
            transformation.Translate(0, 5);

        if(Keyboard.S.Down)
            transformation
                .Translate(-(box.Position.X + box.Width / 2), -(box.Position.Y + box.Height / 2))
                .Scale(0.99f, 0.99f)
                .Translate(box.Position.X + box.Width / 2, box.Position.Y + box.Height / 2);

        if(Keyboard.W.Down)
            transformation
                .Translate(-(box.Position.X + box.Width / 2), -(box.Position.Y + box.Height / 2))
                .Scale(1.01f, 1.01f)
                .Translate(box.Position.X + box.Width / 2, box.Position.Y + box.Height / 2);

        // if(Keyboard.R.Pressed)
        // {
        //     _polygon = new Polygon(new List<Vector2>()
        //     {
        //         new(100, 100),
        //         new(300, 100),
        //         new(300, 300),
        //         new(100, 300)
        //     });
        // }
        
        _canvas.DrawRect(box);

        if(Keyboard.A.Down)
            transformation
                .Translate(-(box.Position.X + box.Width / 2), -(box.Position.Y + box.Height / 2))
                .Rotate(0.02f)
                .Translate(box.Position.X + box.Width / 2, box.Position.Y + box.Height / 2);

        if(Keyboard.D.Down)
            transformation
                .Translate(-(box.Position.X + box.Width / 2), -(box.Position.Y + box.Height / 2))
                .Rotate(-0.02f)
                .Translate(box.Position.X + box.Width / 2, box.Position.Y + box.Height / 2);

        transformation.Apply(_polygon);
        transformation.Apply(_polygon2);

        _canvas.StrokePolygon(_polygon);
        _canvas.StrokePolygon(_polygon2);
    }

    private Rectangle GetBoundingBox(Polygon polygon)
    {
        var lowestX = polygon.Points.OrderBy(x => x.X).First().X;
        var lowestY = polygon.Points.OrderBy(x => x.Y).First().Y;
        var largestX = polygon.Points.OrderByDescending(x => x.X).First().X;
        var largestY = polygon.Points.OrderByDescending(x => x.Y).First().Y;

        return new Rectangle(lowestX, lowestY, largestY - lowestY, largestX - lowestX);
    }

    private class Transformation {

        private readonly List<Matrix<float>> _transforms = new();

        public Transformation Translate(Vector2 amount)
            => Translate(amount.X, amount.Y);

        public Transformation Translate(float x, float y)
        {
            _transforms.Add(M(new [,] {{1f, 0f, x },
                                        {0f, 1f, y },
                                        {0f, 0f, 1f}}));
            return this;
        }

        public Transformation Scale(Vector2 amount)
            => Scale(amount.X, amount.Y);

        public Transformation Scale(float x, float y)
        {
            _transforms.Add(M(new [,] {{x, 0f, 0f },
                                        {0f, y, 0f },
                                        {0f, 0f, 1f}}));
            return this;
        }

        public Transformation Rotate(float radians)
        {
            _transforms.Add(M(new [,] {{(float)Math.Cos(radians), (float)Math.Sin(radians), 0f },
                                        {(float)-Math.Sin(radians), (float)Math.Cos(radians), 0f },
                                        {0f, 0f, 1f}}));
            return this;
        }

        private Matrix<float> M(float[,] matrix)
            => Matrix<float>.Build.DenseOfArray(matrix);

        public Vector2 Apply(Vector2 input)
        {
            if(!_transforms.Any())
                return input;

            var inputMatrix = M(new[,] { { input.X }, 
                                         { input.Y }, 
                                         { 1f      } });

            _transforms.Reverse();
            var result = _transforms.Aggregate((m1, m2) => m1 * m2);
            var outputMatrix = result * inputMatrix;
            _transforms.Reverse();

            return new Vector2(outputMatrix[0, 0], outputMatrix[1, 0]);
        }

        public Polygon Apply(Polygon input)
        {
            input.Points = input.Points.Select(x => Apply(x)).ToList();
            return input;
        }
    }
}

