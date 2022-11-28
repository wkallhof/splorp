using System;
using Splorp.Core.Input;
using Splorp.Core.Primitives;

namespace Splorp.Core.UI.Sliders
{
    public class Slider : UIElement
    {
        public Color FillColor {get; set;}
        public Color HandleColor {get; set;}
        public Action<float> OnUpdate {get; set;}

        public float Height {get; private set;}
        public float Width {get; private set;}

        public float MinValue {get; set;}
        public float MaxValue {get; set;}
        public float InitialValue {get; set;}

        private Rectangle _barArea;
        private bool _dragging;

        private bool _isVertical => Height > Width;
        
        public float Value {get; private set;}
        public float Percent {get; private set;}

        private Circle _handle;

        public Slider(float x, float y, float height, float width, float minValue, float maxValue, float initialValue, Color fillColor, Color handleColor, float handleRadius, Action<float> onUpdate) : base(x, y)
        {
            Height = height;
            Width = width;

            MinValue = minValue;
            MaxValue = maxValue;
            InitialValue = initialValue;

            _barArea = new Rectangle(x, y, width, height);
            FillColor = fillColor;
            HandleColor = handleColor;
            OnUpdate = onUpdate;
            Value = InitialValue;
            
            _handle = new Circle(
                position: _isVertical ? new Vector2(x + width/2, y) : new Vector2(x, y + height/2),
                radius: handleRadius
            );
        }

        public override void Update(MouseState mouse)
        {
            var mouseIsInBarArea = mouse.Position.IsInside(_barArea);
            var mouseIsInHandle = mouse.Position.IsInside(_handle);

            if(_dragging && !mouse.LeftButtonDown)
                _dragging = false;

            if(!_dragging && (mouseIsInBarArea || mouseIsInHandle) && mouse.LeftButtonDown)
                _dragging = true;

            if(!_dragging)
                return;

            if(_isVertical)
                _handle.Position.Y = mouse.Position.Y.Clamp(Position.Y, Position.Y + Height);
            else
                _handle.Position.X = mouse.Position.X.Clamp(Position.X, Position.X + Width);

            Percent = _isVertical ? ((_handle.Position.Y - Position.Y) / Height) : ((_handle.Position.X - Position.X) / Width);
            Value = MinValue + ((MaxValue - MinValue) *Percent);
            OnUpdate(Value);
        }

        public bool MouseIsOverHandleOrSliderBox(MouseState mouse)
            =>  mouse.Position.IsInside(_barArea) || mouse.Position.IsInside(_handle);

        public override void Render(ICanvas canvas)
        {
            canvas.SetDrawColor(FillColor);
            canvas.FillRect(Position.X, Position.Y, Width, Height);

            canvas.SetDrawColor(HandleColor);
            canvas.FillCircle(_handle);
        }
    }
}