using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Primitives;

namespace SnakeTris.Engine.Rendering;

public class Batch
{
    private readonly List<Primitive> _items = new();

    public void Pixel(int x, int y)
    {
        var primitive = new Primitive
        {
            Position = new Position (x, y),
            Content = Content.Space
        };
        _items.Add(primitive);
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

    public IEnumerator<Primitive> GetEnumerator()
    {
        return _items.GetEnumerator();
    }
}