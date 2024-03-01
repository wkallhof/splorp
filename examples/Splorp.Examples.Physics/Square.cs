using Splorp.Core;
using Splorp.Core.Physics.Bodies;
using Splorp.Core.Primitives;
using Splorp.Core.UI.Text;

namespace Splorp.Examples.Physics
{
    public class Square : GameObject
    {
        private readonly Font _font;
        private readonly Color _color;

        public float Height { get; }
        public float Width { get; }

        public List<Vector2> Verticies { get; } = new();

        public Square(Vector2 position, float height, float width, float rotation, float mass, Font font, Color color) : base(position, rotation: rotation)
        {
            _font = font;
            _color = color;

            Height = height;
            Width = width;

            Verticies = new List<Vector2>()
            {
                new Vector2(-(width / 2), -(height / 2)),
                new Vector2(width / 2, -(height / 2)),
                new Vector2(width / 2, height / 2),
                new Vector2(-(width / 2), height / 2)
            };

            RigidBody = new SquareBody(this, Verticies, mass);
        }

        public override void Render(ICanvas canvas)
        {
            canvas.SetDrawColor(_color);
            canvas.FillRect(Verticies[0].X, Verticies[0].Y, Width, Height);

            canvas.SetDrawColor(Color.White);
            canvas.DrawText(new Vector2(0,0), Transform.Rotation.ToString(), _font);
        }
    }
}