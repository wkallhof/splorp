using Splorp.Core.Input;
using Splorp.Core.Primitives;

namespace Splorp.Core.UI.Text
{
    public class TextArea : UIElement
    {
        public string Text { get; set; }
        public Color Color { get; set; }
        public bool MultiLine { get; set; }
        public float MultiLineWidth { get; set; }
        public Font Font { get; internal set; }

        public TextArea(float x, float y, Color color, Font font, string text = null, bool multiLine = false, float multiLineWidth = 0) : base(x, y)
        {
            Color = color;
            Font = font;
            Text = text;
            MultiLine = multiLine;
            MultiLineWidth = multiLineWidth;
        }

        public override void Render(ICanvas canvas)
        {
            canvas.SetDrawColor(Color);
            canvas.DrawText(Position, string.IsNullOrEmpty(Text)? " " : Text, Font, MultiLine, MultiLineWidth);
        }

        public override void Update(MouseState mouse){}
    }
}