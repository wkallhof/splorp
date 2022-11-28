using Splorp.Core;
using Splorp.Core.Assets;
using Splorp.Core.Primitives;

namespace Splorp.Examples.Sdl2.LazyFooLessons.Lesson1;

public class Lesson1Scene : Scene
{
    public Lesson1Scene(ICanvas canvas, IAssetManager assetManager, SceneManager sceneManager) : base(canvas, assetManager, sceneManager){}

    public override void Draw()
    {
        _canvas.Clear();
        _canvas.SetDrawColor(Color.White);
        _canvas.FillRect(0, 0, _canvas.Width, _canvas.Height);
    }
}