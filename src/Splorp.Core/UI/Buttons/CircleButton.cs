using System;
using Splorp.Core.Input;
using Splorp.Core.Primitives;

namespace Splorp.Core.UI.Buttons
{
    public class CircleButton : Button
    {
        public float Radius {get; set;}

        public CircleButton(float x, float y, float radius, Color idleColor, Color hoverColor = null, Color activeColor = null, Action onPress = null)
            : base(x, y, idleColor, hoverColor, activeColor, onPress)
            => (Radius, IdleColor, HoverColor, ActiveColor, OnPress)
            = (radius, idleColor, hoverColor, activeColor, onPress);

        public override bool MouseIsInButtonArea(MouseState mouse)
            => Position.DistanceTo(mouse.Position) <= Radius;

        public override void Render(ICanvas canvas)
        {
            canvas.SetDrawColor(_fillColor);
            canvas.FillCircle(Position, Radius);
        }
    }
}