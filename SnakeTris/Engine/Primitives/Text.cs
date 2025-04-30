using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Engine.Primitives;

public class Text : List<Primitive>
{
  public Text(int x, int y, string text, PixelColor color)
  {
    for (int i = 0; i < text.Length; i++)
    {
      Add(new Primitive
      {
        Position = new Position(i + x, y),
        Content = new Content
        {
          Character = text[i],
          Foreground = color,
          Background = PixelColor.Black,
        }
      });
    }
  }
}
