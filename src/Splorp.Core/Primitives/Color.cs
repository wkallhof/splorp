namespace Splorp.Core.Primitives;

public class Color
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }

    public Color(byte r, byte g, byte b, byte a = 255)
        => (R, G, B, A) = (r, g, b, a);

    public static Color Black => new(0, 0, 0);
    public static Color Red => new(255, 0, 0);
    public static Color White => new(255, 255, 255);
    public static Color Blue => new(0, 0, 255);
    public static Color Green => new(0, 255, 0);
    public static Color Yellow => new(255, 255, 0);

    public static Color LightGrey => new(211, 211, 211);
    public static Color Grey => new(128, 128, 128);
    public static Color DarkGrey => new(169, 169, 169);
}