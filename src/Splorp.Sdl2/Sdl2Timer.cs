using Splorp.Core;
using static Splorp.Sdl2.Interops.SDL;

namespace Splorp.Sdl2;

public class Sdl2Timer : ITimer
{
    public float DeltaTime { get; set; }
    public void Delay(uint milliseconds) => SDL_Delay(milliseconds);
    public uint GetTicks() => SDL_GetTicks();
}