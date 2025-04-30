namespace SnakeTris.Engine.Entities;

public class Rectangle
{
  public Position Position { get; set; }
  public Size Size { get; set; }

  public Rectangle() : this(0, 0, 0, 0) { }

  public Rectangle(int x, int y, int width, int height)
  {
    Position = new Position(x, y);
    Size = new Size(width, height);
  }

  public Rectangle Shrink(int size)
  {
    return new Rectangle
    {
      Position = new Position(Position.X + size, Position.Y + size),
      Size = new Size(Size.Width - size, Size.Height - size)
    };
  }

  public Rectangle Scale(Position scaleFactor)
  {
    return new Rectangle
    {
      Position = new Position(Position.X, Position.Y),
      Size = new Size(Size.Width * scaleFactor.X + 1, Size.Height * scaleFactor.Y + 1)
    };
  }

  public bool Inside(Position position)
  {
    return position.X >= Position.X && position.X < Position.X + Size.Width
      && position.Y >= Position.Y && position.Y < Position.Y + Size.Height;
  }

  public bool Inside(List<Position> positions)
  {
    return positions.All(Inside);
  }

  public bool AtBottom(IEnumerable<Position> positions)
  {
    if (positions.Any(AtTop))
      return false;

    return positions.Any(AtBottom);
  }

  public bool AtBottom(Position position)
  {
    return position.Y == Position.Y + Size.Height - 1;
  }

  public bool AtTop(Position position)
  {
    return position.Y == Position.Y;
  }

  public bool AtTop(IEnumerable<Position> positions)
  {
    return positions.Any(AtTop);
  }

  public void Normalize(Position position)
  {
    if (position.X >= Position.X + Size.Width)
      position.X -= Size.Width;

    if (position.X < Position.X)
      position.X += Size.Width;

    if (position.Y >= Position.Y + Size.Height)
      position.Y -= Size.Height;

    if (position.Y < Position.Y)
      position.Y += Size.Height;
  }
}
