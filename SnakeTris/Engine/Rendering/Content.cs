using SnakeTris.Engine.Entities;

namespace SnakeTris.Engine.Rendering;

public class Content
{
    public static Content Transparent { get; } = new()
    {
        Character = ' ',
        Color = PixelColor.Black
    };

    public static Content Space { get; } = new()
    {
        Character = ' ',
        Color = PixelColor.Green
    };

    public static Content Empty { get; } = new()
    {
        Character = 'e',
        Color = PixelColor.Red
    };

    public PixelColor Color { get; set; }
    public char Character { get; set; }

    public override string ToString()
    {
        return "[" + Character + ", " + Color + "]";
    }
}