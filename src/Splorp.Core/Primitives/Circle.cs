namespace Splorp.Core.Primitives;

public record Circle : IPositionable
{
    public Vector2 Position {get; set;}
    public Vector2 Center => Position;
    public float Radius {get; init;}

    public Circle(Vector2 position, float radius)
        => (Position, Radius) = (position, radius);

    public Circle(float x, float y, float radius)
        => (Position, Radius) = (new Vector2(x, y), radius);

}