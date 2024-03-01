using Splorp.Core;
using Splorp.Core.Assets;

namespace Splorp.Examples.Sdl2.LazyFooLessons.Lesson2
{
    public class Lesson3Scene : Scene
    {
        private Image xImage;

        public Lesson3Scene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager, ITimer timer) : base(canvas, assetManager, sceneManager, timer){
            xImage = assetManager.LoadImage("./Lesson3/x.bmp");
        }

        public override void Draw()
        {
            _canvas.DrawImage(xImage, 0, 0, _canvas.Width, _canvas.Height);
        }
    }
}