using Splorp.Core;
using Splorp.Core.Assets;

namespace Splorp.Examples.Sdl2.LazyFooLessons.Lesson2
{
    public class Lesson5Scene : Scene
    {
        private Image _stretchImage;

        public Lesson5Scene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager) : base(canvas, assetManager, sceneManager){
            _stretchImage = assetManager.LoadImage("./Lesson5/stretch.bmp");
        }

        public override void Draw()
        {
            _canvas.DrawImage(_stretchImage, 0, 0, _canvas.Width, _canvas.Height);
        }
    }
}