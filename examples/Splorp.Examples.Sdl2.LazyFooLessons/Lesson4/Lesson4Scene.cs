using Splorp.Core;
using Splorp.Core.Assets;

namespace Splorp.Examples.Sdl2.LazyFooLessons.Lesson2
{
    public class Lesson4Scene : Scene
    {
        private readonly Image _upImage;
        private readonly Image _downImage;
        private readonly Image _leftImage;
        private readonly Image _rightImage;
        private readonly Image _defaultImage;

        public Lesson4Scene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager) : base(canvas, assetManager, sceneManager){
            _upImage = assetManager.LoadImage("./Lesson4/Assets/up.bmp");
            _downImage = assetManager.LoadImage("./Lesson4/Assets/down.bmp");
            _leftImage = assetManager.LoadImage("./Lesson4/Assets/left.bmp");
            _rightImage = assetManager.LoadImage("./Lesson4/Assets/right.bmp");
            _defaultImage = assetManager.LoadImage("./Lesson4/Assets/press.bmp");
        }

        public override void Draw()
        {
            var image = Keyboard.Up.Down ? _upImage
                     : Keyboard.Down.Down ? _downImage
                     : Keyboard.Left.Down ? _leftImage
                     : Keyboard.Right.Down ? _rightImage
                     : _defaultImage;

            _canvas.DrawImage(image, 0, 0, _canvas.Width, _canvas.Height);
        }
    }
}