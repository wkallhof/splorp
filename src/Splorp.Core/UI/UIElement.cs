using Splorp.Core.Input;
using Splorp.Core.Primitives;

namespace Splorp.Core.UI
{
    public abstract class UIElement : IPositionable, IRotatable
    {
        public Vector2 Position {get; set;}
        public float Rotation {get; set;}

        public UIElement(float x, float y, float rotation = 0)
            => (Position, Rotation) = (new Vector2(x, y), rotation);

        public abstract void Render(ICanvas canvas);
        public abstract void Update(MouseState mouse);
    }
}