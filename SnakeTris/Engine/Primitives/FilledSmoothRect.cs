using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Engine.Primitives;

public class FilledSmoothRect : SmoothRect
{
  static readonly char[] CenterShapes =
  [
    '╤',
    '╧',
    '╟',
    '╢',
    '─',
    '│',
    '┼'
  ];

  enum CenterShape
  {
    TopToBottom = 0,
    BottomToTop = 1,
    LeftToRight = 2,
    RightToLeft = 3,
    Horizontal = 4,
    Vertical = 5,
    Cross = 6,
  }

  public FilledSmoothRect(Rectangle rect, Style style = Style.Double) : base(rect, style)
  {
    FillCenter(rect);
  }

  private void FillCenter(Rectangle rect)
  {
    for (int i = rect.Position.Y; i < rect.Position.Y + rect.Size.Height; i++)
    {
      for (int j = rect.Position.X; j < rect.Position.X + rect.Size.Width; j++)
      {
        var content = Resolve(rect, i, j);
        if (content == null)
          continue;

        Add(new Primitive
        {
          Position = new Position(j, i),
          Content = content,
        });
      }
    }
  }

  private Content? Resolve(Rectangle rect, int i, int j)
  {
    bool top = i == rect.Position.Y;
    bool bottom = i == rect.Position.Y + rect.Size.Height - 1;
    bool left = j == rect.Position.X;
    bool right = j == rect.Position.X + rect.Size.Width - 1;
    bool oddX = j % 3 == 0;
    bool oddY = i % 2 == 0;
    if ((top && left) || (top && right) || (bottom && left) || (bottom && right))
      return null;
    if ((left && !oddY) || (right && !oddY))
      return null;
    if ((top && !oddX) || (bottom && !oddX))
      return null;
    if (!oddX && !oddY)
      return null;

    var shape = GetShape(top, bottom, left, right, oddX, oddY);
    var sideChar = CenterShapes[(int)shape];
    return CreateShapeContent(sideChar);
  }

  private CenterShape GetShape(bool top, bool bottom, bool left, bool right, bool oddX, bool oddY)
  {
    if (top && oddX)
      return CenterShape.TopToBottom;

    if (bottom && oddX)
      return CenterShape.BottomToTop;

    if (left && oddY)
      return CenterShape.LeftToRight;

    if (right && oddY)
      return CenterShape.RightToLeft;

    if (oddX && oddY)
      return CenterShape.Cross;

    if (oddX)
      return CenterShape.Vertical;

    // if (oddY)
    return CenterShape.Horizontal;
  }
}
