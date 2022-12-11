using MathNet.Numerics.LinearAlgebra;
using Splorp.Core.Primitives;

namespace Splorp.Core;

public static class CanvasMath
{
    public static double DistanceTo(this Vector2 point1, Vector2 point2)
        => Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));

    public static float AngleTo(this Vector2 point1, Vector2 point2)
        => (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

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

    public static Matrix<float> IdentityMatrix
        => M(new[,] {{1f, 0f, 0f },
                      {0f, 1f, 0f },
                      {0f, 0f, 1f}});

    public static Matrix<float> Translate(this Matrix<float> matrix, Vector2 amount)
            => matrix.Translate(amount.X, amount.Y);

    public static Matrix<float> Translate(this Matrix<float> matrix, float x, float y)
        => matrix * M(new [,] {{1f, 0f, x },
                               {0f, 1f, y },
                               {0f, 0f, 1f}});

    public static Matrix<float> Rotate(this Matrix<float> matrix, float radians)
        => matrix * M(new [,]  {{(float)Math.Cos(radians), (float)-Math.Sin(radians), 0f },
                                {(float)Math.Sin(radians), (float)Math.Cos(radians), 0f },
                                {0f, 0f, 1f}});

    public static Matrix<float> Scale(this Matrix<float> matrix, Vector2 amount)
            => matrix.Scale(amount.X, amount.Y);

    public static Matrix<float> Scale(this Matrix<float> matrix, float x, float y)
        => matrix * M(new [,] {{x, 0f, 0f },
                               {0f, y, 0f },
                               {0f, 0f, 1f}});

    public static float GetScale(this Matrix<float> matrix)
    {
        var point1 = matrix.ApplyTo(new Vector2(0, 0));
        var point2 = matrix.ApplyTo(new Vector2(0, 1));
        return (float)point1.DistanceTo(point2);
    }

    public static float GetRotation(this Matrix<float> matrix)
    {
        var point1 = matrix.ApplyTo(new Vector2(0, 0));
        var point2 = matrix.ApplyTo(new Vector2(0, 1));
        return point1.AngleTo(point2);
    }

    public static Matrix<float> ToIdentity(this Matrix<float> _) => IdentityMatrix;

    public static Vector2 ApplyTo(this Matrix<float> matrix, Vector2 input)
    {
        var (x, y) = matrix.ApplyTo(input.X, input.Y);
        return new Vector2(x, y);
    }

    public static (float x, float y) ApplyTo(this Matrix<float> matrix, float x, float y)
    {
        var inputMatrix = M(new[,] { { x }, 
                                     { y }, 
                                     { 1f } });

        var outputMatrix = matrix * inputMatrix;
        return (outputMatrix[0, 0], outputMatrix[1, 0]);
    }

    public static void ApplyTo(this Matrix<float> matrix, ref float x, ref float y)
    {
        var inputMatrix = M(new[,] { { x }, 
                                     { y }, 
                                     { 1f } });

        var outputMatrix = matrix * inputMatrix;
        x = outputMatrix[0, 0];
        y = outputMatrix[1, 0];
    }

    private static Matrix<float> M(float[,] matrix)
        => Matrix<float>.Build.DenseOfArray(matrix);
}