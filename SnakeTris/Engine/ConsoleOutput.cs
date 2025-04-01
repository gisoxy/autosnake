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
    try
    {
      Clean();
      Console.SetBufferSize(screen.Size.Width, screen.Size.Height);
      Console.SetWindowSize(screen.Size.Width, screen.Size.Height);
    }
    catch (Exception e)
    {
      // ignored
    }
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
        Console.BackgroundColor = GetColor(item.Background);
        Console.ForegroundColor = GetColor(item.Foreground);
        Console.Write(item.Character);
      }
    }

    Console.ResetColor();
  }

  public void Clean()
  {
    Console.ResetColor();
    Console.Clear();
    Console.WriteLine("\x1b[3J");
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