namespace Splorp.Core.UI.Text;

//TODO: IntPtr is a leaky abstraction for SDL2 implementation. Remove
public record Font(IntPtr Pointer, string Path, int Size);