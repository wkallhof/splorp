using Splorp.Core.Assets;
using Splorp.Core.Primitives;
using Splorp.Core.UI.Text;

namespace Splorp.Core;

public interface ICanvas : IDisposable {
    public int Height {get; }
    public int Width {get; }
    public IntPtr Window {get; }
    public IntPtr Renderer {get; }
    public Color DrawColor {get; }

    public Vector2 CurrentTranslation { get; }
    public Vector2 CurrentScale { get; }
    public float CurrentRotation { get; }

    public void Save();
    public void Restore();

    public void Translate(Vector2 position);
    public void Translate(float x, float y);

    public void Scale(Vector2 amount);
    public void Scale(float x, float y);

    public void Rotate(float angle);

    public void Resize(int width, int height);

    public void RenderPresent();
    public void Clear();
    public void SetDrawColor(Color color);

    public void DrawPixel(Vector2 position);
    public void DrawPixel(float x, float y);

    public void DrawText(Vector2 position, string text, Font font, bool multiLine = false, float multiLineWidth = 0);

    public void DrawRect(float x, float y, float w, float h);

    public void DrawRect(Rectangle rectangle);
    public void FillRect(float x, float y, float w, float h);

    public void FillRect(Rectangle rectangle);
    public void StrokePolygon(Polygon polygon);

    public void StrokeCircle(Circle circle);

    public void DrawCircle(Vector2 center, float radius);
    public void FillCircle(Circle circle);

    public void FillCircle(Vector2 center, float radius);

    public void DrawLine(Vector2 start, Vector2 end);

    public void DrawLine(float x1, float y1, float x2, float y2);

    public void DrawLineWidth(float x1, float y1, float x2, float y2, float width);

    public void DrawImage(Image image, float x, float y, float? w = null, float? h = null, float rotation = 0.0f);

    public new void Dispose();
}