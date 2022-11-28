using Splorp.Core.Primitives;

namespace Splorp.Core;

public abstract class GameObject
{
    public Vector2 Position { get; private set; }
    public Vector2 Velocity { get; set; } = new Vector2(0, 0);
    public Vector2 Direction => new Vector2((float)Math.Cos(Rotation.DegreesToRadians()), (float)Math.Sin(Rotation.DegreesToRadians())).Normalize();
    public float Rotation { get; private set; }

    public GameObject(float x, float y, float rotation)
            => (Position, Rotation) = (new Vector2(x, y), rotation);
}