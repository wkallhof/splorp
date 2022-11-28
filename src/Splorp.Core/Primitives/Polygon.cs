namespace Splorp.Core.Primitives;

public class Polygon
{
    public List<Vector2> Points {get; set;}

    public Polygon(List<Vector2> points)
    {
        Points = points;
    }

    public Polygon Offset(Vector2 vector)
    {
        Points = Points.Select(x => x+vector).ToList();
        return this;
    }
        
    public Polygon Rotate(Vector2 center, float radians){
        Points = Points.Select(x => x.Rotate(center, radians)).ToList();
        return this;
    }
}