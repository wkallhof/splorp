namespace Splorp.Core.Primitives;

public class Rectangle : IPositionable, IRotatable
{
    public Vector2 Position {get; set;}
    public float Rotation {get; set;}
    public float Height {get; set;}
    public float Width {get; set;}

    public Rectangle(float x, float y, float width, float height, float rotation = 0)
        => (Position, Height, Width, Rotation) = (new Vector2(x, y), height, width, rotation);

    public Rectangle(Vector2 position, float width, float height, float rotation = 0)
        => (Position, Height, Width, Rotation) = (position, height, width, rotation);
        
}