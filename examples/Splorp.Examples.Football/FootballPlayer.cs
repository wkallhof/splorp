using Splorp.Core;
using Splorp.Core.Primitives;

namespace Splorp.Examples.Football;

public class FootballPlayer : GameObject
{
    public float TurnSpeed = 180;
    public float ForwardSpeed = 180;
    public float SideStepSpeed = 70;
    public float BackwardSpeed = 60;

    public FootballPlayer(Vector2 position, float scale = 1, float rotation = 0) : base(position, scale, rotation)
    {
    }

    public override void Render(ICanvas canvas)
    {
        var bodyPosition = new Vector2(0, 0);
        var headPosition = new Vector2(0, -5);

        canvas.SetDrawColor(Color.Blue);
        canvas.FillCircle(bodyPosition, 15);
        canvas.SetDrawColor(Color.Black);
        canvas.StrokeCircle(bodyPosition, 15);

        canvas.SetDrawColor(Color.White);
        canvas.FillCircle(headPosition, 8);
        canvas.SetDrawColor(Color.Black);
        canvas.StrokeCircle(headPosition, 8);

        canvas.SetDrawColor(Color.Grey);
        canvas.DrawLine(headPosition, headPosition + new Vector2(0, -30));
    }
}