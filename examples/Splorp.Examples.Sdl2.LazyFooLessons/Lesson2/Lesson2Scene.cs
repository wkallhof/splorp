using Splorp.Core;
using Splorp.Core.Assets;

namespace Splorp.Examples.Sdl2.LazyFooLessons.Lesson2
{
    public class Lesson2Scene : Scene
    {
        private Image _helloWorldImage;

        public Lesson2Scene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager, ITimer timer) : base(canvas, assetManager, sceneManager, timer){
            _helloWorldImage = assetManager.LoadImage("./Lesson2/hello_world.bmp");
        }

        public override void Draw()
        {
            _canvas.DrawImage(_helloWorldImage, 0, 0, _canvas.Width, _canvas.Height);
        }
    }
}