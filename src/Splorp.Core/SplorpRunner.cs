using Splorp.Core.Assets;
using Splorp.Core.Input;

namespace Splorp.Core
{
    public class SplorpRunner
    {
        public bool Quitting = false;

        private readonly ICanvas _canvas;
        private readonly IAssetManager _assetManager;
        private readonly ITimer _timer;
        private readonly IInputManager _inputManager;

        public SplorpRunner(ICanvas canvas, IAssetManager assetManager, ITimer timer, IInputManager inputManager)
        {
            _canvas = canvas;
            _assetManager = assetManager;
            _timer = timer;
            _inputManager = inputManager;
        }

        public void Run<TScene>() where TScene: Scene
        {
            var sceneManager = new SceneManager(_canvas, _assetManager, _timer);
            sceneManager.LoadScene<TScene>();

            _inputManager.OnExit = () => Quitting = true;
            _inputManager.OnResize = _canvas.Resize;

            Scene? currentScene = null;

            while (!Quitting) {
                var start = _timer.GetTicks();
                currentScene = sceneManager.CurrentScene;

                _inputManager.ProcessInputs(currentScene!.Keyboard, currentScene.Mouse);

                currentScene!.Draw();

                currentScene.UiElements.ForEach(x => {
                    x.Update(currentScene.Mouse);
                    x.Render(_canvas);
                });

                _canvas.RenderPresent();

                var end = _timer.GetTicks();
                var durationMs = end - start;
                if(durationMs < 0)
                    continue;

                var diff = 16 - durationMs;
                _timer.Delay(Math.Min(diff, 16));
                var totalTime = _timer.GetTicks();
                _timer.DeltaTime = (totalTime - start) / 1000f;
            }
        }
    }
}