using SnakeTris.Engine.Entities;

namespace SnakeTris.Engine.Rendering;

public class Content
{
  public static Content Transparent { get; } = new()
  {
    Character = ' ',
    Foreground = PixelColor.Black,
    Background = PixelColor.Black,
  };

  public static Content Space { get; } = new()
  {
    Character = ' ',
    Foreground = PixelColor.Green,
    Background = PixelColor.Green,
  };

  public static Content Empty { get; } = new()
  {
    Character = 'e',
    Foreground = PixelColor.Red,
    Background = PixelColor.Red
  };

  public PixelColor Foreground { get; set; }
  public PixelColor Background { get; set; }
  public char Character { get; set; }

  public override string ToString()
  {
    return "[" + Character + ", " + Foreground + "]";
  }
}