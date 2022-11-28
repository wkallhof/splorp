
using Splorp.Core.Primitives;

namespace Splorp.Core.Input;

public class MouseState
{
    public Vector2? Position { get; set; }
    public bool LeftButtonDown {get; set;}
    public bool RightButtonDown {get; set;}
}