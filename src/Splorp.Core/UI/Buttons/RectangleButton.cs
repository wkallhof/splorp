using Splorp.Core.Input;
using Splorp.Core.Primitives;

namespace Splorp.Core.UI.Buttons
{
    public class RectangleButton : Button
    {
        public Rectangle ButtonArea {get; set;}

        public RectangleButton(float x, float y, int width, int height, Color idleColor, Color hoverColor = null, Color activeColor = null, Action onPress = null)
            : base(x, y, idleColor, hoverColor, activeColor, onPress)
            => (ButtonArea, IdleColor, HoverColor, ActiveColor, OnPress)
            = (new Rectangle(x, y, width, height), idleColor, hoverColor, activeColor, onPress);

        public override bool MouseIsInButtonArea(MouseState mouse)
            => mouse.Position.IsInside(ButtonArea);

        public override void Render(ICanvas canvas)
        {
            canvas.SetDrawColor(_fillColor);
            canvas.FillRect(ButtonArea);
        }
    }
}