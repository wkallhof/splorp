using Splorp.Core;
using Splorp.Core.Assets;
using Splorp.Core.Physics.Bodies;
using Splorp.Core.Primitives;
using Splorp.Core.UI.Text;

namespace Splorp.Examples.Physics;

public class Circle : GameObject
{
    private readonly Font _font;
    private readonly Color _color;

    public float Radius { get; init; }

    public Circle(Vector2 position, float mass, Font font, Color color, float rotation) : base(position, rotation: rotation)
    {
        _font = font;
        _color = color;

        Radius = mass * 10;

        RigidBody = new CircleBody(this, mass, Radius);
    }

    public override void Render(ICanvas canvas)
    {
        canvas.SetDrawColor(_color);
        canvas.FillCircle(new Vector2(0,0), Radius);

        canvas.SetDrawColor(Color.White);
        canvas.DrawText(new Vector2(0,0), Transform.Rotation.ToString(), _font);

        canvas.DrawLine(Vector2.Zero, Transform.Forward * Radius);
    }

    public bool IsCollidingWith(Circle circle)
    {
        return Transform.Position.DistanceTo(circle.Transform.Position) <= Radius + circle.Radius;
    }

    public bool IsCollidingWith(Circle circle, out Vector2? normal, out float depth)
    {
        normal = null;
        depth = 0;

        var distance = Transform.Position.DistanceTo(circle.Transform.Position);
        var radii = Radius + circle.Radius;

        var isColliding =  distance <= radii;
        if(!isColliding)
            return false;

        normal = (circle.Transform.Position - Transform.Position).Normalize();
        depth = radii - distance;
        return true;
    }
}