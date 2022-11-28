using System;
using Splorp.Core.Input;
using Splorp.Core.Primitives;

namespace Splorp.Core.UI.Buttons
{
    public abstract class Button : UIElement{
        public Color IdleColor {get; set;}
        public Color HoverColor {get; set;}
        public Color ActiveColor {get; set;}
        public Action OnPress {get; set;}

        protected Color _fillColor;
        protected bool _pressed;

        public Button(float x, float y, Color idleColor, Color hoverColor = null, Color activeColor = null, Action onPress = null)
            : base(x, y)
            => (Position, IdleColor, HoverColor, ActiveColor, OnPress)
            = (new Vector2(x, y), idleColor, hoverColor, activeColor, onPress);

        public abstract bool MouseIsInButtonArea(MouseState mouse);

        public override void Update(MouseState mouse)
        {
            var mouseIsInButtonArea = MouseIsInButtonArea(mouse);

            if(_pressed && (!mouse.LeftButtonDown || !mouseIsInButtonArea))
            {
                OnPress();
                _pressed = false;
            }

            if(!_pressed && mouseIsInButtonArea && mouse.LeftButtonDown)
                _pressed = true;

            _fillColor = mouse switch {
                MouseState s when ActiveColor is not null && s.LeftButtonDown && mouseIsInButtonArea => ActiveColor,
                MouseState s when HoverColor is not null && mouseIsInButtonArea => HoverColor,
                _ => IdleColor
            };
        }
    }
}