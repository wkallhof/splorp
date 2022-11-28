
namespace Splorp.Core.Primitives;

public record Line (Vector2 Start, Vector2 End)
{
    public double Length => Math.Sqrt(Math.Pow(End.X - Start.X, 2) + Math.Pow(End.Y - Start.Y, 2));

    public Line Rotate(Vector2 center, float radians)
        => new(Start.Rotate(center, radians), End.Rotate(center, radians));

    public bool Intersects(Line line2) {
        var det = (End.X - Start.X) * (line2.End.Y - line2.Start.Y) - (line2.End.X - line2.Start.X) * (End.Y - Start.Y);
        if (det == 0) {
            return false;

        } else {
            var lambda = ((line2.End.Y - line2.Start.Y) * (line2.End.X - Start.X) + (line2.Start.X - line2.End.X) * (line2.End.Y - Start.Y)) / det;
            var gamma = ((Start.Y - End.Y) * (End.X - Start.X) + (End.X - Start.X) * (line2.End.Y - Start.Y)) / det;
            return 0 < lambda && lambda < 1 && 0 < gamma && gamma < 1;
        }
    }
}