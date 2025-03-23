using SnakeTris.Engine.Config;
using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Engine;

public class ConsoleOutput
{
    private readonly Size _size;

    public ConsoleOutput(Settings.ScreenSettings screen)
    {
        _size = screen.Size;

        Console.ResetColor();
        Console.Clear();
    }

    public void Add(Content[,] frame)
    {
        for (int i = 0; i < _size.Height; i++)
        {
            for (int j = 0; j < _size.Width; j++)
            {
                var item = frame[i, j];
                if (item == Content.Empty)
                    continue;

                Console.SetCursorPosition(j, i);
                Console.BackgroundColor = GetColor(item.Color);
                Console.ForegroundColor = GetColor(item.Color);
                Console.Write(item.Character);
            }
        }
    }

    private ConsoleColor GetColor(PixelColor color)
    {
        switch (color)
        {
            case PixelColor.Red:
                return ConsoleColor.Red;
            case PixelColor.Green:
                return ConsoleColor.Green;
            case PixelColor.Blue:
                return ConsoleColor.Blue;
            case PixelColor.Yellow:
                return ConsoleColor.Yellow;
            case PixelColor.Orange:
                return ConsoleColor.DarkRed;
            case PixelColor.White:
                return ConsoleColor.White;
            default:
                return ConsoleColor.Black;
        }
    }
}