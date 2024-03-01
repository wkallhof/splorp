using Splorp.Core;
using Splorp.Core.Assets;

namespace Splorp.Examples.Sdl2.LazyFooLessons.Lesson2
{
    public class Lesson7Scene : Scene
    {
        private readonly Image _textureImage;

        public Lesson7Scene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager, ITimer timer) : base(canvas, assetManager, sceneManager, timer){
            _textureImage = assetManager.LoadImage("./Lesson7/texture.png");
        }

        public override void Draw()
        {
            _canvas.DrawImage(_textureImage, 0, 0, _canvas.Width, _canvas.Height);
        }
    }
}