namespace Splorp.Core.Primitives;

public record Color (byte R, byte G, byte B, byte A = 255)
{
    public readonly static Color Black = new(0, 0, 0);
    public readonly static Color Red = new(255, 0, 0);
    public readonly static Color White = new(255, 255, 255);
    public readonly static Color Blue = new(0, 0, 255);
    public readonly static Color Green = new(0, 255, 0);
    public readonly static Color Yellow = new(255, 255, 0);

    public readonly static Color LightGrey = new(211, 211, 211);
    public readonly static Color Grey = new(128, 128, 128);
    public readonly static Color DarkGrey = new(169, 169, 169);

    public static Color Random(){
        var rand = new Random();
        return new Color((byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255));
    }
}