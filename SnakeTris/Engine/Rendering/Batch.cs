using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Primitives;

namespace SnakeTris.Engine.Rendering;

public class Batch
{
  private readonly List<Primitive> _items = new();
  private readonly Position _translationMap;
  private readonly int _widthMultiply;

  public Batch(Position translationMap, int widthMultiply)
  {
    _translationMap = translationMap;
    _widthMultiply = widthMultiply;
  }

  public void Pixel(int x, int y, PixelColor color, bool local = false)
  {
    var position = new Position(x, y);
    if (!local)
    {
      var primitive = new Primitive
      {
        Position = position,

        Content = Content.Space(color)
      };

      _items.Add(primitive);
      return;
    }

    position.X *= _translationMap.X;
    position.Y *= _translationMap.Y;

    position.Y += 1;
    position.X += 1;

    for (var i = 0; i < _widthMultiply; i++)
    {
      var primitive = new Primitive
      {
        Position = new Position(position.X + i, position.Y),
        Content = Content.Space(color)
      };

      _items.Add(primitive);
    }
  }

  public void Text(int x, int y, string value, PixelColor color = PixelColor.White)
  {
    var text = new Text(x, y, value, color);
    _items.AddRange(text);
  }

  public void Line(int x1, int y1, int x2, int y2)
  {
    var line = new Line(x1, y1);
    line.LineTo(x2, y2);
    _items.AddRange(line);
  }

  public void Rect(int x, int y, int width, int height)
  {
    var rect = new Rect(x, y, width, height);
    _items.AddRange(rect);
  }

  public void SmoothRect(Rectangle rectangle)
  {
    var rect = new SmoothRect(rectangle);
    _items.AddRange(rect);
  }

  public void FilledSmoothRect(Rectangle rectangle, bool local)
  {
    var rect = new FilledSmoothRect(rectangle.Scale(_translationMap));
    _items.AddRange(rect);
  }

  public IEnumerator<Primitive> GetEnumerator()
  {
    return _items.GetEnumerator();
  }
}
