using Splorp.Core;
using Splorp.Core.Assets;
using Splorp.Core.Primitives;

namespace Splorp.Examples.Sdl2.LazyFooLessons.Lesson2
{
    public class Lesson9Scene : Scene
    {
        public Lesson9Scene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager, ITimer timer) : base(canvas, assetManager, sceneManager, timer){
        }

        public override void Draw()
        {
            _canvas.Clear();
            _canvas.SetDrawColor(Color.Red);
            _canvas.FillRect(_canvas.Width / 4, _canvas.Height / 4, _canvas.Width / 2, _canvas.Height / 2);

            _canvas.SetDrawColor(Color.Green);
            _canvas.DrawRect(_canvas.Width / 6, _canvas.Height / 6, _canvas.Width * 2 / 3, _canvas.Height * 2 / 3);

            _canvas.SetDrawColor(Color.Blue);
            _canvas.DrawLine(0, _canvas.Height / 2, _canvas.Width, _canvas.Height / 2);

            _canvas.SetDrawColor(Color.Yellow);

            for(var i = 0; i < _canvas.Height; i += 4 )
            {
                _canvas.DrawPixel(_canvas.Width / 2, i );
            }
        }
    }
}