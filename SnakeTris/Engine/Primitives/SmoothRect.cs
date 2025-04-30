using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Engine.Primitives;

public class SmoothRect : List<Primitive>
{
  public enum Style
  {
    Single = 0,
    Double = 1,
    ThinLeft = 2,
    ThinTop = 3,
  }

  enum BoxShape
  {
    TopLeft = 0,
    Horizontal = 1,
    TopRight = 2,
    Vertical = 3,
    Space = 4,
    BottomLeft = 6,
    BottomRight = 8
  }

  private static readonly char[][] BoxShapes =
  [
    [
      '┌', '─', '┐',
      '│', ' ', '│',
      '└', '─', '┘'
    ],
    [
      '╔', '═', '╗',
      '║', ' ', '║',
      '╚', '═', '╝'
    ],
    [
      '╒', '═', '╕',
      '│', ' ', '│',
      '└', '─', '┘'
    ],
    [
      '┌', '─', '┐',
      '║', ' ', '║',
      '╘', '═', '╛'
    ]
  ];

  private static readonly Dictionary<char, Content> Parts = new();

  public SmoothRect(Rectangle rect, Style style = Style.Double)
  {
    Build(rect, style);
  }

  private void Build(Rectangle rect, Style style)
  {
    for (int i = rect.Position.Y; i < rect.Position.Y + rect.Size.Height; i++)
    {
      for (int j = rect.Position.X; j < rect.Position.X + rect.Size.Width; j++)
      {
        var content = Resolve(style, rect, i, j);
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

  private Content? Resolve(Style style, Rectangle rect, int i, int j)
  {
    bool top = i == rect.Position.Y;
    bool bottom = i == rect.Position.Y + rect.Size.Height - 1;
    bool left = j == rect.Position.X;
    bool right = j == rect.Position.X + rect.Size.Width - 1;
    if (!top && !bottom && !left && !right)
      return null;

    var shape = GetShape(top, bottom, left, right);
    var sideChar = BoxShapes[(int)style][(int)shape];
    return CreateShapeContent(sideChar);
  }

  private BoxShape GetShape(bool top, bool bottom, bool left, bool right)
  {
    if (top && left)
      return BoxShape.TopLeft;
    if (top && right)
      return BoxShape.TopRight;
    if (bottom && left)
      return BoxShape.BottomLeft;
    if (bottom && right)
      return BoxShape.BottomRight;
    if (top || bottom)
      return BoxShape.Horizontal;
    return BoxShape.Vertical;
  }

  protected Content CreateShapeContent(char side)
  {
    if (Parts.TryGetValue(side, out var content))
      return content;

    var part = new Content
    {
      Character = side,
      Background = PixelColor.Black,
      Foreground = PixelColor.White,
    };
    Parts.Add(side, part);
    return part;
  }
}
