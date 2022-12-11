namespace Splorp.Core.Primitives;

public class Vector2
{
    public float X {get; set;}
    public float Y {get; set;}

    public float Magnitude => (float)Math.Sqrt((X*X) + (Y*Y));
    public float Length => Magnitude;

    public Vector2(float x, float y)
        => (X, Y) = (x, y);

    public Vector2 Normalize()
        => new(X/Length, Y/Length);

    public Vector2 Rotate(Vector2 center, float radians)
        => new(
            x:(float)(Math.Cos(radians) * (X - center.X) - Math.Sin(radians) * (Y - center.Y) + center.X),
            y:(float)(Math.Sin(radians) * (X - center.X) + Math.Cos(radians) * (Y - center.Y) + center.Y)
        );

    public Vector2 PerpRight()
        => new(-Y, X);

    public Vector2 PerpLeft()
        => new(Y, -X);

    public static Vector2 Right => new(1, 0);
    public static Vector2 Left => new(-1, 0);
    public static Vector2 Up => new(0, -1);
    public static Vector2 Down => new(0, 1);

    public static Vector2 operator +(Vector2 vector1, Vector2 vector2)
        => new(vector1.X + vector2.X, vector1.Y + vector2.Y);

    public static Vector2 operator -(Vector2 vector1, Vector2 vector2)
        => new(vector1.X - vector2.X, vector1.Y - vector2.Y);

    public static Vector2 operator *(Vector2 vector1, Vector2 vector2)
        => new(vector1.X * vector2.X, vector1.Y * vector2.Y);

    public static Vector2 operator *(Vector2 vector1, float value)
        => new(vector1.X * value, vector1.Y * value);

    public static Vector2 operator /(Vector2 vector1, Vector2 vector2)
        => new(vector1.X / vector2.X, vector1.Y / vector2.Y);

    public static Vector2 operator /(Vector2 vector1, float value)
        => new(vector1.X / value, vector1.Y / value);

    public override bool Equals(object? obj) => (obj is Vector2 other) && this == other;

    public static bool operator ==(Vector2 vector1, Vector2 vector2)
        => vector1.X == vector2.X && vector1.Y == vector2.Y;

    public static bool operator !=(Vector2 vector1, Vector2 vector2)
         => vector1.X != vector2.X || vector1.Y != vector2.Y;

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() => $"({X},{Y})";
}