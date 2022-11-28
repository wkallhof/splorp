namespace Splorp.Core.Assets;

//TODO: IntPtr is a leaky abstraction for SDL2 implementation. Remove
public record Image(IntPtr Pointer, string Path);