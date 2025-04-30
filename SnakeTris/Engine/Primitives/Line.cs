using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Engine.Primitives;

public class Line : List<Primitive>
{
  private readonly int _x;
  private readonly int _y;

  public Line(int x, int y)
  {
    _x = x;
    _y = y;
    Add(new Primitive
    {
      Position = new Position(x, y),
      Content = Content.Space(PixelColor.Green)
    });
  }

  public void LineTo(int x, int y)
  {
    var stepX = Math.Sign(x - _x);
    var stepY = Math.Sign(y - _y);
    for (int i = 0; i != x; i += stepX)
      for (int j = 0; j != y; j += stepY)
        Add(new Primitive
        {
          Position = new Position(j, i),
          Content = Content.Space(PixelColor.Green)
        });
  }
}
