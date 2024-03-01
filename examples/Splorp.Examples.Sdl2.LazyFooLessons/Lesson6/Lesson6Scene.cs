using Splorp.Core;
using Splorp.Core.Assets;

namespace Splorp.Examples.Sdl2.LazyFooLessons.Lesson2
{
    public class Lesson6Scene : Scene
    {
        private Image _loadedImage;

        public Lesson6Scene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager, ITimer timer) : base(canvas, assetManager, sceneManager, timer){
            _loadedImage = assetManager.LoadImage("./Lesson6/loaded.png");
        }

        public override void Draw()
        {
            _canvas.DrawImage(_loadedImage, 0, 0, _canvas.Width, _canvas.Height);
        }
    }
}