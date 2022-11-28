using Splorp.Core.Primitives;

namespace Splorp.Core;

public static class CanvasMath
{
    public static double DistanceTo(this Vector2 point1, Vector2 point2)
        => Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));

    public static bool IsInside(this Vector2 point, Rectangle rectangle)
        => point.X >= rectangle.Position.X
        && point.X <= rectangle.Position.X + rectangle.Width
        && point.Y >= rectangle.Position.Y
        && point.Y <= rectangle.Position.Y + rectangle.Height;

    public static bool IsInside(this Vector2 point, Circle circle)
        => point.DistanceTo(circle.Center) < circle.Radius;

    public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0)
            return min;
        if (value.CompareTo(max) > 0)
            return max;

        return value;
    }

    public static float DegreesToRadians(this float degrees)
        => (float)(Math.PI / 180) * degrees;

    public static float NextFloat(this Random random, float min, float max)
        => (float)random.NextDouble() * (max - min) + min;

    //https://stackoverflow.com/a/16391873/3985264
    public static bool IsInside(this Vector2 p, Polygon polygon)
    {
        // https://wrf.ecse.rpi.edu/Research/Short_Notes/pnpoly.html
        bool inside = false;
        for ( int i = 0, j = polygon.Points.Count - 1 ; i < polygon.Points.Count ; j = i++ )
        {
            if ( ( polygon.Points[i].Y > p.Y ) != ( polygon.Points[j].Y > p.Y ) &&
                p.X < ( polygon.Points[j].X - polygon.Points[i].X ) * ( p.Y - polygon.Points[i].Y ) / ( polygon.Points[j].Y - polygon.Points[i].Y ) + polygon.Points[i].X )
            {
                inside = !inside;
            }
        }

        return inside;
    }

    public static bool OverlapsWith(this Polygon polygon1, Polygon polygon2)
    {
        return polygon1.Points.Any(x => x.IsInside(polygon2));
    }

}