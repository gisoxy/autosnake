using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Engine.Primitives;

public class Rect : List<Primitive>
{
  public Rect(int x, int y, int width, int height)
  {
    for (int i = y; i < height; i++)
    {
      for (int j = x; j < width; j++)
      {
        bool top = i == y;
        bool bottom = i == y + height - 1;
        bool left = j == x;
        bool right = j == x + width - 1;
        if (!top && !bottom && !left && !right)
          continue;

        Add(new Primitive
        {
          Position = new Position(j, i),
          Content = Content.Space
        });
      }
    }
  }
}