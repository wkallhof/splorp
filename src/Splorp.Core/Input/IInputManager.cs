namespace Splorp.Core.Input;

public interface IInputManager
{
    Action? OnExit { get; set; }
    Action<int,int>? OnResize { get; set; }

    void ProcessInputs(KeyboardState keyboard, MouseState mouse);
}