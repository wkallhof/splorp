using Splorp.Core.Primitives;

namespace Splorp.Core
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }

        public Vector2 Forward => new Vector2((float)Math.Sin(Rotation.DegreesToRadians()), -(float)Math.Cos(Rotation.DegreesToRadians())).Normalize();
        public Vector2 Backwards => Forward * -1;
        public Vector2 Left => Forward.PerpLeft();
        public Vector2 Right => Forward.PerpRight();

        public Transform(Vector2? position = null, float scale = 1, float rotation = 0)
        {
            Position = position ?? new Vector2(0, 0);
            Scale = scale;
            Rotation = rotation;
        }
    }
}