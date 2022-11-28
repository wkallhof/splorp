using Splorp.Core;
using Splorp.Examples.Sdl2.LazyFooLessons.Lesson2;
using Splorp.Sdl2;

using var canvas = new Sdl2Canvas(480, 640);
using var assetManager = new Sdl2AssetManager(canvas);

var splorp = new SplorpRunner(
    canvas: canvas,
    assetManager: assetManager,
    timer: new Sdl2Timer(),
    inputManager: new Sdl2InputManager()
);

splorp.Run<Lesson8Scene>();