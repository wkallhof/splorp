using System.Runtime.InteropServices;
using MathNet.Numerics.LinearAlgebra;
using Splorp.Core;
using Splorp.Core.Assets;
using Splorp.Core.Primitives;
using Splorp.Core.UI.Text;
using static Splorp.Core.CanvasMath;
using static Splorp.Sdl2.Interops.SDL;
using static Splorp.Sdl2.Interops.SDL_gfx;
using static Splorp.Sdl2.Interops.SDL_image;
using static Splorp.Sdl2.Interops.SDL_ttf;


namespace Splorp.Sdl2
{
    public class Sdl2Canvas : ICanvas {
        public int Height {get; private set; }
        public int Width {get; private set; }
        public IntPtr Window {get; private set; }
        public IntPtr Renderer {get; private set; }

        public Color DrawColor {get; private set; }

        public Matrix<float> Transform => _currentTransform;

        private Matrix<float> _currentTransform = IdentityMatrix;
        private Stack<Matrix<float>> _transformStack = new();

        public Sdl2Canvas(int height, int width){
            Height = height;
            Width = width;

            SDL_Init(SDL_INIT_VIDEO);
            TTF_Init();
            Window = SDL_CreateWindow("Canvas", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, width, height, SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
            Renderer = SDL_CreateRenderer(Window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            DrawColor = new Color(0, 0, 0, 0);
        }

        public void Resize(int width, int height){
            Height = height;
            Width = width;
        }

        public void RenderPresent(){
            SDL_RenderPresent(Renderer);
        }

        public void Clear(){
            _ = SDL_SetRenderDrawColor(Renderer, 255, 255, 255, 255);
            _ = SDL_RenderClear(Renderer);
        }

        public void SetDrawColor(Color color){
            SDL_SetRenderDrawColor(Renderer, color.R, color.G, color.B, color.A);
            DrawColor = color;
        }

        public void DrawPixel(Vector2 position)
            => DrawPixel(position.X, position.Y);

        public void DrawPixel(float x, float y)
        {
            _currentTransform.ApplyTo(ref x, ref y);
            _ = SDL_RenderDrawPoint(Renderer, (int)x, (int)y);
        }

        public void DrawText(Vector2 position, string text, Font font, bool multiLine = false, float multiLineWidth = 0)
        {            
            var color = new SDL_Color{ r = DrawColor.R, g = DrawColor.G, b = DrawColor.B, a = DrawColor.A};
            var surface = multiLine ? TTF_RenderText_Blended_Wrapped(font.Pointer, text, color, (uint)multiLineWidth) : TTF_RenderText_Blended(font.Pointer, text, color); 
            var surfaceDetails = Marshal.PtrToStructure<SDL_Surface>(surface);

            int height;
            int width;

            if(multiLine)
            {
                height = surfaceDetails.h;
                width = surfaceDetails.w;
            }
            else{
                _ = TTF_SizeText(font.Pointer, text, out width, out height);
            }
            
            var texture = SDL_CreateTextureFromSurface(Renderer, surface);
            position = _currentTransform.ApplyTo(position);
            var rect = new SDL_Rect{
                x = (int)position.X,
                y = (int)position.Y,
                w = width,
                h = height
            };

            SDL_FreeSurface(surface);
            _ = SDL_RenderCopy(Renderer, texture, IntPtr.Zero, ref rect);
            SDL_DestroyTexture(texture);
        }

        public void DrawRect(float x, float y, float w, float h)
        {
            DrawLine(x, y, x + w, y);
            DrawLine(x + w, y, x + w, y + h);
            DrawLine(x + w, y + h, x, y + h);
            DrawLine(x, y + h, x, y);
        }

        public void DrawRect(Rectangle rectangle)
            => DrawRect(rectangle.Position.X, rectangle.Position.Y, rectangle.Width, rectangle.Height);

        public void FillRect(float x, float y, float w, float h)
        {
            _currentTransform.ApplyTo(ref x, ref y);
            var rect = new SDL_Rect(){ x = (int)x, y = (int)y, w = (int)w, h = (int)h};
            SDL_RenderFillRect(Renderer, ref rect);
        }

        public void FillRect(Rectangle rectangle)
            => FillRect(rectangle.Position.X, rectangle.Position.Y, rectangle.Width, rectangle.Height);

        public void StrokePolygon(Polygon polygon){
            for(var i = 0; i < polygon.Points.Count-1; i++)
            {
                DrawLine(polygon.Points[i], polygon.Points[i+1]);
            }

            DrawLine(polygon.Points[polygon.Points.Count-1], polygon.Points[0]);
        }

        public void StrokeCircle(Circle circle)
            => StrokeCircle(circle.Center, circle.Radius);

        public void StrokeCircle(Vector2 center, float radius)
        {
            center = _currentTransform.ApplyTo(center);
            var diameter = (radius * 2);

            var x = (int)(radius - 1);
            var y = 0;
            var tx = 1;
            var ty = 1;
            var error = (tx - diameter);

            while (x >= y)
            {
                //  Each of the following renders an octant of the circle
                SDL_RenderDrawPoint(Renderer, (int)center.X + x, (int)center.Y - y);
                SDL_RenderDrawPoint(Renderer, (int)center.X + x, (int)center.Y + y);
                SDL_RenderDrawPoint(Renderer, (int)center.X - x, (int)center.Y - y);
                SDL_RenderDrawPoint(Renderer, (int)center.X - x, (int)center.Y + y);
                SDL_RenderDrawPoint(Renderer, (int)center.X + y, (int)center.Y - x);
                SDL_RenderDrawPoint(Renderer, (int)center.X + y, (int)center.Y + x);
                SDL_RenderDrawPoint(Renderer, (int)center.X - y, (int)center.Y - x);
                SDL_RenderDrawPoint(Renderer, (int)center.X - y, (int)center.Y + x);

                if (error <= 0)
                {
                    ++y;
                    error += ty;
                    ty += 2;
                }

                if (error > 0)
                {
                    --x;
                    tx += 2;
                    error += (tx - diameter);
                }
            }
        }

        public void FillCircle(Circle circle)
            => FillCircle(circle.Position, circle.Radius);

        public void FillCircle(Vector2 center, float radius)
        {
            center = _currentTransform.ApplyTo(center);
            float offsetX = 0;
            float offsetY = radius;
            float d = radius -1;

            while (offsetY >= offsetX) {

                SDL_RenderDrawLine(Renderer, (int)(center.X - offsetY), (int)(center.Y + offsetX), (int)(center.X + offsetY), (int)(center.Y + offsetX));
                SDL_RenderDrawLine(Renderer, (int)(center.X - offsetX), (int)(center.Y + offsetY), (int)(center.X + offsetX), (int)(center.Y + offsetY));
                SDL_RenderDrawLine(Renderer, (int)(center.X - offsetX), (int)(center.Y - offsetY), (int)(center.X + offsetX), (int)(center.Y - offsetY));
                SDL_RenderDrawLine(Renderer, (int)(center.X - offsetY), (int)(center.Y - offsetX), (int)(center.X + offsetY), (int)(center.Y - offsetX));

                if (d >= 2*offsetX) {
                    d -= 2*offsetX + 1;
                    offsetX +=1;
                }
                else if (d < 2 * (radius - offsetY)) {
                    d += 2 * offsetY - 1;
                    offsetY -= 1;
                }
                else {
                    d += 2 * (offsetY - offsetX - 1);
                    offsetY -= 1;
                    offsetX += 1;
                }
            }
        }
        
        public void DrawLine(Vector2 start, Vector2 end)
            => DrawLine(start.X, start.Y, end.X, end.Y);

        public void DrawLine(float x1, float y1, float x2, float y2)
        {
            _currentTransform.ApplyTo(ref x1, ref y1);
            _currentTransform.ApplyTo(ref x2, ref y2);
            _ = SDL_RenderDrawLine(Renderer, (int)x1, (int)y1, (int)x2, (int)y2);
        }

        public void DrawLineWidth(float x1, float y1, float x2, float y2, float width)
        {
            _currentTransform.ApplyTo(ref x1, ref y1);
            _currentTransform.ApplyTo(ref x2, ref y2);
            _ = thickLineRGBA(Renderer, (short)x1, (short)y1, (short)x2, (short)y2, (byte)width, DrawColor.R, DrawColor.G, DrawColor.B, DrawColor.A);
        }

        public void DrawImage(Image image, float x, float y, float? w = null, float? h = null, float rotation = 0.0f)
        {
            _currentTransform.ApplyTo(ref x, ref y);

            var texture = image.Pointer;

            SDL_QueryTexture(texture, out var _, out var _, out var sourceWidth, out var sourceHeight);

            if(w.HasValue && !h.HasValue)
                h = (float)w/((float)sourceWidth/sourceHeight);

            if(h.HasValue && !w.HasValue)
                w = (float)h*((float)sourceWidth/sourceHeight);

            var sourceRect = new SDL_Rect() { x = 0, y = 0, w = sourceWidth, h = sourceHeight};
            var destRect = new SDL_Rect() { 
                x = (int)x, 
                y = (int)y, 
                w = (int)(w ?? sourceWidth), 
                h = (int)(h ?? sourceHeight)};
            
            var point = new SDL_Point(){ 
                x = w.HasValue ? (int)(w/2) : destRect.w / 2, 
                y = h.HasValue ? (int)(h/2) : destRect.h / 2
            };

            _ = SDL_RenderCopyEx(Renderer, texture, ref sourceRect, ref destRect, rotation, ref point, SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public void Dispose()
        {
            SDL_DestroyRenderer(Renderer);
            SDL_DestroyWindow(Window);
            TTF_Quit();
            SDL_Quit();
        }

        public void Save()
        {
            _transformStack.Push(_currentTransform);
        }

        public void Restore()
        {
            if(_transformStack.Any())
                _currentTransform = _transformStack.Pop();
        }

        public void Translate(Vector2 position)
            => _currentTransform = _currentTransform.Translate(position);

        public void Translate(float x, float y)
            => _currentTransform = _currentTransform.Translate(x, y);

        public void Scale(Vector2 amount)
        {
            throw new NotImplementedException();
        }

        public void Scale(float x, float y)
        {
            throw new NotImplementedException();
        }

        public void Rotate(float angle)
            => _currentTransform = _currentTransform.Rotate(angle);
    }
}